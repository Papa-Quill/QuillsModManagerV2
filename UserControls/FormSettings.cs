using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;
using QuillsModManagerV2.Util.Controls.AnimatedList;

namespace QuillsModManagerV2.UserControls
{
    public partial class FormSettings : Form
    {
        private System.Collections.Generic.Dictionary<string, string> _settingDisplayNames;
        private Panel _boolInnerPanel;
        private AnimatedListController _animation;

        public FormSettings()
        {
            InitializeComponent();
            InitializeSettingsAndEvents();
            UpdateTheme.RegisterIcons(this);
        }

        private void UpdateBoolScroll()
        {
            if (_boolInnerPanel == null)
                return;

            int total = _boolInnerPanel.Height;
            int view = PanelContent.Height;

            vScroll.Minimum = 0;
            vScroll.Maximum = Math.Max(0, total - view);
            vScroll.LargeChange = Math.Max(1, view);
            vScroll.Value = Math.Max(0, Math.Min(vScroll.Value, vScroll.Maximum));

            _boolInnerPanel.Top = -vScroll.Value;
            vScroll.Enabled = total > view;
        }

        private void BoolInnerPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (_boolInnerPanel == null)
                return;
            _boolInnerPanel.Top = -vScroll.Value;
            _boolInnerPanel.Invalidate();
        }

        private void PanelContent_Resize(object sender, EventArgs e)
        {
            if (_boolInnerPanel == null)
                return;
            _boolInnerPanel.Width = PanelContent.Width;
            UpdateBoolScroll();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int delta = e.Delta > 0 ? -12 : 12;

            int newVal = Math.Max(
                vScroll.Minimum,
                Math.Min(vScroll.Maximum, vScroll.Value + delta)
            );

            vScroll.Value = newVal;
            Invalidate(false);
        }

        #region Form base
        private void InitializeSettingsAndEvents()
        {
            SettingsChangedEvent(this, null);
            _settingDisplayNames = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "FormShadows", "Form Glow" },
                { "ToolTips", "Tool Tips" },
                { "DebugMode", "Debug Mode" },
                { "EfficiencyMode", "Effeciency Mode" }
            };

            PopulateBooleanToggles();

            Settings.Default.SettingsLoaded += SettingsChangedEvent;
        }

        #region Hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.Control | Keys.W:
                    BtnClose.PerformClick();
                    return true;

                case Keys.Control | Keys.T:
                    BtnTheme.PerformClick();
                    return true;

                case Keys.Control | Keys.K:
                    Settings.Default.HotKeyForm = "Settings";
                    FormUtil.ShowForm<FormHotKeys>();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        private void BtnToolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.AccessibleDescription is string tooltipText)
                ToolTipUtil.SetToolTip(button, tooltipText);
        }

        private void PopulateBooleanToggles()
        {
            try
            {
                PanelContent.SuspendLayout();

                if (_boolInnerPanel != null)
                {
                    for (int i = _boolInnerPanel.Controls.Count - 1; i >= 0; i--)
                    {
                        var c = _boolInnerPanel.Controls[i];
                        if (c is AnimatedPanel)
                            _boolInnerPanel.Controls.RemoveAt(i);
                    }

                    try
                    {
                        _animation?.ClearItems();
                    }
                    catch { }
                }

                var boolProps = _settingDisplayNames
                    .Keys.Select(k => typeof(Settings).GetProperty(k))
                    .Where(pi => pi != null && pi.PropertyType == typeof(bool))
                    .ToList();

                int y = 8;
                int width = Math.Max(100, PanelContent.Width - 24);

                if (_boolInnerPanel == null || _boolInnerPanel.IsDisposed)
                {
                    _boolInnerPanel = new Panel
                    {
                        Left = 0,
                        Top = 0,
                        Width = PanelContent.Width,
                        Height = PanelContent.Height,
                        BackColor = Settings.Default.BGPrimary,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    PanelContent.Controls.Add(_boolInnerPanel);

                    vScroll.Scroll -= (s, e) =>
                    {
                        _boolInnerPanel.Top = -vScroll.Value;
                        _boolInnerPanel.Invalidate();
                    };
                    vScroll.Scroll += (s, e) =>
                    {
                        _boolInnerPanel.Top = -vScroll.Value;
                        _boolInnerPanel.Invalidate();
                    };

                    PanelContent.Resize -= PanelContent_Resize;
                    PanelContent.Resize += PanelContent_Resize;

                    if (_animation == null)
                    {
                        _animation = new AnimatedListController();
                        _animation.AnimationUpdated += Animation_AnimationUpdated;
                    }
                }

                for (int i = 0; i < boolProps.Count; i++)
                {
                    var p = boolProps[i];
                    string key = p.Name;
                    string title = _settingDisplayNames.ContainsKey(key)
                        ? _settingDisplayNames[key]
                        : NicifyName(key);

                    var panel = new AnimatedPanel
                    {
                        Left = 0,
                        Top = 0,
                        Width = _boolInnerPanel.Width,
                        Height = 28,
                        BackColor = Settings.Default.BGSecondary
                    };

                    var row = new ToggleRow(key, title)
                    {
                        Left = 8,
                        Top = 0,
                        Width = width - 8,
                        Height = 28,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                    };

                    row.CheckedChanged += (s, e) =>
                    {
                        try
                        {
                            Settings.Default.Save();
                            Settings.Default.Reload();
                            SettingsChangedEvent(this, null);
                        }
                        catch { }
                    };

                    panel.Controls.Add(row);
                    _boolInnerPanel.Controls.Add(panel);

                    if (_animation != null)
                    {
                        var item = new AnimatedItem
                        {
                            Key = key,
                            Panel = panel,
                            CurrentY = i * 28 + 55,
                            TargetY = i * 28,
                            Opacity = 0f,
                            OffsetY = 8f,
                            AnimationStart = _animation.AnimationClock.ElapsedMilliseconds,
                            Delay = i * 45
                        };
                        _animation.RegisterItem(item);
                    }

                    y += panel.Height + 6;
                }

                _animation?.Start();

                _boolInnerPanel.Height = Math.Max(PanelContent.Height, y);
                UpdateBoolScroll();

                PanelContent.ResumeLayout();
            }
            catch { }
        }

        private void Animation_AnimationUpdated()
        {
            if (_animation == null)
                return;

            foreach (AnimatedItem item in _animation.AnimatedItems)
            {
                if (item.Panel == null)
                    continue;

                item.Panel.Top = (int)item.CurrentY;
                item.Panel.Visible = item.Visible;
            }

            _boolInnerPanel?.Invalidate();
        }

        private string NicifyName(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(s[0]);
            for (int i = 1; i < s.Length; i++)
            {
                if (char.IsUpper(s[i]) && !char.IsUpper(s[i - 1]))
                    sb.Append(' ');
                sb.Append(s[i]);
            }
            return sb.ToString();
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
            var controlsToModify = new Control[]
            {
                BtnGamePathLabel,
                BtnUserDataPathLabel,
                BtnModLibraryPathLabel,
                BtnChooseGamePath,
                BtnChooseUserDataPath,
                BtnChooseModPath,
                BtnTheme
            };
            UpdateTheme.Refresh(this, controlsToModify);
            FormSettingsEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormSettingsEProperties.ShadowColor = Color.Black;

            if (Settings.Default.BorderRadius == 0)
                BtnClose.Width = 36;
            else
                BtnClose.Width = 37;

            if (WindowState != FormWindowState.Maximized)
                FormSettingsEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
            FormSettingsEProperties.BorderRadius =
                (WindowState == FormWindowState.Maximized) ? 0 : Settings.Default.BorderRadius * 3;

            #region OverlappedControls event
            foreach (var control in controlsToModify)
            {
                control.MouseEnter += OverlappedControls_MouseEnter;
            }
            #endregion
            RefreshDynamicTheme();
        }

        public void RefreshDynamicTheme()
        {
            try
            {
                FormSettingsEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
                try
                {
                    PanelContent.BackColor = Settings.Default.BGPrimary;
                }
                catch { }

                if (_boolInnerPanel != null)
                {
                    try
                    {
                        _boolInnerPanel.BackColor = Settings.Default.BGPrimary;
                    }
                    catch { }
                    try
                    {
                        foreach (Control c in _boolInnerPanel.Controls)
                        {
                            UpdateControlThemeRecursive(c);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void UpdateControlThemeRecursive(Control ctrl)
        {
            if (ctrl == null)
                return;
            try
            {
                if (ctrl is Panel)
                    ctrl.BackColor = Settings.Default.BGSecondary;

                if (ctrl is Label lbl)
                    lbl.ForeColor = Settings.Default.TextColor;

                if (ctrl is Guna2Button gbtn)
                {
                    gbtn.BorderColor = Settings.Default.DetailColor;
                    try
                    {
                        gbtn.CustomBorderColor = Settings.Default.DetailColor;
                    }
                    catch { }
                }

                if (ctrl.HasChildren)
                {
                    foreach (Control child in ctrl.Controls)
                    {
                        UpdateControlThemeRecursive(child);
                    }
                }
            }
            catch { }
        }

        private void OverlappedControls_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control button)
                button.BringToFront();
        }
        #endregion

        private void BtnResetSettings_Click(object sender, EventArgs e)
        {
            const string MsgBoxMessage =
                "Are you sure you want to reset all settings?\nThis cannot be reversed!";
            DialogResult dgresult = MessageBoxUtil.Show("Reset Settings", MsgBoxMessage, true);
            if (dgresult == DialogResult.Yes)
                ResetSettings();
        }

        private void ResetSettings()
        {
            try
            {
                Settings.Default.Reset();
                Settings.Default.Save();
                ToastUtil.CreateToast("Settings reset!");
                RefreshSettingsTheme();
                Settings.Default.Reload();

                try
                {
                    PopulateBooleanToggles();
                }
                catch { }
                try
                {
                    RefreshDynamicTheme();
                }
                catch { }
                try
                {
                    UpdateBoolScroll();
                }
                catch { }
            }
            catch (Exception ex)
            {
                ToastUtil.CreateToast(Color.Red, $"Failed to reset settings! Error: {ex.Message}");
            }
        }

        private void RefreshSettingsTheme()
        {
            var controlsToModify = new Control[] { };

            UpdateTheme.Refresh(this, controlsToModify);
            FormSettingsEProperties.BorderRadius = Settings.Default.BorderRadius * 3;

            FormSettingsEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormSettingsEProperties.ShadowColor = Color.Black;
        }

        private void BtnChoosePath_Click(object sender, EventArgs e)
        {
            if (sender is Guna2Button btn)
            {
                if (btn.Name == "BtnChooseGamePath" || btn.Name == "BtnGamePathLabel")
                {
                    ToastUtil.CreateToast("Hint: steam\\steamapps\\common\\CastleCrashers");
                    using (var openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select Castle.exe";
                        openFileDialog.CheckFileExists = false;
                        openFileDialog.CheckPathExists = true;
                        openFileDialog.FileName = "castle.exe";
                        openFileDialog.Filter = "Castle.exe|castle.exe";
                        openFileDialog.InitialDirectory = Settings.Default.GamePath;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (
                                !File.Exists(openFileDialog.FileName)
                                || Path.GetFileName(openFileDialog.FileName).ToLower()
                                    != "castle.exe"
                            )
                            {
                                ToastUtil.CreateToast(
                                    Color.Red,
                                    "Invalid selection! Please select the castle.exe file/folder."
                                );
                                return;
                            }
                            Settings.Default.GamePath = Path.GetDirectoryName(
                                openFileDialog.FileName
                            );
                            Settings.Default.Save();
                            ToastUtil.CreateToast(
                                Color.Lime,
                                "Game path set to: " + Settings.Default.GamePath
                            );
                        }
                    }
                }
                else if (btn.Name == "BtnChooseUserDataPath" || btn.Name == "BtnUserDataPathLabel")
                {
                    ToastUtil.CreateToast("Hint: steam\\userdata");
                    using (var openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select Folder";
                        openFileDialog.CheckFileExists = false;
                        openFileDialog.CheckPathExists = true;
                        openFileDialog.FileName = "Select the user data folder";
                        openFileDialog.Filter = "Folders|\n";
                        openFileDialog.InitialDirectory = Settings.Default.UserDataPath;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (
                                Path.GetDirectoryName(openFileDialog.FileName)
                                    .ToLower()
                                    .EndsWith("userdata")
                            )
                            {
                                Settings.Default.UserDataPath = Path.GetDirectoryName(
                                    openFileDialog.FileName
                                );
                                Settings.Default.Save();
                                ToastUtil.CreateToast(
                                    Color.Lime,
                                    "User data path set to: " + Settings.Default.UserDataPath
                                );
                            }
                            else
                            {
                                ToastUtil.CreateToast(
                                    Color.Red,
                                    "Invalid selection! Please select the steam\\userdata folder."
                                );
                            }
                        }
                    }
                }
                else if (btn.Name == "BtnChooseModPath" || btn.Name == "BtnModLibraryPathLabel")
                {
                    ToastUtil.CreateToast("Hint: repos\\QMM-Mod-Repo");
                    using (var openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select Folder";
                        openFileDialog.CheckFileExists = false;
                        openFileDialog.CheckPathExists = true;
                        openFileDialog.FileName = "Select the mod library folder";
                        openFileDialog.Filter = "Folders|\n";
                        openFileDialog.InitialDirectory = Settings.Default.ModPath;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Settings.Default.ModPath = Path.GetDirectoryName(
                                openFileDialog.FileName
                            );
                            Settings.Default.Save();
                            ToastUtil.CreateToast(
                                Color.Lime,
                                "Mod library path set to: " + Settings.Default.ModPath
                            );
                        }
                    }
                }
            }
        }

        private void BtnTheme_Click(object sender, EventArgs e)
        {
            FormThemer formThemer = new FormThemer();
            FormUtil.ShowModalForm(this, formThemer);
        }
    }
}
