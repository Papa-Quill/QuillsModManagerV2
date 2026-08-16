using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.InfoForms
{
    public partial class FormHotKeys : Form
    {
        #region Constants
        private const int FadeOutInterval = 50;
        private const double OpacityDecrement = 0.3;
        #endregion

        #region Variables
        private readonly Timer closeTimer = new Timer();
        private readonly Timer fadeOutTimer = new Timer();
        #endregion

        public FormHotKeys()
        {
            InitializeComponent();
            InitializeSettings();
            SetupEventHandlers();
        }

        #region Form base
        private void InitializeSettings()
        {
            var settingsMapping = CreateSettingsMapping();

            if (
                settingsMapping.TryGetValue(
                    Properties.Settings.Default.HotKeyForm,
                    out var settings
                )
            )
            {
                var (interval, width, height, title, notification) = settings;

                closeTimer.Interval = interval;
                Width = width;
                Height = height;
                LabelTitle.Text = title;
                TxtNotif.Text = notification;

                HotkeyBox.Width = Width - 25;
                HotkeyBox.Height = Height - 20;
                Location = new Point(0, Screen.PrimaryScreen.Bounds.Height / 2 - Height / 2);
                Text = title;

                closeTimer.Start();
            }
        }

        private Dictionary<
            string,
            (int interval, int width, int height, string title, string notification)
        > CreateSettingsMapping()
        {
            return new Dictionary<string, (int, int, int, string, string)>
            {
                ["Default"] = (
                    4000,
                    232,
                    102,
                    "Hotkeys - Default",
                    "Escape = Close Window\r\nCtrl + W = Close Window"
                ),
                ["Main"] = (
                    7000,
                    285,
                    168,
                    "Hotkeys - Main",
                    "Escape (x2) = Close Application\r\nCtrl + W (x2) = Close Application\r\nCtrl + E = Open Appdata\r\nCtrl + S = Settings\r\nCtrl + R = Profile Manager"
                ),
                ["Settings"] = (
                    5000,
                    232,
                    124,
                    "Hotkeys - Settings",
                    "Escape = Close Window\r\nCtrl + W = Close Window\r\nCtrl + T = Theme Manager"
                ),
                ["Color"] = (
                    5000,
                    232,
                    124,
                    "Hotkeys - Color",
                    "Escape = Close Window\r\nCtrl + W = Close Window\r\nEnter = Select Color"
                )
            };
        }

        private void SetupEventHandlers()
        {
            closeTimer.Tick += (sender, e) => FadeOut();
            var controlsToModify = new Control[]
            {
                LabelTitle,
                TxtNotif,
                HotkeyBox,
                PanelNotifColor
            };
            foreach (var control in controlsToModify)
            {
                control.MouseHover += (sender, e) =>
                {
                    closeTimer.Enabled = false;
                    Opacity = 1;
                };
                control.MouseLeave += (sender, e) => closeTimer.Enabled = true;
                control.Click += (sender, e) => FormUtil.Close(this);
            }
        }

        private void FadeOut()
        {
            closeTimer.Interval = FadeOutInterval;
            if (Opacity <= 0)
            {
                fadeOutTimer.Stop();
                FormUtil.Close(this);
            }
            else
                Opacity -= OpacityDecrement;
        }
        #endregion

        #region Hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == (Keys.Control | Keys.W))
            {
                FormUtil.Close(this);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
