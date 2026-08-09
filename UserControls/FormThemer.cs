using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;
using QuillsModManagerV2.Util.Controls;
using QuillsModManagerV2.Util.Controls.AnimatedList;

namespace QuillsModManagerV2.UserControls
{
    public partial class FormThemer : Form
    {
        private class ThemePreset
        {
            public string Name { get; set; }
            public Dictionary<string, Color> Colors { get; set; }
            public bool IsProtected { get; set; }
        }

        private Panel _colorsInnerPanel;
        private AnimatedListController _colorAnimation;

        private List<ThemePreset> _builtInPresets = null;
        private List<ThemePreset> _userPresets = new List<ThemePreset>();
        private Panel _presetsInnerPanel;
        private AnimatedListController _presetsAnimation;

        public FormThemer()
        {
            InitializeComponent();
            InitializeSettingsAndEvents();
            UpdateTheme.RegisterIcons(this);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Point screen = Cursor.Position;

            try
            {
                var colorsRect = PanelColorsContent.RectangleToScreen(
                    PanelColorsContent.ClientRectangle
                );
                var presetsRect = PanelPresetsContent.RectangleToScreen(
                    PanelPresetsContent.ClientRectangle
                );

                int delta = e.Delta > 0 ? -12 : 12;

                if (colorsRect.Contains(screen))
                {
                    int newVal = Math.Max(
                        vScrollColors.Minimum,
                        Math.Min(vScrollColors.Maximum, vScrollColors.Value + delta)
                    );
                    vScrollColors.Value = newVal;
                    Invalidate(false);
                    return;
                }

                if (presetsRect.Contains(screen))
                {
                    int newVal = Math.Max(
                        vScrollPresets.Minimum,
                        Math.Min(vScrollPresets.Maximum, vScrollPresets.Value + delta)
                    );
                    vScrollPresets.Value = newVal;
                    Invalidate(false);
                    return;
                }
            }
            catch
            {
                base.OnMouseWheel(e);
            }
        }

        #region Form base
        private void InitializeSettingsAndEvents()
        {
            SettingsChangedEvent(this, null);
            InitializeBuiltInPresets();
            PopulateColorSwatches();
            LoadUserPresets();
            PopulatePresets();

            Settings.Default.SettingsLoaded += SettingsChangedEvent;

            try
            {
                BtnAddPreset.Click += (s, e) => BtnAddPreset_Click();
            }
            catch { }
        }

        private void InitializeBuiltInPresets()
        {
            if (_builtInPresets != null)
                return;
            _builtInPresets = new List<ThemePreset>();

            void add(
                string name,
                string p,
                string s,
                string t,
                string d,
                string da,
                string b,
                string text,
                string placeholder
            )
            {
                var preset = new ThemePreset
                {
                    Name = name,
                    IsProtected = true,
                    Colors = new Dictionary<string, Color>
                    {
                        { "BGPrimary", ColorTranslator.FromHtml(p) },
                        { "BGSecondary", ColorTranslator.FromHtml(s) },
                        { "BGTertiary", ColorTranslator.FromHtml(t) },
                        { "DetailColor", ColorTranslator.FromHtml(d) },
                        { "DetailActive", ColorTranslator.FromHtml(da) },
                        { "ButtonColor", ColorTranslator.FromHtml(b) },
                        { "TextColor", ColorTranslator.FromHtml(text) },
                        { "PlaceholderColor", ColorTranslator.FromHtml(placeholder) }
                    }
                };
                _builtInPresets.Add(preset);
            }

            add(
                "Default",
                "#101014",
                "#14141A",
                "#19191F",
                "#3C3C42",
                "#6E6AF6",
                "#101014",
                "White",
                "#C1C8CF"
            );
            add(
                "Default Light",
                "#F5F5F7",
                "#FFFFFF",
                "#EBEBEF",
                "#D1D1D6",
                "#3B82F6",
                "#FFFFFF",
                "#1C1C1E",
                "#8E8E93"
            );
            add(
                "Simple Dark",
                "#1E1E1E",
                "#252526",
                "#2D2D30",
                "#3F3F46",
                "#3B82F6",
                "#2D2D30",
                "#F3F3F3",
                "#9CA3AF"
            );
            add(
                "Simple Light",
                "#FAFBFC",
                "#FFFFFF",
                "#F6F8FA",
                "#D8DEE4",
                "#3B82F6",
                "#F6F8FA",
                "#24292F",
                "#6E7781"
            );
            add(
                "Cosmos",
                "#16141C",
                "#1A1926",
                "#292246",
                "#C4B7FF",
                "#8D50EB",
                "#16141C",
                "#F5F4F2",
                "#C4B7FF"
            );
            add(
                "Rosé Pine",
                "#191724",
                "#1F1D2E",
                "#26233A",
                "#C4A7E7",
                "#9CCFD8",
                "#191724",
                "#E0DEF4",
                "#6E6A86"
            );
            add(
                "Rosé Pine Moon",
                "#232136",
                "#2A273F",
                "#393552",
                "#C4A7E7",
                "#E29492",
                "#232136",
                "#E0DEF4",
                "#6E6A86"
            );
            add(
                "Rosé Pine Dawn",
                "#FAF4ED",
                "#E1DCD5",
                "#D4CBC3",
                "#907AA9",
                "#56949F",
                "#FAF4ED",
                "#575279",
                "#9893A5"
            );
        }

        private string GetPresetFilePath()
        {
            string dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "QuillsModManagerV2"
            );
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, "theme_presets.json");
        }

        private void LoadUserPresets()
        {
            try
            {
                string path = GetPresetFilePath();
                if (!File.Exists(path))
                    return;
                var txt = File.ReadAllText(path);
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ThemePreset>>(txt);
                if (list != null)
                    _userPresets = list;
            }
            catch { }
        }

        private void SaveUserPresets()
        {
            try
            {
                string path = GetPresetFilePath();
                File.WriteAllText(
                    path,
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        _userPresets,
                        Newtonsoft.Json.Formatting.Indented
                    )
                );
            }
            catch { }
        }

        private void PopulateColorSwatches()
        {
            try
            {
                PanelColorsContent.SuspendLayout();

                if (_colorsInnerPanel == null || _colorsInnerPanel.IsDisposed)
                {
                    _colorsInnerPanel = new Panel
                    {
                        Left = 0,
                        Top = 0,
                        Width = PanelColorsContent.Width,
                        Height = PanelColorsContent.Height,
                        BackColor = Settings.Default.BGPrimary,
                        AutoSize = false
                    };
                    PanelColorsContent.Controls.Add(_colorsInnerPanel);

                    vScrollColors.Scroll -= (s, e) =>
                    {
                        _colorsInnerPanel.Top = -vScrollColors.Value;
                        _colorsInnerPanel.Invalidate();
                    };
                    vScrollColors.Scroll += (s, e) =>
                    {
                        _colorsInnerPanel.Top = -vScrollColors.Value;
                        _colorsInnerPanel.Invalidate();
                    };

                    PanelColorsContent.Resize -= PanelColorsContent_Resize;
                    PanelColorsContent.Resize += PanelColorsContent_Resize;

                    if (_colorAnimation == null)
                    {
                        _colorAnimation = new AnimatedListController();
                        _colorAnimation.AnimationUpdated += () =>
                        {
                            _colorsInnerPanel?.Invalidate();
                        };
                    }
                }

                _colorsInnerPanel.Controls.Clear();

                var borderPanel = new AnimatedPanel
                {
                    Left = 0,
                    Top = 0,
                    Width = _colorsInnerPanel.Width,
                    Height = 48,
                    BackColor = Settings.Default.BGSecondary
                };
                var borderLabel = new Label
                {
                    Left = 8,
                    Top = 6,
                    Width = borderPanel.Width - 96,
                    Height = 20,
                    Text = "Border Radius",
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = Settings.Default.TextColor,
                    BackColor = Color.Transparent
                };
                var borderSlider = new CustomHScrollBar
                {
                    Left = 8,
                    Top = 28,
                    Width = borderPanel.Width - 30,
                    Height = 16
                };
                try
                {
                    borderSlider.Minimum = 0;
                    borderSlider.Maximum = 15;
                    borderSlider.Value = Math.Max(0, Math.Min(15, Settings.Default.BorderRadius));
                    borderSlider.SmallChange = 1;
                    borderSlider.LargeChange = 1;
                }
                catch { }
                borderSlider.Scroll += (s, e) =>
                {
                    try
                    {
                        if (s is CustomHScrollBar sb)
                        {
                            Settings.Default.BorderRadius = sb.Value;
                            FormThemerEProperties.BorderRadius = sb.Value * 3;
                            Settings.Default.Save();
                            Settings.Default.Reload();
                        }
                        try
                        {
                            this.BeginInvoke(new Action(() => RefreshDynamicTheme()));
                        }
                        catch
                        {
                            RefreshDynamicTheme();
                        }
                        try
                        {
                            var open = FormUtil.GetAllOpenForms();
                            foreach (var f in open)
                            {
                                try
                                {
                                    if (f is FormSettings fs)
                                    {
                                        try
                                        {
                                            fs.BeginInvoke(
                                                new Action(() => fs.RefreshDynamicTheme())
                                            );
                                        }
                                        catch
                                        {
                                            fs.RefreshDynamicTheme();
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                    catch { }
                };
                borderPanel.Controls.Add(borderLabel);
                borderPanel.Controls.Add(borderSlider);
                _colorsInnerPanel.Controls.Add(borderPanel);

                int y = borderPanel.Height + 6;

                var colorKeys = new (string key, string title)[]
                {
                    ("BGPrimary", "Primary Color"),
                    ("BGSecondary", "Secondary Color"),
                    ("BGTertiary", "Tertiary Color"),
                    ("DetailColor", "Detail Color"),
                    ("DetailActive", "Active Color"),
                    ("ButtonColor", "Button Color"),
                    ("TextColor", "Text Color"),
                    ("PlaceholderColor", "Placeholder Color")
                };

                for (int i = 0; i < colorKeys.Length; i++)
                {
                    var ck = colorKeys[i];
                    var panel = new AnimatedPanel
                    {
                        Left = 0,
                        Top = y,
                        Width = _colorsInnerPanel.Width,
                        Height = 36,
                        BackColor = Settings.Default.BGSecondary
                    };

                    var lbl = new Label
                    {
                        Left = 8,
                        Top = 0,
                        Width = panel.Width - 96,
                        Height = 36,
                        Text = ck.title,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Settings.Default.TextColor,
                        BackColor = Color.Transparent
                    };
                    var sw = new Guna2Button
                    {
                        Left = panel.Width - 82,
                        Top = 6,
                        Width = 60,
                        Height = 24,
                        PressedColor = Settings.Default.DetailActive,
                        Cursor = Cursors.Hand,
                        Animated = true,
                        FillColor = Settings.Default.ButtonColor,
                        BorderRadius = Settings.Default.BorderRadius,
                        BackColor = Settings.Default.BGPrimary
                    };
                    Color col =
                        Settings.Default.GetType().GetProperty(ck.key).GetValue(Settings.Default)
                            as Color?
                        ?? Color.Black;
                    sw.FillColor = col;
                    sw.Tag = ck.key;

                    sw.BorderColor = Settings.Default.DetailColor;
                    sw.BorderThickness = 1;
                    sw.CustomBorderColor = Settings.Default.DetailColor;
                    sw.HoverState.BorderColor = Settings.Default.DetailActive;

                    sw.Click += (s, e) =>
                    {
                        try
                        {
                            var selector = new infoforms.UserInput.FormColorSelector
                            {
                                StartPosition = FormStartPosition.Manual,
                                Location = Cursor.Position
                            };
                            selector.UpdateUIFromColor(col);
                            selector.ColorChanged += (c) =>
                            {
                                try
                                {
                                    var prop = Settings.Default.GetType().GetProperty(ck.key);
                                    prop?.SetValue(Settings.Default, c);
                                    sw.FillColor = c;
                                    try
                                    {
                                        this.BeginInvoke(new Action(() => RefreshDynamicTheme()));
                                    }
                                    catch
                                    {
                                        RefreshDynamicTheme();
                                    }
                                    try
                                    {
                                        var open = FormUtil.GetAllOpenForms();
                                        foreach (var f in open)
                                        {
                                            try
                                            {
                                                if (f is FormSettings fs)
                                                {
                                                    try
                                                    {
                                                        fs.BeginInvoke(
                                                            new Action(
                                                                () => fs.RefreshDynamicTheme()
                                                            )
                                                        );
                                                    }
                                                    catch
                                                    {
                                                        fs.RefreshDynamicTheme();
                                                    }
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    catch { }
                                }
                                catch { }
                            };
                            FormUtil.ShowModalForm(this, selector, false);
                        }
                        catch { }
                    };

                    panel.Controls.Add(lbl);
                    panel.Controls.Add(sw);
                    _colorsInnerPanel.Controls.Add(panel);

                    if (_colorAnimation != null)
                    {
                        var item = new AnimatedItem
                        {
                            Key = ck.key,
                            Panel = panel,
                            CurrentY = i * 36 + 55,
                            TargetY = i * 36,
                            Opacity = 0f,
                            OffsetY = 8f,
                            AnimationStart = _colorAnimation.AnimationClock.ElapsedMilliseconds,
                            Delay = i * 25
                        };
                        _colorAnimation.RegisterItem(item);
                    }

                    y += panel.Height + 6;
                }

                _colorAnimation?.Start();

                _colorsInnerPanel.Height = Math.Max(PanelColorsContent.Height, y);
                UpdateColorScroll();

                PanelColorsContent.ResumeLayout();
            }
            catch { }
        }

        private void UpdateColorScroll()
        {
            if (_colorsInnerPanel == null)
                return;
            int total = _colorsInnerPanel.Height;
            int view = PanelColorsContent.Height;

            vScrollColors.Minimum = 0;
            vScrollColors.Maximum = Math.Max(0, total - view);
            vScrollColors.LargeChange = Math.Max(1, view);
            vScrollColors.Value = Math.Max(0, Math.Min(vScrollColors.Value, vScrollColors.Maximum));

            _colorsInnerPanel.Top = -vScrollColors.Value;
            vScrollColors.Enabled = total > view;
        }

        private void PanelColorsContent_Resize(object sender, EventArgs e)
        {
            if (_colorsInnerPanel == null)
                return;
            _colorsInnerPanel.Width = PanelColorsContent.Width;
            UpdateColorScroll();
        }

        private void UpdatePresetsScroll()
        {
            if (_presetsInnerPanel == null)
                return;
            int total = _presetsInnerPanel.Height;
            int view = PanelPresetsContent.Height;

            vScrollPresets.Minimum = 0;
            vScrollPresets.Maximum = Math.Max(0, total - view);
            vScrollPresets.LargeChange = Math.Max(1, view);
            vScrollPresets.Value = Math.Max(
                0,
                Math.Min(vScrollPresets.Value, vScrollPresets.Maximum)
            );

            _presetsInnerPanel.Top = -vScrollPresets.Value;
            vScrollPresets.Enabled = total > view;
        }

        private void PanelPresetsContent_Resize(object sender, EventArgs e)
        {
            if (_presetsInnerPanel == null)
                return;
            _presetsInnerPanel.Width = PanelPresetsContent.Width;
            UpdatePresetsScroll();
        }

        private void RefreshDynamicTheme()
        {
            try
            {
                try
                {
                    if (_colorsInnerPanel != null)
                        _colorsInnerPanel.BackColor = Settings.Default.BGPrimary;
                }
                catch { }
                try
                {
                    if (_presetsInnerPanel != null)
                        _presetsInnerPanel.BackColor = Settings.Default.BGPrimary;
                }
                catch { }
                if (_colorsInnerPanel != null)
                {
                    foreach (Control c in _colorsInnerPanel.Controls)
                    {
                        if (c is Panel p)
                        {
                            p.BackColor = Settings.Default.BGSecondary;
                            foreach (Control child in p.Controls)
                            {
                                if (child is Label l)
                                    l.ForeColor = Settings.Default.TextColor;
                                if (child is Guna2Button b)
                                {
                                    b.BorderColor = Settings.Default.DetailColor;
                                    b.CustomBorderColor = Settings.Default.DetailColor;
                                    b.FillColor = (Color)
                                        Settings
                                            .Default.GetType()
                                            .GetProperty((string)b.Tag ?? "ButtonColor")
                                            .GetValue(Settings.Default);
                                }
                            }
                        }
                    }
                }

                if (_presetsInnerPanel != null)
                {
                    foreach (Control c in _presetsInnerPanel.Controls)
                    {
                        if (c is Panel p)
                        {
                            p.BackColor = Settings.Default.BGSecondary;
                            foreach (Control child in p.Controls)
                            {
                                if (child is Label l)
                                    l.ForeColor = Settings.Default.TextColor;
                                if (child is Panel pv)
                                    pv.BackColor = Settings.Default.ButtonColor;
                                if (child is Guna2Button b)
                                {
                                    b.BorderColor = Settings.Default.DetailColor;
                                    b.CustomBorderColor = Settings.Default.DetailColor;
                                }
                                if (child is Label ic)
                                    ic.ForeColor = Settings.Default.TextColor;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void PopulatePresets()
        {
            try
            {
                PanelPresetsContent.SuspendLayout();

                if (_presetsInnerPanel == null || _presetsInnerPanel.IsDisposed)
                {
                    _presetsInnerPanel = new Panel
                    {
                        Left = 0,
                        Top = 0,
                        Width = PanelPresetsContent.Width,
                        Height = PanelPresetsContent.Height,
                        BackColor = Settings.Default.BGPrimary,
                        AutoSize = false
                    };
                    PanelPresetsContent.Controls.Add(_presetsInnerPanel);

                    vScrollPresets.Scroll -= (s, e) =>
                    {
                        _presetsInnerPanel.Top = -vScrollPresets.Value;
                        _presetsInnerPanel.Invalidate();
                    };
                    vScrollPresets.Scroll += (s, e) =>
                    {
                        _presetsInnerPanel.Top = -vScrollPresets.Value;
                        _presetsInnerPanel.Invalidate();
                    };

                    PanelPresetsContent.Resize -= PanelPresetsContent_Resize;
                    PanelPresetsContent.Resize += PanelPresetsContent_Resize;

                    if (_presetsAnimation == null)
                    {
                        _presetsAnimation = new AnimatedListController();
                        _presetsAnimation.AnimationUpdated += () =>
                        {
                            _presetsInnerPanel?.Invalidate();
                        };
                    }
                }

                _presetsInnerPanel.Controls.Clear();

                var all = new List<ThemePreset>();
                if (_builtInPresets != null)
                    all.AddRange(_builtInPresets);
                if (_userPresets != null)
                    all.AddRange(_userPresets);

                int y = 8;

                for (int i = 0; i < all.Count; i++)
                {
                    var preset = all[i];
                    var panel = new AnimatedPanel
                    {
                        Left = 0,
                        Top = y,
                        Width = _presetsInnerPanel.Width,
                        Height = 28,
                        BackColor = Settings.Default.BGSecondary,
                        Tag = preset
                    };

                    var lbl = new Label
                    {
                        Left = 8,
                        Top = 0,
                        Width = panel.Width - 100,
                        Height = 28,
                        Text = preset.Name,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ForeColor = Settings.Default.TextColor,
                        BackColor = Color.Transparent
                    };
                    var preview = new Panel
                    {
                        Left = panel.Width - 112,
                        Top = 6,
                        Width = 20,
                        Height = 16,
                        BackColor = Settings.Default.ButtonColor
                    };

                    var renameLbl = new Label
                    {
                        Left = panel.Width - 84,
                        Top = 4,
                        Width = 20,
                        Height = 20,
                        Text = "✎",
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Settings.Default.TextColor,
                        Cursor = Cursors.Hand,
                        BackColor = Color.Transparent
                    };
                    var delLbl = new Label
                    {
                        Left = panel.Width - 56,
                        Top = 4,
                        Width = 20,
                        Height = 20,
                        Text = "🗑",
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Settings.Default.TextColor,
                        Cursor = Cursors.Hand,
                        BackColor = Color.Transparent,
                        Visible = !preset.IsProtected
                    };

                    renameLbl.Visible = !preset.IsProtected;

                    renameLbl.Click += (s, e) =>
                    {
                        try
                        {
                            if (preset.IsProtected)
                                return;
                            var inputDialog = new FormUserInputDialog("Rename Preset", preset.Name);
                            inputDialog.FormClosed += (s2, e2) =>
                            {
                                try
                                {
                                    if (!inputDialog.Confirmed)
                                    {
                                        try
                                        {
                                            inputDialog.Dispose();
                                        }
                                        catch { }
                                        return;
                                    }
                                    string newName = inputDialog.TxtUserInput.Text?.Trim();
                                    if (
                                        !string.IsNullOrWhiteSpace(newName)
                                        && newName != preset.Name
                                    )
                                    {
                                        preset.Name = newName;
                                        SaveUserPresets();
                                        PopulatePresets();
                                    }
                                }
                                catch { }
                                finally
                                {
                                    try
                                    {
                                        inputDialog.Dispose();
                                    }
                                    catch { }
                                }
                            };
                            FormUtil.ShowModalForm(this, inputDialog, false);
                        }
                        catch { }
                    };

                    delLbl.Click += (s, e) =>
                    {
                        try
                        {
                            if (preset.IsProtected)
                                return;
                            if (
                                MessageBoxUtil.Show(
                                    $"Delete Preset",
                                    "Delete preset '{preset.Name}'?",
                                    true
                                ) != DialogResult.Yes
                            )
                                return;
                            _userPresets.RemoveAll(x => x.Name == preset.Name);
                            SaveUserPresets();
                            PopulatePresets();
                        }
                        catch { }
                    };

                    panel.Click += (s, e) =>
                    {
                        ApplyPreset(preset);
                    };
                    lbl.Click += (s, e) =>
                    {
                        ApplyPreset(preset);
                    };
                    preview.Click += (s, e) =>
                    {
                        ApplyPreset(preset);
                    };

                    panel.Controls.Add(lbl);
                    panel.Controls.Add(preview);
                    panel.Controls.Add(renameLbl);
                    panel.Controls.Add(delLbl);
                    _presetsInnerPanel.Controls.Add(panel);

                    if (_presetsAnimation != null)
                    {
                        var item = new AnimatedItem
                        {
                            Key = preset.Name,
                            Panel = panel,
                            CurrentY = i * 34 + 55,
                            TargetY = i * 34 + 8,
                            Opacity = 0f,
                            OffsetY = 8f,
                            AnimationStart = _presetsAnimation.AnimationClock.ElapsedMilliseconds,
                            Delay = i * 25
                        };
                        _presetsAnimation.RegisterItem(item);
                    }

                    y += panel.Height + 6;
                }

                _presetsAnimation?.Start();

                _presetsInnerPanel.Height = Math.Max(PanelPresetsContent.Height, y);
                UpdatePresetsScroll();

                PanelPresetsContent.ResumeLayout();
            }
            catch { }
        }

        private void ApplyPreset(ThemePreset p)
        {
            try
            {
                foreach (var kv in p.Colors)
                {
                    var prop = Settings.Default.GetType().GetProperty(kv.Key);
                    prop?.SetValue(Settings.Default, kv.Value);
                }
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        Settings.Default.Save();
                    }
                    catch { }
                    try
                    {
                        Settings.Default.Reload();
                    }
                    catch { }
                });

                try
                {
                    this.BeginInvoke(new Action(() => RefreshDynamicTheme()));
                }
                catch
                {
                    RefreshDynamicTheme();
                }
                PopulateColorSwatches();
                Refresh();
                try
                {
                    var open = FormUtil.GetAllOpenForms();
                    foreach (var f in open)
                    {
                        try
                        {
                            if (f is FormSettings fs)
                            {
                                try
                                {
                                    fs.BeginInvoke(new Action(() => fs.RefreshDynamicTheme()));
                                }
                                catch
                                {
                                    fs.RefreshDynamicTheme();
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
            catch { }
        }

        private void BtnAddPreset_Click()
        {
            try
            {
                var inputDialog = new FormUserInputDialog("Preset name", "");
                inputDialog.FormClosed += (s, e) =>
                {
                    try
                    {
                        if (!inputDialog.Confirmed)
                        {
                            try
                            {
                                inputDialog.Dispose();
                            }
                            catch { }
                            return;
                        }
                        string name = inputDialog.TxtUserInput.Text?.Trim();
                        if (string.IsNullOrWhiteSpace(name))
                            name = "Custom " + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                        var preset = new ThemePreset
                        {
                            Name = name,
                            IsProtected = false,
                            Colors = new Dictionary<string, Color>()
                        };
                        var keys = new[]
                        {
                            "BGPrimary",
                            "BGSecondary",
                            "BGTertiary",
                            "DetailColor",
                            "DetailActive",
                            "ButtonColor",
                            "TextColor",
                            "PlaceholderColor"
                        };
                        foreach (var k in keys)
                        {
                            var prop = Settings.Default.GetType().GetProperty(k);
                            if (prop != null)
                                preset.Colors[k] = (Color)prop.GetValue(Settings.Default);
                        }
                        _userPresets.Add(preset);
                        SaveUserPresets();
                        PopulatePresets();
                    }
                    catch { }
                    finally
                    {
                        try
                        {
                            inputDialog.Dispose();
                        }
                        catch { }
                    }
                };

                FormUtil.ShowModalForm(this, inputDialog, false);
            }
            catch { }
        }

        private class ToggleRow : Control
        {
            private string _settingKey;
            private string _title;
            private float _progress = 0f;
            private float _target = 0f;
            private Timer _timer;

            private Label _label;

            public event EventHandler CheckedChanged;

            public ToggleRow(string settingKey, string title)
            {
                _settingKey = settingKey;
                _title = title;
                DoubleBuffered = true;
                Height = 28;

                _label = new Label
                {
                    Left = 6,
                    Top = 0,
                    Width = 200,
                    Height = Height,
                    Text = _title,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent,
                    ForeColor = Settings.Default.TextColor
                };
                Controls.Add(_label);

                bool cur = GetSettingValue();
                _progress = cur ? 1f : 0f;
                _target = _progress;

                _timer = new Timer { Interval = 16 };
                _timer.Tick += (s, e) =>
                {
                    const float SPEED = 0.12f;
                    _progress += (_target - _progress) * SPEED;

                    if (
                        Math.Abs(_target - _progress) < 0.02f
                        || (_target > _progress && _progress > 0.95f)
                    )
                    {
                        _progress = _target;
                        _timer.Stop();
                    }

                    Invalidate();
                };

                this.MouseDown += (s, e) => Toggle();
                _label.MouseDown += (s, e) => Toggle();
                this.Resize += (s, e) =>
                {
                    _label.Width = Math.Max(50, Width - 90);
                };
            }

            private bool GetSettingValue()
            {
                try
                {
                    var pi = Settings.Default.GetType().GetProperty(_settingKey);
                    if (pi != null)
                        return (bool)pi.GetValue(Settings.Default);
                }
                catch { }
                return false;
            }

            private void SetSettingValue(bool v)
            {
                try
                {
                    var pi = Settings.Default.GetType().GetProperty(_settingKey);
                    pi?.SetValue(Settings.Default, v);
                }
                catch { }
            }

            private void Toggle()
            {
                bool newVal = !GetSettingValue();
                SetSettingValue(newVal);
                _target = newVal ? 1f : 0f;
                if (!_timer.Enabled)
                    _timer.Start();
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Color offBase = Settings.Default.BGPrimary;
                Color offColor = Color.FromArgb(160, offBase.R, offBase.G, offBase.B);
                Color onColor = Settings.Default.DetailActive;
                Color bg = InterpolateColor(offColor, onColor, _progress);

                int trackW = 34;
                int trackH = 22;
                int trackX = Width - trackW - 8;
                int trackY = (Height - trackH) / 2;
                Rectangle trackRect = new Rectangle(trackX, trackY, trackW, trackH);

                using (var path = RoundedRect(trackRect, trackH / 2))
                using (var brush = new SolidBrush(bg))
                {
                    e.Graphics.FillPath(brush, path);
                }

                int pad = 3;
                int knobSize = trackH - pad * 2 - 2;
                int knobRange = trackW - pad * 2 - knobSize;
                int knobX = trackX + pad + (int)(knobRange * _progress);
                Rectangle knobRect = new Rectangle(knobX, trackY + pad + 1, knobSize, knobSize);
                using (var kb = new SolidBrush(Settings.Default.TextColor))
                {
                    e.Graphics.FillEllipse(kb, knobRect);
                }

                using (var pen = new Pen(Settings.Default.DetailColor))
                {
                    e.Graphics.DrawPath(pen, RoundedRect(trackRect, trackH / 2));
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
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            Settings.Default.Reload();
            FormUtil.Close(this);
        }

        private void SettingsChangedEvent(
            object sender,
            System.Configuration.SettingsLoadedEventArgs e
        )
        {
            var controlsToModify = new Control[] { BtnAddPreset };
            UpdateTheme.Refresh(this, controlsToModify);
            FormThemerEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormThemerEProperties.ShadowColor = Color.Black;

            if (Settings.Default.BorderRadius == 0)
                BtnClose.Width = 36;
            else
                BtnClose.Width = 37;

            if (WindowState != FormWindowState.Maximized)
                FormThemerEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
        }
        #endregion

        private void BtnResetTheme_Click(object sender, EventArgs e)
        {
            const string MsgBoxMessage =
                "Are you sure you want to reset the theme?\nThis cannot be reversed!";
            DialogResult dgresult = MessageBoxUtil.Show("Reset Theme", MsgBoxMessage, true);
            if (dgresult == DialogResult.Yes)
                ResetTheme();
        }

        private void SetThemeSettings(
            string primary,
            string secondary,
            string tertiary,
            string detail,
            string detailActive,
            string button,
            string text,
            string placeholder
        )
        {
            Settings.Default.BGPrimary = ColorTranslator.FromHtml(primary);
            Settings.Default.BGSecondary = ColorTranslator.FromHtml(secondary);
            Settings.Default.BGTertiary = ColorTranslator.FromHtml(tertiary);
            Settings.Default.DetailColor = ColorTranslator.FromHtml(detail);
            Settings.Default.DetailActive = ColorTranslator.FromHtml(detailActive);
            Settings.Default.ButtonColor = ColorTranslator.FromHtml(button);
            Settings.Default.TextColor = ColorTranslator.FromHtml(text);
            Settings.Default.PlaceholderColor = ColorTranslator.FromHtml(placeholder);
        }

        private void ResetTheme()
        {
            try
            {
                SetThemeSettings(
                    "#101014",
                    "#14141A",
                    "#19191F",
                    "#3C3C42",
                    "#6E6AF6",
                    "#101014",
                    "White",
                    "#C1C8CF"
                );
                Settings.Default.BorderRadius = 8;
                Settings.Default.FormShadows = true;
                Settings.Default.Save();
                ToastUtil.CreateToast("Theme reset!");
                RefreshSettingsTheme();
                Settings.Default.Reload();

                try
                {
                    _colorsInnerPanel?.Dispose();
                    _colorsInnerPanel = null;
                }
                catch { }
                try
                {
                    _presetsInnerPanel?.Dispose();
                    _presetsInnerPanel = null;
                }
                catch { }
                try
                {
                    _colorAnimation?.ClearItems();
                }
                catch { }
                try
                {
                    _presetsAnimation?.ClearItems();
                }
                catch { }
                try
                {
                    PopulateColorSwatches();
                }
                catch { }
                try
                {
                    PopulatePresets();
                }
                catch { }
                try
                {
                    RefreshDynamicTheme();
                }
                catch { }
            }
            catch (Exception ex)
            {
                ToastUtil.CreateToast(Color.Red, $"Failed to reset theme! Error: {ex.Message}");
            }
        }

        private void RefreshSettingsTheme()
        {
            var controlsToModify = new Control[] { };

            UpdateTheme.Refresh(this, controlsToModify);
            FormThemerEProperties.BorderRadius = Settings.Default.BorderRadius * 3;

            FormThemerEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormThemerEProperties.ShadowColor = Color.Black;
        }
    }
}
