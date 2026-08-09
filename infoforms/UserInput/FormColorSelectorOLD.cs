using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.InfoForms.UserInput
{
    public partial class FormColorSelectorOLD : Form
    {
        #region Variables
        private bool ExitCode;
        private readonly Timer fadeTimer = new Timer();
        public static Color SelectedColor;
        #endregion

        public FormColorSelectorOLD()
        {
            InitializeComponent();
            InitializeSettingsAndTimer();
        }

        #region Form base
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams shadowParams = base.CreateParams;
                if (Settings.Default.FormShadows)
                    shadowParams.ClassStyle |= 0x00020000;
                return shadowParams;
            }
        }

        private void InitializeSettingsAndTimer()
        {
            fadeTimer.Interval = 50;
            fadeTimer.Tick += FadeOut;

            BtnFinishSelection.HoverState.FillColor = Color.SeaGreen;
            var controlsToModify = new Control[]
            {
                BtnColorPicker,
                TxtColorInputHex,
                TxtColorInputRGB
            };
            UpdateTheme.Refresh(this, controlsToModify);
        }

        private void FadeClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            fadeTimer.Start();
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                fadeTimer.Stop();
                if (!ExitCode)
                {
                    DialogResult = DialogResult.No;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
                Opacity -= 0.3;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (fadeTimer.Enabled)
                e.Cancel = true;
            else
                base.OnFormClosing(e);
        }
        #endregion

        #region Hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.Control | Keys.W:
                    BtnClose.PerformClick();
                    return true;

                case Keys.Control | Keys.K:
                    Settings.Default.HotKeyForm = "Color";
                    FormUtil.ShowForm<FormHotKeys>();
                    return true;

                case Keys.Enter:
                    BtnFinishSelection.PerformClick();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Functions
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            SelectedColor = Color.FromArgb(
                ScrollBarRed.Value,
                ScrollBarGreen.Value,
                ScrollBarBlue.Value
            );
            ScrollBarRed.FillColor = Color.FromArgb(ScrollBarRed.Value, 0, 0);
            ScrollBarGreen.FillColor = Color.FromArgb(0, ScrollBarGreen.Value, 0);
            ScrollBarBlue.FillColor = Color.FromArgb(0, 0, ScrollBarBlue.Value);
            BtnColorPreview.FillColor = SelectedColor;
            TxtColorInputHex.Text = ColorTranslator.ToHtml(SelectedColor);
            TxtColorInputRGB.Text =
                SelectedColor.R.ToString()
                + ", "
                + SelectedColor.G.ToString()
                + ", "
                + SelectedColor.B.ToString();
        }

        private void TxtColorInputRGB_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtColorInputRGB.Text))
                return;

            if (IsValidRGBColor(TxtColorInputRGB.Text))
            {
                SelectedColor = ConvertToColor(TxtColorInputRGB.Text);
                UpdateUIFromColor(SelectedColor);
            }
            else
                ToastUtil.CreateToast(Color.Red, "Invalid RGB Color!");
        }

        private void TxtColorInputHex_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtColorInputHex.Text))
                return;

            string hexInput = TxtColorInputHex.Text.Trim();
            if (!hexInput.StartsWith("#"))
                hexInput = "#" + hexInput;

            try
            {
                SelectedColor = ColorTranslator.FromHtml(hexInput);
                UpdateUIFromColor(SelectedColor);
            }
            catch (Exception)
            {
                ToastUtil.CreateToast(Color.Red, "Invalid Hex Color!");
            }
        }
        #endregion

        #region Helper functions
        private void UpdateUIFromColor(Color color)
        {
            TxtColorInputHex.Text = ColorTranslator.ToHtml(color);
            TxtColorInputRGB.Text = $"{color.R}, {color.G}, {color.B}";

            ScrollBarRed.Value = color.R;
            ScrollBarGreen.Value = color.G;
            ScrollBarBlue.Value = color.B;

            BtnColorPreview.FillColor = color;
            Settings.Default.SelectedColor = color;
        }

        static bool IsValidRGBColor(string input)
        {
            string pattern = @"^\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*$";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                int r = int.Parse(match.Groups[1].Value);
                int g = int.Parse(match.Groups[2].Value);
                int b = int.Parse(match.Groups[3].Value);

                if (r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255)
                    return true;
            }

            return false;
        }

        static Color ConvertToColor(string input)
        {
            string pattern = @"^\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*$";
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                int r = int.Parse(match.Groups[1].Value);
                int g = int.Parse(match.Groups[2].Value);
                int b = int.Parse(match.Groups[3].Value);

                if (r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255)
                    return Color.FromArgb(r, g, b);
            }
            ToastUtil.CreateToast(Color.Red, "Invalid RGB Color!");
            return Color.Black;
        }

        private Color GetColorAtCursor()
        {
            Point cursorPos = Cursor.Position;
            Bitmap screenPixel = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(screenPixel))
            {
                g.CopyFromScreen(cursorPos, Point.Empty, new Size(1, 1));
            }

            return screenPixel.GetPixel(0, 0);
        }
        #endregion

        #region Button functions
        private void BtnToolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.AccessibleDescription is string tooltipText)
                ToolTipUtil.SetToolTip(button, tooltipText);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FadeClosing(new FormClosingEventArgs(CloseReason.None, false));
        }

        private void BtnFinishSelection_Click(object sender, EventArgs e)
        {
            Settings.Default.SelectedColor = SelectedColor;
            FadeClosing(new FormClosingEventArgs(CloseReason.None, false));
            ExitCode = true;
        }

        private void BtnColorPicker_MouseDown(object sender, MouseEventArgs e)
        {
            BtnColorPicker.MouseMove += BtnColorPicker_MouseMove;
            Cursor = Cursors.Cross;
        }

        private void BtnColorPicker_MouseUp(object sender, MouseEventArgs e)
        {
            BtnColorPicker.MouseMove -= BtnColorPicker_MouseMove;
            Cursor = Cursors.Default;
        }

        private void BtnColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            Color color = GetColorAtCursor();
            SelectedColor = color;
            BtnColorPreview.FillColor = SelectedColor;
            TxtColorInputHex.Text = ColorTranslator.ToHtml(SelectedColor);
            TxtColorInputRGB.Text =
                SelectedColor.R.ToString()
                + ", "
                + SelectedColor.G.ToString()
                + ", "
                + SelectedColor.B.ToString();
            ScrollBarRed.Value = SelectedColor.R;
            ScrollBarGreen.Value = SelectedColor.G;
            ScrollBarBlue.Value = SelectedColor.B;
        }
        #endregion
    }
}
