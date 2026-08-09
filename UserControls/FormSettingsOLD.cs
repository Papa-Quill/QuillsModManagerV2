using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.InfoForms.UserInput;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.UserControls
{
    public partial class FormSettingsOLD : Form
    {
        public FormSettingsOLD()
        {
            InitializeComponent();
            InitializeSettings();
            RefreshSettingsTheme();
            UpdateTheme.RegisterIcons(this);
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

                case Keys.Control | Keys.S:
                    BtnSave.PerformClick();
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

        #region Form base
        private void InitializeSettings()
        {
            BtnDebugMode.Checked = QMM.DebugModeEnabled;
            BtnToolTips.Checked = Settings.Default.ToolTips;
            BtnFormShadows.Checked = Settings.Default.FormShadows;
            BtnEfficiencyMode.Checked = Settings.Default.EfficiencyMode;
            TxtRoundAmount.Text = Settings.Default.BorderRadius.ToString();
        }

        private void RefreshSettingsTheme()
        {
            var controlsToModify = new Control[]
            {
                ComboBoxThemes,
                ComboBoxChooseColor,
                BtnFormShadows,
                BtnToolTips,
                BtnSetColor,
                BtnSetColor,
                BtnSelectColor,
                BtnAbout,
                BtnEfficiencyMode,
                TxtColorInput,
                TxtRoundAmount,
                TxtColorInput,
                BtnDebugMode,
                BtnGamePath
            };

            UpdateTheme.Refresh(this, controlsToModify);
            FormSettingsEProperties.BorderRadius = Settings.Default.BorderRadius * 3;

            FormSettingsEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormSettingsEProperties.ShadowColor = Color.Black;
        }
        #endregion

        #region Functions
        private void SetColor(Action<Color> setColorAction)
        {
            try
            {
                setColorAction(ColorTranslator.FromHtml(TxtColorInput.Text.Trim()));
                RefreshSettingsTheme();
            }
            catch (Exception)
            {
                InvalidColor();
            }
        }

        private void InvalidColor()
        {
            ToastUtil.CreateToast(Color.Red, "Invalid Color! Example: #00FFFF or Cyan");
        }

        private void TxtNumCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
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
            }
            catch (Exception ex)
            {
                ToastUtil.CreateToast(Color.Red, $"Failed to reset settings! Error: {ex.Message}");
            }
        }
        #endregion

        #region Header buttons
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            Settings.Default.Reload();
            FormUtil.Close(this);
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            FormUtil.MinimizeWindow();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.Save();
                Settings.Default.Reload();
                ToastUtil.CreateToast(Color.Lime, "Save Successful!");
            }
            catch (Exception ex)
            {
                ToastUtil.CreateToast(Color.Red, $"Save Failed! Error: {ex.Message}");
            }
        }

        private void BtnResetSettings_Click(object sender, EventArgs e)
        {
            const string MsgBoxMessage =
                "Are you sure you want to reset all settings?\nThis cannot be reversed!";
            DialogResult dgresult = MessageBoxUtil.Show("Reset Settings", MsgBoxMessage, true);
            if (dgresult == DialogResult.Yes)
                ResetSettings();
        }
        #endregion

        #region Button functions
        private void BtnToolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.AccessibleDescription is string tooltipText)
                ToolTipUtil.SetToolTip(button, tooltipText);
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using (FormColorSelectorOLD ColorSelector = new FormColorSelectorOLD())
            {
                Form modalBackground = new Form();
                FormUtil.SetModalBackgroundForm(modalBackground, this);
                modalBackground.Show();

                Timer fadeTimer = new Timer { Interval = 10 };
                FormUtil.SetModalBackgroundFormFadeTimer(fadeTimer, modalBackground, ColorSelector);
                fadeTimer.Start();

                Timer fadeOutTimer = new Timer { Interval = 10 };
                FormUtil.SetupModal(fadeOutTimer, ColorSelector, this, modalBackground, false);

                if (ColorSelector.ShowDialog() == DialogResult.OK)
                {
                    string htmlColor = ColorTranslator.ToHtml(Settings.Default.SelectedColor);
                    TxtColorInput.Text = htmlColor;
                }
                return;
            }
        }

        private Dictionary<string, Action<Color>> colorActions = new Dictionary<
            string,
            Action<Color>
        >
        {
            { "Primary Color", color => Settings.Default.BGPrimary = color },
            { "Secondary Color", color => Settings.Default.BGSecondary = color },
            { "Tertiary Color", color => Settings.Default.BGTertiary = color },
            { "Detail Color", color => Settings.Default.DetailColor = color },
            { "Active Color", color => Settings.Default.DetailActive = color },
            { "Button Color", color => Settings.Default.ButtonColor = color },
            { "Text Color", color => Settings.Default.TextColor = color },
            { "Placeholder Color", color => Settings.Default.PlaceholderColor = color }
        };

        private void BtnSetColor_Click(object sender, EventArgs e)
        {
            SetColor(setColorAction: color => colorActions[ComboBoxChooseColor.Text](color));
            if (ComboBoxChooseColor.Text == "Active Color" && Settings.Default.FormShadows)
                FormSettingsEProperties.ShadowColor = Settings.Default.DetailActive;
            else
                FormSettingsEProperties.ShadowColor = Color.Black;
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        private void BtnFormShadows_Click(object sender, EventArgs e)
        {
            Settings.Default.FormShadows = !Settings.Default.FormShadows;
            FormSettingsEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                FormSettingsEProperties.ShadowColor = Color.Black;
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        private void BtnToolTips_Click(object sender, EventArgs e)
        {
            Settings.Default.ToolTips = !Settings.Default.ToolTips;
        }

        private void BtnDebugMode_Click(object sender, EventArgs e)
        {
            QMM.DebugModeEnabled = !QMM.DebugModeEnabled;
            if (QMM.DebugModeEnabled)
                ToastUtil.CreateToast("Restart application for debug mode to apply.");
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            Form form = new FormAbout();
            FormUtil.ShowModalForm(this, form);
        }
        #endregion

        #region Other control functions
        private void ComboBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ComboBoxThemes.SelectedIndex)
            {
                case 1:
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
                    break;
                case 2:
                    SetThemeSettings(
                        "#F5F5F7",
                        "#FFFFFF",
                        "#EBEBEF",
                        "#D1D1D6",
                        "#3B82F6",
                        "#FFFFFF",
                        "#1C1C1E",
                        "#8E8E93"
                    );
                    break;
                case 3:
                    SetThemeSettings(
                        "#1E1E1E",
                        "#252526",
                        "#2D2D30",
                        "#3F3F46",
                        "#3B82F6",
                        "#2D2D30",
                        "#F3F3F3",
                        "#9CA3AF"
                    );
                    break;
                case 4:
                    SetThemeSettings(
                        "#FAFBFC",
                        "#FFFFFF",
                        "#F6F8FA",
                        "#D8DEE4",
                        "#3B82F6",
                        "#F6F8FA",
                        "#24292F",
                        "#6E7781"
                    );
                    break;
                case 5:
                    SetThemeSettings(
                        "#16141C",
                        "#1A1926",
                        "#292246",
                        "#C4B7FF",
                        "#8D50EB",
                        "#16141C",
                        "#F5F4F2",
                        "#C4B7FF"
                    );
                    break;
                case 6:
                    SetThemeSettings(
                        "#191724",
                        "#1F1D2E",
                        "#26233A",
                        "#C4A7E7",
                        "#9CCFD8",
                        "#191724",
                        "#E0DEF4",
                        "#6E6A86"
                    );
                    break;
                case 7:
                    SetThemeSettings(
                        "#232136",
                        "#2A273F",
                        "#393552",
                        "#C4A7E7",
                        "#E29492",
                        "#232136",
                        "#E0DEF4",
                        "#6E6A86"
                    );
                    break;
                case 8:
                    SetThemeSettings(
                        "#FAF4ED",
                        "#E1DCD5",
                        "#D4CBC3",
                        "#907AA9",
                        "#56949F",
                        "#FAF4ED",
                        "#575279",
                        "#9893A5"
                    );
                    break;
                default:
                    break;
            }
            RefreshSettingsTheme();
            Settings.Default.Save();
            Settings.Default.Reload();
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

        private void TxtRoundAmount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(TxtRoundAmount.Text, out int value))
            {
                if (value < 0)
                {
                    TxtRoundAmount.Text = "0";
                    value = 0;
                }
                else if (value > 15)
                {
                    TxtRoundAmount.Text = "15";
                    value = 15;
                }

                Settings.Default.BorderRadius = value;
            }
            else
            {
                TxtRoundAmount.Text = "0";
                Settings.Default.BorderRadius = 0;
            }

            RefreshSettingsTheme();
        }
        #endregion

        private void BtnGamePath_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select castle.exe";
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.FileName = "Select castle.exe";
                openFileDialog.Filter = "exe|*.exe";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (
                        Path.GetFileName(Path.GetDirectoryName(openFileDialog.FileName))
                            == "CastleCrashers"
                        && File.Exists(
                            Path.Combine(
                                Path.GetDirectoryName(openFileDialog.FileName),
                                "castle.exe"
                            )
                        )
                    )
                    {
                        DebugUtil.Log(
                            DebugLevel.INFO,
                            DebugFilter.SETTINGS,
                            $"Path set to {Path.GetDirectoryName(openFileDialog.FileName)}"
                        );
                        Settings.Default.GamePath = Path.GetDirectoryName(openFileDialog.FileName);
                        ToastUtil.CreateToast(Color.Lime, "Game path set successfully!");
                    }
                    else
                    {
                        DebugUtil.Log(
                            DebugLevel.WARNING,
                            DebugFilter.SETTINGS,
                            "Invalid folder selected. Please select the 'CastleCrashers' folder."
                        );
                        ToastUtil.CreateToast(
                            Color.Red,
                            "Invalid folder selected! Please select the 'CastleCrashers' folder."
                        );
                    }
                }
            }
        }

        private void BtnEfficiencyMode_Click(object sender, EventArgs e)
        {
            Settings.Default.EfficiencyMode = !Settings.Default.EfficiencyMode;
        }
    }
}
