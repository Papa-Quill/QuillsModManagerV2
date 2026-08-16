using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using QuillsModManagerV2.Util.Controls.AnimatedList;

namespace QuillsModManagerV2.Util.Controls
{
    public class ModListView : Control
    {
        private string _emptyMessage;
        private float _emptyOpacity = 1f;
        private bool _emptyPulseIncreasing = false;
        private Timer _emptyTimer;

        private BindingList<ModEntry> _items;
        private CustomVScrollBar _vScroll;
        private Dictionary<int, float> _toggleProgress = new Dictionary<int, float>();
        private Dictionary<int, int> _toggleStart = new Dictionary<int, int>();
        private Dictionary<int, int> _toggleTarget = new Dictionary<int, int>();
        private Dictionary<int, float> _rowOffsets = new Dictionary<int, float>();
        private Dictionary<int, float> _rowOffsetsTarget = new Dictionary<int, float>();
        private int _lastDragTarget = -1;
        private int _dragMouseY = 0;
        private Timer _autoScrollTimer;
        private int _autoScrollDir = 0;
        private float _autoScrollSpeed = 0f;
        private const float AUTO_SCROLL_ACCEL = 0.18f;
        private const float AUTO_SCROLL_MAX = 6f;
        private int _rowHeight = 42;
        private int _hoverIndex = -1;
        private HashSet<int> _selectedIndices = new HashSet<int>();
        private int _selectedIndex = -1;
        private int _lastSelectedIndex = -1;
        private int _dragIndex = -1;
        private int _dragStartY = 0;
        private bool _isDragging = false;
        private bool _suppressListAnimation;
        private int _dragTarget = -1;
        private CustomToolTip _tooltip;
        private int _tooltipShownIndex = -1;
        public event Action<int> ItemRightClicked;
        public event Action<int> ItemDoubleClicked;
        public event Action ItemsReordered;
        public event Action<int, bool> ToggleChanged;
        private readonly AnimatedListController _animation = new AnimatedListController();
        private Timer _animTimer;
        public IEnumerable<int> SelectedIndices => _selectedIndices;
        private GraphicsPath _cachedTogglePath;
        private readonly SolidBrush _cachedBrush = new SolidBrush(Color.Transparent);

        public BindingList<ModEntry> Items
        {
            get => _items;
            set
            {
                if (ReferenceEquals(_items, value))
                    return;

                if (_items != null)
                    _items.ListChanged -= Items_ListChanged;

                _items = value;

                if (_items != null)
                    _items.ListChanged += Items_ListChanged;

                UpdateScroll();

                UpdateEmptyMessage();

                if (_animateNextItemsChange)
                {
                    _animateNextItemsChange = false;
                    BuildAnimatedItems(true);
                }
                else
                {
                    BuildAnimatedItems(false);
                }
                Invalidate();
            }
        }

        public void RefreshWithAnimation()
        {
            BuildAnimatedItems(true);
            PlayEntryAnimation();
            Invalidate();
        }

        public int SelectedIndex => _selectedIndex;
        public ModEntry SelectedItem =>
            (_items != null && _selectedIndex >= 0 && _selectedIndex < _items.Count)
                ? _items[_selectedIndex]
                : null;

        public ModListView()
        {
            SetStyle(
                ControlStyles.UserPaint
                    | ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.OptimizedDoubleBuffer,
                true
            );
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
            _vScroll = new CustomVScrollBar { Dock = DockStyle.Right };
            _vScroll.Scroll += (s, e) => Invalidate();
            Controls.Add(_vScroll);

            try
            {
                Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            }
            catch { }

            _animation.AnimationUpdated += () =>
            {
                Invalidate(false);
            };

            _emptyTimer = new Timer { Interval = 40 };
            _emptyTimer.Tick += (s, e) =>
            {
                if (string.IsNullOrEmpty(_emptyMessage))
                {
                    if (_emptyTimer.Enabled)
                        _emptyTimer.Stop();
                    return;
                }

                float delta = 0.03f;
                if (_emptyPulseIncreasing)
                    _emptyOpacity += delta;
                else
                    _emptyOpacity -= delta;
                if (_emptyOpacity >= 1f)
                {
                    _emptyOpacity = 1f;
                    _emptyPulseIncreasing = false;
                }
                if (_emptyOpacity <= 0.35f)
                {
                    _emptyOpacity = 0.35f;
                    _emptyPulseIncreasing = true;
                }
                Invalidate();
            };

            _animTimer = new Timer { Interval = 16 };
            _animTimer.Tick += (s, e) =>
            {
                bool active = false;

                if (_toggleProgress.Count > 0)
                {
                    var keys = _toggleProgress.Keys.ToArray();
                    foreach (var k in keys)
                    {
                        _toggleProgress[k] += 0.15f;
                        if (_toggleProgress[k] >= 1f)
                        {
                            _toggleProgress[k] = 1f;
                            _toggleProgress.Remove(k);
                            _toggleStart.Remove(k);
                            _toggleTarget.Remove(k);
                        }
                        else
                            active = true;
                    }
                }

                if (_rowOffsetsTarget.Count > 0)
                {
                    var keys = _rowOffsetsTarget.Keys.ToArray();
                    foreach (var k in keys)
                    {
                        float cur = _rowOffsets.ContainsKey(k) ? _rowOffsets[k] : 0f;
                        float target = _rowOffsetsTarget[k];
                        float delta = (target - cur) * 0.25f;
                        if (Math.Abs(target - cur) < 0.5f)
                        {
                            _rowOffsets[k] = target;
                            _rowOffsetsTarget.Remove(k);
                        }
                        else
                        {
                            _rowOffsets[k] = cur + delta;
                            active = true;
                        }
                    }
                }

                if (!active)
                    _animTimer.Stop();

                Invalidate(false);
            };

            _autoScrollTimer = new Timer { Interval = 250 };
            _autoScrollTimer.Tick += (s, e) =>
            {
                if (!_isDragging)
                {
                    _autoScrollTimer.Stop();
                    _autoScrollDir = 0;
                    _autoScrollSpeed = 0f;
                    return;
                }
                if (_autoScrollDir == 0)
                {
                    _autoScrollSpeed = 0f;
                    return;
                }

                _autoScrollSpeed += AUTO_SCROLL_ACCEL;
                if (_autoScrollSpeed > AUTO_SCROLL_MAX)
                    _autoScrollSpeed = AUTO_SCROLL_MAX;
                int move = (int)Math.Ceiling(_autoScrollSpeed);

                if (_autoScrollDir == -1)
                {
                    if (_vScroll.Value > _vScroll.Minimum)
                    {
                        int newVal = Math.Max(_vScroll.Minimum, _vScroll.Value - move);
                        if (newVal != _vScroll.Value)
                        {
                            _vScroll.Value = newVal;
                            UpdateOffsetsForDrag();
                            _animation.Start();
                        }
                        else
                        {
                            _autoScrollTimer.Stop();
                        }
                    }
                }
                else if (_autoScrollDir == 1)
                {
                    if (_vScroll.Value < _vScroll.Maximum)
                    {
                        int newVal = Math.Min(_vScroll.Maximum, _vScroll.Value + move);
                        if (newVal != _vScroll.Value)
                        {
                            _vScroll.Value = newVal;
                            UpdateOffsetsForDrag();
                            _animation.Start();
                        }
                        else
                        {
                            _autoScrollTimer.Stop();
                        }
                    }
                }
            };
            BackColor = Properties.Settings.Default.ButtonColor;
            ForeColor = Properties.Settings.Default.TextColor;
            Font = new Font("Segoe UI", 9F);

            _animation.AnimationUpdated += () =>
            {
                Invalidate(false);
            };
        }

        private bool _animateNextItemsChange = false;

        private void BuildAnimatedItems(bool playAnimation = true)
        {
            _animation.ClearItems();

            if (_items == null)
                return;

            for (int i = 0; i < _items.Count; i++)
            {
                AnimatedItem item = new AnimatedItem
                {
                    Key = _items[i],
                    Visible = true,
                    Removing = false,

                    CurrentY = i * _rowHeight,
                    TargetY = i * _rowHeight,

                    Opacity = playAnimation ? 0f : 1f,
                    OffsetY = playAnimation ? 8f : 0f,

                    AnimationStart = _animation.AnimationClock.ElapsedMilliseconds,
                    Delay = i * 45
                };

                _animation.RegisterItem(item);
            }

            if (playAnimation)
                _animation.Start();
        }

        public void PlayEntryAnimation()
        {
            if (!Visible)
                return;

            if (_animation.AnimatedItems.Count == 0)
                return;

            long now = _animation.AnimationClock.ElapsedMilliseconds;

            for (int i = 0; i < _animation.AnimatedItems.Count; i++)
            {
                AnimatedItem item = _animation.AnimatedItems[i];

                item.Opacity = 0f;
                item.OffsetY = 4f;

                item.AnimationStart = now;
                item.Delay = i * 45;
            }

            _animation.Start();
        }

        private void Items_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                case ListChangedType.ItemDeleted:
                case ListChangedType.Reset:
                    UpdateScroll();

                    Sync_animation();

                    break;

                case ListChangedType.ItemChanged:
                    break;
            }

            if (_selectedIndex >= (_items?.Count ?? 0))
            {
                _selectedIndex = -1;
                _selectedIndices.Clear();
            }

            UpdateEmptyMessage();
            Invalidate(false);
        }

        private void Sync_animation()
        {
            if (_items == null)
                return;

            var existing = new Dictionary<object, AnimatedItem>(_animation.AnimationLookup);

            _animation.AnimatedItems.Clear();
            _animation.AnimationLookup.Clear();

            for (int i = 0; i < _items.Count; i++)
            {
                ModEntry entry = _items[i];

                if (existing.TryGetValue(entry, out AnimatedItem anim))
                {
                    anim.TargetY = i * _rowHeight;

                    _animation.AnimatedItems.Add(anim);
                    _animation.AnimationLookup[entry] = anim;
                }
                else
                {
                    AnimatedItem newAnim = new AnimatedItem
                    {
                        Key = entry,
                        Opacity = 1f,
                        OffsetY = 0f,
                        CurrentY = i * _rowHeight,
                        TargetY = i * _rowHeight
                    };

                    _animation.AnimatedItems.Add(newAnim);
                    _animation.AnimationLookup[entry] = newAnim;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_vScroll != null)
                UpdateScroll();

            Invalidate();
        }

        private void UpdateScroll()
        {
            int count = _items?.Count ?? 0;

            int contentHeight = Height;
            int visibleRows = Math.Max(1, contentHeight / _rowHeight);

            _vScroll.Minimum = 0;
            _vScroll.LargeChange = visibleRows;
            _vScroll.Maximum = Math.Max(0, count - visibleRows);

            _vScroll.Enabled = count > visibleRows;

            if (!_vScroll.Enabled)
            {
                _vScroll.Value = 0;
            }
            else
            {
                _vScroll.Value = Math.Min(_vScroll.Value, _vScroll.Maximum);
            }

            UpdateEmptyMessage();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_animation.AnimatedItems.Count == 0 && _items != null && _items.Count > 0)
            {
                BuildAnimatedItems(false);
            }

            if (Width <= 0 || Height <= 0)
                return;

            Color themeBackground = Properties.Settings.Default.ButtonColor;
            Color themeSecondary = Properties.Settings.Default.BGSecondary;
            Color themeTertiary = Properties.Settings.Default.BGTertiary;
            Color themeText = Properties.Settings.Default.TextColor;
            Color themeDetail = Properties.Settings.Default.DetailColor;
            Color themeActive = Properties.Settings.Default.DetailActive;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            e.Graphics.Clear(themeBackground);
            if (!string.IsNullOrEmpty(_emptyMessage))
            {
                try
                {
                    using (
                        SolidBrush b = new SolidBrush(
                            Color.FromArgb(
                                (int)(_emptyOpacity * 255f),
                                Properties.Settings.Default.PlaceholderColor
                            )
                        )
                    )
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        RectangleF msgRect = new RectangleF(
                            8,
                            Height / 2f - 24f,
                            Math.Max(1, Width - _vScroll.Width - 16),
                            48f
                        );
                        using (Font f = new Font(Font.FontFamily, 11f, FontStyle.Regular))
                        {
                            e.Graphics.DrawString(_emptyMessage, f, b, msgRect, sf);
                        }
                    }
                }
                catch { }

                return;
            }
            int start = _vScroll.Value;
            int visible = Math.Max(1, (Height + _rowHeight - 1) / _rowHeight);
            for (int i = 0; i < visible; i++)
            {
                int idx = start + i;
                if (_items == null || idx >= _items.Count)
                    break;
                var m = _items[idx];
                _animation.AnimationLookup.TryGetValue(m, out AnimatedItem anim);
                if (anim == null)
                {
                    anim = new AnimatedItem
                    {
                        Key = m,
                        Opacity = 0f,
                        OffsetY = 8f,
                        AnimationStart = _animation.AnimationClock.ElapsedMilliseconds,
                        Delay = i * 45
                    };

                    _animation.AnimatedItems.Add(anim);
                    _animation.AnimationLookup[m] = anim;

                    _animation.Start();
                }

                bool isSelected = _selectedIndices.Contains(idx);
                bool isHovered = _hoverIndex == idx;
                int y = (int)anim.CurrentY - (_vScroll.Value * _rowHeight);
                float dragOffset = _rowOffsets.ContainsKey(idx) ? _rowOffsets[idx] : 0f;
                float py = y + dragOffset + anim.OffsetY;
                Rectangle rowRect = new Rectangle(0, (int)py, Width - _vScroll.Width, _rowHeight);
                Color rowBack = themeSecondary;
                if (isSelected)
                    rowBack = themeTertiary;
                else if (isHovered)
                    rowBack = Color.FromArgb(
                        Math.Min(themeSecondary.A + 20, 255),
                        Math.Min(themeSecondary.R + 10, 255),
                        Math.Min(themeSecondary.G + 10, 255),
                        Math.Min(themeSecondary.B + 10, 255)
                    );
                if (_isDragging && idx == _dragIndex)
                {
                    using (Brush br = new SolidBrush(Color.FromArgb(60, rowBack)))
                    {
                        e.Graphics.FillRectangle(
                            br,
                            new Rectangle(0, (int)py, Width - _vScroll.Width, _rowHeight)
                        );
                    }

                    continue;
                }

                using (Brush br = new SolidBrush(WithOpacity(rowBack, anim.Opacity)))
                {
                    _cachedBrush.Color = WithOpacity(rowBack, anim.Opacity);
                    e.Graphics.FillRectangle(_cachedBrush, rowRect);
                }

                Rectangle iconRect = new Rectangle(8, (int)py + 6, 28, 28);
                if (m.Icon != null)
                {
                    ColorMatrix matrix = new ColorMatrix { Matrix33 = anim.Opacity };

                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        attributes.SetColorMatrix(matrix);

                        e.Graphics.DrawImage(
                            m.Icon,
                            iconRect,
                            0,
                            0,
                            m.Icon.Width,
                            m.Icon.Height,
                            GraphicsUnit.Pixel,
                            attributes
                        );
                    }
                }

                int toggleX = iconRect.Right + 8;
                int toggleY = (int)py + 8;
                Rectangle toggleRect = new Rectangle(toggleX, toggleY, 34, 22);
                DrawToggle(e.Graphics, toggleRect, m.Enabled, idx, anim.Opacity);
                int dotsX = Width - _vScroll.Width - 28;
                Rectangle dotsRect = new Rectangle(dotsX, (int)py + (_rowHeight - 18) / 2, 18, 18);
                Rectangle warningRect = new Rectangle(
                    dotsRect.Left - 22,
                    (int)py + (_rowHeight - 18) / 2,
                    18,
                    18
                );
                if (m.HasConflict && m.Enabled)
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(255, 210, 140, 0)))
                    {
                        Point[] tri = new Point[]
                        {
                            new Point(warningRect.Left + warningRect.Width / 2, warningRect.Top),
                            new Point(warningRect.Left, warningRect.Bottom),
                            new Point(warningRect.Right, warningRect.Bottom)
                        };
                        e.Graphics.FillPolygon(b, tri);
                    }
                    using (Pen p = new Pen(Color.Black, 1))
                    {
                        e.Graphics.DrawPolygon(
                            p,
                            new Point[]
                            {
                                new Point(
                                    warningRect.Left + warningRect.Width / 2,
                                    warningRect.Top
                                ),
                                new Point(warningRect.Left, warningRect.Bottom),
                                new Point(warningRect.Right, warningRect.Bottom)
                            }
                        );
                    }
                }
                int textX = toggleRect.Right + 8;
                int colTitleW = 220;
                int colAuthorW = 120;
                int colVersionW = 80;
                int rightControlsWidth = 60;
                float offset2 = dragOffset + anim.OffsetY;
                RectangleF titleRect = new RectangleF(textX, y + 8 + offset2, colTitleW, 20);
                RectangleF authorRect2 = new RectangleF(
                    textX + colTitleW,
                    y + 8 + offset2,
                    colAuthorW,
                    20
                );
                RectangleF versionRect2 = new RectangleF(
                    textX + colTitleW + colAuthorW,
                    y + 8 + offset2,
                    colVersionW,
                    20
                );
                int descWidth = Math.Max(
                    1,
                    Width
                        - textX
                        - colTitleW
                        - colAuthorW
                        - colVersionW
                        - _vScroll.Width
                        - rightControlsWidth
                );

                RectangleF descRect = new RectangleF(
                    textX + colTitleW + colAuthorW + colVersionW,
                    y + 4 + offset2,
                    descWidth,
                    _rowHeight - 8
                );

                DrawFadedText(
                    e.Graphics,
                    m.Title,
                    titleRect,
                    anim.Opacity,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );

                DrawFadedText(
                    e.Graphics,
                    m.Author,
                    authorRect2,
                    anim.Opacity,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );

                DrawFadedText(
                    e.Graphics,
                    m.Version,
                    versionRect2,
                    anim.Opacity,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );

                DrawFadedText(
                    e.Graphics,
                    m.Description,
                    descRect,
                    anim.Opacity,
                    TextFormatFlags.VerticalCenter
                        | TextFormatFlags.Left
                        | TextFormatFlags.EndEllipsis
                );

                if (_isDragging && idx == _dragTarget)
                {
                    using (Pen p = new Pen(themeActive, 3))
                    {
                        int lineY = (int)py;
                        e.Graphics.DrawLine(p, 4, lineY, Width - _vScroll.Width - 4, lineY);
                    }
                }
                if (
                    dotsRect.Width > 0
                    && dotsRect.Height > 0
                    && dotsRect.Left >= 0
                    && dotsRect.Top >= 0
                    && dotsRect.Right <= Width
                    && dotsRect.Bottom <= Height
                )
                {
                    {
                        _cachedBrush.Color = WithOpacity(themeDetail, anim.Opacity);

                        int dotSize = 3;

                        e.Graphics.FillEllipse(
                            _cachedBrush,
                            dotsRect.Left + 3,
                            dotsRect.Top + 7,
                            dotSize,
                            dotSize
                        );

                        e.Graphics.FillEllipse(
                            _cachedBrush,
                            dotsRect.Left + 8,
                            dotsRect.Top + 7,
                            dotSize,
                            dotSize
                        );

                        e.Graphics.FillEllipse(
                            _cachedBrush,
                            dotsRect.Left + 13,
                            dotsRect.Top + 7,
                            dotSize,
                            dotSize
                        );
                    }
                }
            }

            if (_isDragging && _dragIndex >= 0 && _dragIndex < _items.Count)
            {
                var m = _items[_dragIndex];
                float drawY = _dragMouseY - (_rowHeight / 2);
                drawY = Math.Max(0, Math.Min(this.Height - _rowHeight, drawY));
                Rectangle floatRect = new Rectangle(
                    0,
                    (int)drawY,
                    Width - _vScroll.Width,
                    _rowHeight
                );
                Rectangle shadowRect = new Rectangle(
                    floatRect.Left + 6,
                    floatRect.Top + 6,
                    floatRect.Width,
                    floatRect.Height
                );
                using (GraphicsPath sp = RoundedRect(shadowRect, 6))
                using (Brush sb = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                {
                    e.Graphics.FillPath(sb, sp);
                }
                using (GraphicsPath gp = RoundedRect(floatRect, 6))
                using (
                    Brush fb = new SolidBrush(
                        Color.FromArgb(
                            240,
                            Properties.Settings.Default.BGTertiary.R,
                            Properties.Settings.Default.BGTertiary.G,
                            Properties.Settings.Default.BGTertiary.B
                        )
                    )
                )
                {
                    e.Graphics.FillPath(fb, gp);
                }
                Rectangle iconRect = new Rectangle(8, (int)drawY + 6, 28, 28);
                if (m.Icon != null)
                    e.Graphics.DrawImage(m.Icon, iconRect);
                int toggleX = iconRect.Right + 8;
                int toggleY = (int)drawY + 8;
                Rectangle toggleRect = new Rectangle(toggleX, toggleY, 34, 22);
                DrawToggle(e.Graphics, toggleRect, m.Enabled, _dragIndex, .8f);
                int dotsX = Width - _vScroll.Width - 28;
                Rectangle dotsRect = new Rectangle(
                    dotsX,
                    (int)drawY + (_rowHeight - 18) / 2,
                    18,
                    18
                );
                Rectangle warningRect = new Rectangle(
                    dotsRect.Left - 22,
                    (int)drawY + (_rowHeight - 18) / 2,
                    18,
                    18
                );
                if (m.HasConflict && m.Enabled)
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(255, 210, 140, 0)))
                    {
                        Point[] tri = new Point[]
                        {
                            new Point(warningRect.Left + warningRect.Width / 2, warningRect.Top),
                            new Point(warningRect.Left, warningRect.Bottom),
                            new Point(warningRect.Right, warningRect.Bottom)
                        };
                        e.Graphics.FillPolygon(b, tri);
                    }
                    using (Pen p = new Pen(Color.Black, 1))
                        e.Graphics.DrawPolygon(
                            p,
                            new Point[]
                            {
                                new Point(
                                    warningRect.Left + warningRect.Width / 2,
                                    warningRect.Top
                                ),
                                new Point(warningRect.Left, warningRect.Bottom),
                                new Point(warningRect.Right, warningRect.Bottom)
                            }
                        );
                    using (Brush btxt = new SolidBrush(Color.Black))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        e.Graphics.DrawString("!", this.Font, btxt, warningRect, sf);
                    }
                }
                int textX = toggleRect.Right + 8;
                int colTitleW = 220;
                int colAuthorW = 120;
                int colVersionW = 80;
                int rightControlsWidth = 60;
                RectangleF titleRect = new RectangleF(textX, (int)drawY + 8, colTitleW, 20);
                RectangleF authorRect = new RectangleF(
                    textX + colTitleW,
                    (int)drawY + 8,
                    colAuthorW,
                    20
                );
                RectangleF versionRect = new RectangleF(
                    textX + colTitleW + colAuthorW,
                    (int)drawY + 8,
                    colVersionW,
                    20
                );
                int descWidth = Math.Max(
                    1,
                    Width
                        - textX
                        - colTitleW
                        - colAuthorW
                        - colVersionW
                        - _vScroll.Width
                        - rightControlsWidth
                );

                RectangleF descRect = new RectangleF(
                    textX + colTitleW + colAuthorW + colVersionW,
                    (int)drawY + 8,
                    descWidth,
                    20
                );
                TextRenderer.DrawText(
                    e.Graphics,
                    m.Title,
                    Font,
                    Rectangle.Round(titleRect),
                    Properties.Settings.Default.TextColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
                TextRenderer.DrawText(
                    e.Graphics,
                    m.Author,
                    Font,
                    Rectangle.Round(authorRect),
                    Properties.Settings.Default.TextColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
                TextRenderer.DrawText(
                    e.Graphics,
                    m.Version,
                    Font,
                    Rectangle.Round(versionRect),
                    Properties.Settings.Default.TextColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                );
                TextRenderer.DrawText(
                    e.Graphics,
                    m.Description,
                    Font,
                    Rectangle.Round(descRect),
                    Properties.Settings.Default.TextColor,
                    TextFormatFlags.VerticalCenter
                        | TextFormatFlags.Left
                        | TextFormatFlags.EndEllipsis
                );
                using (Brush b = new SolidBrush(Properties.Settings.Default.DetailColor))
                {
                    int dotSize = 3;
                    e.Graphics.FillEllipse(
                        b,
                        dotsRect.Left + 3,
                        dotsRect.Top + 7,
                        dotSize,
                        dotSize
                    );
                    e.Graphics.FillEllipse(
                        b,
                        dotsRect.Left + 8,
                        dotsRect.Top + 7,
                        dotSize,
                        dotSize
                    );
                    e.Graphics.FillEllipse(
                        b,
                        dotsRect.Left + 13,
                        dotsRect.Top + 7,
                        dotSize,
                        dotSize
                    );
                }
            }
        }

        private void DrawToggle(Graphics g, Rectangle r, bool on, int idx, float opacity)
        {
            int startVal = _toggleStart.ContainsKey(idx) ? _toggleStart[idx] : (on ? 1 : 0);
            int targetVal = _toggleTarget.ContainsKey(idx) ? _toggleTarget[idx] : (on ? 1 : 0);
            float p = _toggleProgress.ContainsKey(idx) ? _toggleProgress[idx] : 1f;
            float pos = startVal + (targetVal - startVal) * p;
            Color offBase = Properties.Settings.Default.BGSecondary;
            Color offColor = Color.FromArgb(160, offBase.R, offBase.G, offBase.B);
            Color onColor = Properties.Settings.Default.DetailActive;
            Color bg = WithOpacity(InterpolateColor(offColor, onColor, pos), opacity);

            using (GraphicsPath path = RoundedRect(r, r.Height / 2))
            using (Brush b = new SolidBrush(bg))
            {
                g.FillPath(b, path);
            }

            int pad = 3;
            int knobSize = r.Height - pad * 2 - 2;
            int knobRange = r.Width - pad * 2 - knobSize;
            int knobX = r.Left + pad + (int)(knobRange * pos);
            Rectangle knobRect = new Rectangle(knobX, r.Top + pad + 1, knobSize, knobSize);
            using (
                Brush kb = new SolidBrush(
                    WithOpacity(Properties.Settings.Default.TextColor, opacity)
                )
            )
            {
                g.FillEllipse(kb, knobRect);
            }
            using (Pen pen = new Pen(WithOpacity(Properties.Settings.Default.DetailColor, opacity)))
            {
                g.DrawPath(pen, RoundedRect(r, r.Height / 2));
            }
        }

        private GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            GraphicsPath p = new GraphicsPath();
            int d = radius * 2;
            p.AddArc(r.Left, r.Top, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Top, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        private Color InterpolateColor(Color a, Color b, float t)
        {
            t = Math.Max(0f, Math.Min(1f, t));
            int A = (int)(a.A + (b.A - a.A) * t);
            int R = (int)(a.R + (b.R - a.R) * t);
            int G = (int)(a.G + (b.G - a.G) * t);
            int B = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(A, R, G, B);
        }

        private Color WithOpacity(Color color, float opacity)
        {
            int alpha = (int)(255 * opacity);

            if (alpha < 0)
                alpha = 0;

            if (alpha > 255)
                alpha = 255;

            return Color.FromArgb(alpha, color);
        }

        private void DrawFadedText(
            Graphics g,
            string text,
            RectangleF rect,
            float opacity,
            TextFormatFlags flags
        )
        {
            if (string.IsNullOrEmpty(text) || rect.Width <= 0 || rect.Height <= 0)
                return;

            int alpha = (int)(255f * opacity);

            if (alpha <= 0)
                return;

            using (
                SolidBrush brush = new SolidBrush(
                    Color.FromArgb(alpha, Properties.Settings.Default.TextColor)
                )
            )
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;

                    if ((flags & TextFormatFlags.EndEllipsis) != 0)
                        sf.Trimming = StringTrimming.EllipsisCharacter;

                    g.DrawString(text, Font, brush, rect, sf);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int idx = IndexFromPoint(e.Location);
            if (idx != _hoverIndex)
            {
                _hoverIndex = idx;
                Invalidate(false);
            }
            if (
                _dragIndex >= 0
                && !_isDragging
                && (MouseButtons & MouseButtons.Left) == MouseButtons.Left
            )
            {
                if (Math.Abs(e.Y - _dragStartY) > 6)
                {
                    _isDragging = true;
                    _dragTarget = _dragIndex;
                    Capture = true;
                    _rowOffsets.Clear();
                    _rowOffsetsTarget.Clear();
                    _lastDragTarget = _dragTarget;
                    UpdateOffsetsForDrag();
                    _animation.Start();
                    if (_animTimer != null && !_animTimer.Enabled)
                        _animTimer.Start();
                }
            }
            if (_isDragging)
            {
                int target = IndexFromPoint(new Point(e.X, e.Y));
                if (target >= 0)
                    _dragTarget = target;
                _dragMouseY = e.Y;
                if (_dragTarget != _lastDragTarget)
                {
                    UpdateOffsetsForDrag();
                    _lastDragTarget = _dragTarget;
                    _animation.Start();
                }
                int margin = 24;
                if (e.Y < margin)
                {
                    if (_autoScrollDir != -1)
                    {
                        _autoScrollDir = -1;
                        _autoScrollSpeed = 0f;
                        _autoScrollTimer.Start();
                    }
                }
                else if (e.Y > this.Height - margin)
                {
                    if (_autoScrollDir != 1)
                    {
                        _autoScrollDir = 1;
                        _autoScrollSpeed = 0f;
                        _autoScrollTimer.Start();
                    }
                }
                else
                {
                    if (_autoScrollDir != 0)
                    {
                        _autoScrollDir = 0;
                        _autoScrollSpeed = 0f;
                        _autoScrollTimer.Stop();
                    }
                }
                Invalidate(false);
            }
            if (idx >= 0 && _items != null)
            {
                int visibleStart = _vScroll.Value;
                int y = (idx - visibleStart) * _rowHeight;
                Rectangle iconRect = new Rectangle(8, y + 6, 28, 28);
                int dotsX = Width - _vScroll.Width - 28;
                Rectangle dotsRect = new Rectangle(dotsX, y + (_rowHeight - 18) / 2, 18, 18);
                Rectangle warningRect = new Rectangle(
                    dotsRect.Left - 22,
                    y + (_rowHeight - 18) / 2,
                    18,
                    18
                );
                if (
                    _items[idx].HasConflict
                    && _items[idx].Enabled
                    && warningRect.Contains(e.Location)
                )
                {
                    if (_tooltip == null)
                        _tooltip = new CustomToolTip();
                    if (_tooltipShownIndex != idx)
                    {
                        _tooltipShownIndex = idx;
                        _tooltip.Show(
                            _items[idx].ConflictModNames,
                            this,
                            e.Location.X + 12,
                            e.Location.Y + 12,
                            5000
                        );
                    }
                }
                else
                {
                    _tooltip?.Hide(this);
                    _tooltipShownIndex = -1;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int idx = IndexFromPoint(e.Location);
            if (idx >= 0)
            {
                Focus();
                int visibleStart = _vScroll.Value;
                int y = (idx - visibleStart) * _rowHeight;
                Rectangle iconRect = new Rectangle(8, y + 6, 28, 28);
                int toggleX = iconRect.Right + 8;
                Rectangle toggleRect = new Rectangle(toggleX, y + 8, 34, 22);
                int dotsX = Width - _vScroll.Width - 28;
                Rectangle dotsRect = new Rectangle(dotsX, y + (_rowHeight - 18) / 2, 18, 18);
                Rectangle warningRect = new Rectangle(
                    dotsRect.Left - 22,
                    y + (_rowHeight - 18) / 2,
                    18,
                    18
                );
                bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (shift && _lastSelectedIndex >= 0)
                {
                    int a = Math.Min(_lastSelectedIndex, idx);
                    int b = Math.Max(_lastSelectedIndex, idx);
                    _selectedIndices.Clear();
                    for (int s = a; s <= b; s++)
                        _selectedIndices.Add(s);
                    _selectedIndex = idx;
                    Invalidate();
                }
                else if (ctrl)
                {
                    if (_selectedIndices.Contains(idx))
                        _selectedIndices.Remove(idx);
                    else
                        _selectedIndices.Add(idx);
                    _selectedIndex = idx;
                    Invalidate();
                }
                else
                {
                    _selectedIndices.Clear();
                    _selectedIndices.Add(idx);
                    _selectedIndex = idx;
                    Invalidate();
                }
                _lastSelectedIndex = idx;

                if (toggleRect.Contains(e.Location))
                {
                    if (_selectedIndices.Count > 1 && _selectedIndices.Contains(idx))
                    {
                        bool newState = !_items[idx].Enabled;
                        foreach (var s in _selectedIndices.ToList())
                        {
                            bool old = _items[s].Enabled;
                            _items[s].Enabled = newState;
                            _toggleStart[s] = old ? 1 : 0;
                            _toggleTarget[s] = newState ? 1 : 0;
                            _toggleProgress[s] = 0f;
                            ToggleChanged?.Invoke(s, newState);
                        }
                        _animation.Start();
                        if (!_animTimer.Enabled)
                            _animTimer.Start();
                        Invalidate();
                        return;
                    }
                    else
                    {
                        bool newState = !_items[idx].Enabled;
                        int start = _items[idx].Enabled ? 1 : 0;
                        int target = newState ? 1 : 0;
                        _items[idx].Enabled = newState;
                        _toggleStart[idx] = start;
                        _toggleTarget[idx] = target;
                        _toggleProgress[idx] = 0f;
                        _animation.Start();
                        if (!_animTimer.Enabled)
                            _animTimer.Start();
                        ToggleChanged?.Invoke(idx, _items[idx].Enabled);
                        Invalidate();
                        return;
                    }
                }
                if (warningRect.Contains(e.Location))
                {
                    if (_items[idx].HasConflict)
                    {
                        var menu = new ContextMenuStrip
                        {
                            Renderer = new CustomContextMenuStrip(),
                            ShowImageMargin = false,
                            ShowCheckMargin = false
                        };
                        foreach (var kv in _items[idx].Conflicts)
                        {
                            var modItem = new NoMarginToolStripMenuItem(kv.Key);
                            foreach (var fn in kv.Value)
                            {
                                var fItem = new NoMarginToolStripMenuItem(fn) { Enabled = false };
                                modItem.DropDownItems.Add(fItem);
                            }
                            menu.Items.Add(modItem);
                        }
                        menu.Show(this, e.Location);
                    }
                    return;
                }
                if (dotsRect.Contains(e.Location))
                {
                    ItemRightClicked?.Invoke(idx);
                    return;
                }
                _dragIndex = idx;
                _dragStartY = e.Y;
                _isDragging = false;
                _dragMouseY = e.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!_isDragging && _dragIndex >= 0)
            {
                int idx = IndexFromPoint(e.Location);
                if (idx == _dragIndex)
                {
                    _selectedIndex = _dragIndex;
                    if (e.Button == MouseButtons.Right)
                        ItemRightClicked?.Invoke(_selectedIndex);
                    else if (e.Clicks == 2 && !_isDragging)
                        ItemDoubleClicked?.Invoke(_selectedIndex);
                    Invalidate();
                }
            }
            if (_isDragging)
            {
                if (_dragIndex >= 0 && _dragTarget >= 0 && _dragIndex != _dragTarget)
                {
                    if (_selectedIndices.Count > 1 && _selectedIndices.Contains(_dragIndex))
                    {
                        var sel = _selectedIndices.OrderBy(x => x).ToList();
                        var itemsToMove = sel.Select(i => _items[i]).ToList();
                        _suppressListAnimation = true;
                        _items.RaiseListChangedEvents = false;
                        for (int i = sel.Count - 1; i >= 0; i--)
                            _items.RemoveAt(sel[i]);
                        int removedBeforeTarget = sel.Count(s => s < _dragTarget);
                        int insertAt = _dragTarget - removedBeforeTarget;
                        if (insertAt < 0)
                            insertAt = 0;
                        if (insertAt > _items.Count)
                            insertAt = _items.Count;
                        for (int i = 0; i < itemsToMove.Count; i++)
                            _items.Insert(insertAt + i, itemsToMove[i]);
                        _items.RaiseListChangedEvents = true;
                        _items.ResetBindings();
                        UpdateScroll();
                        Sync_animation();
                        foreach (var ai in _animation.AnimatedItems)
                            ai.CurrentY = ai.TargetY;
                        _animTimer?.Stop();
                        Invalidate();
                        _suppressListAnimation = false;
                        _selectedIndices.Clear();
                        for (int i = 0; i < itemsToMove.Count; i++)
                            _selectedIndices.Add(insertAt + i);
                        ItemsReordered?.Invoke();
                    }
                    else
                    {
                        var item = _items[_dragIndex];
                        _suppressListAnimation = true;
                        _items.RaiseListChangedEvents = false;
                        _items.RemoveAt(_dragIndex);

                        int insertAt = _dragTarget;

                        if (_dragIndex < _dragTarget)
                            insertAt--;

                        if (insertAt < 0)
                            insertAt = 0;

                        if (insertAt > _items.Count)
                            insertAt = _items.Count;

                        _items.Insert(insertAt, item);

                        _items.RaiseListChangedEvents = true;
                        _items.ResetBindings();
                        UpdateScroll();
                        Sync_animation();
                        foreach (var ai in _animation.AnimatedItems)
                            ai.CurrentY = ai.TargetY;
                        _animTimer?.Stop();
                        Invalidate();
                        _suppressListAnimation = false;

                        _selectedIndices.Clear();
                        _selectedIndices.Add(insertAt);
                        _selectedIndex = insertAt;
                        _lastSelectedIndex = insertAt;

                        ItemsReordered?.Invoke();
                    }
                }
                _rowOffsetsTarget.Clear();
                _rowOffsets.Clear();
                _lastDragTarget = -1;
            }
            _isDragging = false;
            _dragIndex = -1;
            _dragTarget = -1;
            _hoverIndex = -1;
            _autoScrollDir = 0;
            _autoScrollTimer.Stop();
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Space)
                return true;
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                _selectedIndices.Clear();
                _selectedIndex = -1;
                Invalidate();
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                if (_selectedIndices.Count > 0)
                {
                    var first = _selectedIndices.First();
                    bool newState = !_items[first].Enabled;
                    foreach (var s in _selectedIndices.ToList())
                    {
                        bool old = _items[s].Enabled;
                        _items[s].Enabled = newState;
                        _toggleStart[s] = old ? 1 : 0;
                        _toggleTarget[s] = newState ? 1 : 0;
                        _toggleProgress[s] = 0f;
                        ToggleChanged?.Invoke(s, newState);
                    }
                    _animation.Start();
                    if (!_animTimer.Enabled)
                        _animTimer.Start();
                    Invalidate();
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!_isDragging)
                _hoverIndex = -1;

            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int delta = e.Delta > 0 ? -3 : 3;
            int newVal = Math.Max(
                _vScroll.Minimum,
                Math.Min(_vScroll.Maximum, _vScroll.Value + delta)
            );
            _vScroll.Value = newVal;
            Invalidate(false);
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "ModPath")
                {
                    UpdateEmptyMessage();
                    UpdateScroll();

                    if (_items != null && _items.Count > 0)
                    {
                        BuildAnimatedItems(false);
                    }

                    Invalidate();
                }
            }
            catch { }
        }

        private int IndexFromPoint(Point p)
        {
            if (_items == null || _items.Count == 0)
                return -1;

            if (p.X > Width - _vScroll.Width)
                return -1;

            int row = p.Y / _rowHeight;

            int idx = row + _vScroll.Value;

            if (idx < 0 || idx >= _items.Count)
                return -1;

            return idx;
        }

        private void UpdateOffsetsForDrag()
        {
            if (!_isDragging)
                return;
            int src = _dragIndex;
            int dst = _dragTarget;
            if (src < 0 || dst < 0)
                return;
            _rowOffsetsTarget.Clear();
            int count = _items?.Count ?? 0;
            if (dst > src)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i > src && i <= dst)
                        _rowOffsetsTarget[i] = -_rowHeight;
                    else
                        _rowOffsetsTarget[i] = 0f;
                }
            }
            else if (dst < src)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i >= dst && i < src)
                        _rowOffsetsTarget[i] = _rowHeight;
                    else
                        _rowOffsetsTarget[i] = 0f;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                    _rowOffsetsTarget[i] = 0f;
            }
            for (int i = 0; i < count; i++)
                if (!_rowOffsets.ContainsKey(i))
                    _rowOffsets[i] = 0f;
        }

        private void UpdateEmptyMessage()
        {
            try
            {
                string modPath = Properties.Settings.Default.ModPath;

                if (string.IsNullOrWhiteSpace(modPath))
                {
                    _emptyMessage = "Please select a mod path in the settings.";
                }
                else
                {
                    int count = _items?.Count ?? 0;
                    if (count == 0)
                        _emptyMessage = $"No mods found in {modPath}";
                    else
                        _emptyMessage = null;
                }

                if (!string.IsNullOrEmpty(_emptyMessage))
                {
                    if (!_emptyTimer.Enabled)
                        _emptyTimer.Start();
                }
                else
                {
                    if (_emptyTimer.Enabled)
                        _emptyTimer.Stop();
                }
            }
            catch
            {
                _emptyMessage = null;
                if (_emptyTimer.Enabled)
                    _emptyTimer.Stop();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cachedBrush?.Dispose();
                _cachedTogglePath?.Dispose();
                _animation?.Dispose();
                _autoScrollTimer?.Dispose();
                _animTimer?.Dispose();
                _tooltip?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
