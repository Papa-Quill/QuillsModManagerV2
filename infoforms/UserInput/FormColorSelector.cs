using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.infoforms.UserInput
{
    public partial class FormColorSelector : Form
    {
        #region Variables
        public static Color SelectedColor;
        public event Action<Color> ColorChanged;
        #endregion

        public FormColorSelector()
        {
            InitializeComponent();
            InitializeSettingsAndEvent();
        }

        #region Form base
        private void InitializeSettingsAndEvent()
        {
            var controlsToModify = new Control[]
            {
                BtnColorPicker,
                TextBoxHexColor,
                TextBoxRGBColor
            };
            UpdateTheme.Refresh(this, controlsToModify);

            FormColorSelectorEProperties.BorderRadius = Settings.Default.BorderRadius * 3;

            if (!Settings.Default.FormShadows)
                FormColorSelectorEProperties.ShadowColor = Color.Black;

            BtnClose.Click += (s, e) => ApplySettings();
            TextBoxHexColor.Leave += TextBoxHexColor_Leave;
            TextBoxRGBColor.Leave += TextBoxRGBColor_Leave;
        }

        private void ApplySettings()
        {
            Cursor = Cursors.Default;
            Settings.Default.Save();
            Settings.Default.Reload();
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

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

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
            TextBoxHexColor.Text = ColorTranslator.ToHtml(SelectedColor);
            TextBoxRGBColor.Text =
                SelectedColor.R.ToString()
                + ", "
                + SelectedColor.G.ToString()
                + ", "
                + SelectedColor.B.ToString();
            try
            {
                ColorChanged?.Invoke(SelectedColor);
            }
            catch { }
        }

        public void UpdateUIFromColor(Color color)
        {
            SelectedColor = color;
            BtnColorPreview.FillColor = color;
            TextBoxHexColor.Text = ColorTranslator.ToHtml(color);
            TextBoxRGBColor.Text = $"{color.R}, {color.G}, {color.B}";
            try
            {
                ScrollBarRed.Value = color.R;
            }
            catch { }
            try
            {
                ScrollBarGreen.Value = color.G;
            }
            catch { }
            try
            {
                ScrollBarBlue.Value = color.B;
            }
            catch { }
            try
            {
                ColorChanged?.Invoke(SelectedColor);
            }
            catch { }
        }

        private void TextBoxHexColor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxHexColor.Text))
                return;
            string hexInput = TextBoxHexColor.Text.Trim();
            if (!hexInput.StartsWith("#"))
                hexInput = "#" + hexInput;
            try
            {
                var c = ColorTranslator.FromHtml(hexInput);
                UpdateUIFromColor(c);
            }
            catch
            {
                ToastUtil.CreateToast(Color.Red, "Invalid Hex Color!");
            }
        }

        private void TextBoxRGBColor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxRGBColor.Text))
                return;
            var parts = TextBoxRGBColor.Text.Split(',').Select(s => s.Trim()).ToArray();
            if (parts.Length != 3)
            {
                ToastUtil.CreateToast(Color.Red, "Invalid RGB Color!");
                return;
            }
            if (
                int.TryParse(parts[0], out int r)
                && int.TryParse(parts[1], out int g)
                && int.TryParse(parts[2], out int b)
            )
            {
                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));
                UpdateUIFromColor(Color.FromArgb(r, g, b));
            }
            else
                ToastUtil.CreateToast(Color.Red, "Invalid RGB Color!");
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

        private void BtnColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            Color color = GetColorAtCursor();
            SelectedColor = color;
            BtnColorPreview.FillColor = SelectedColor;
            TextBoxHexColor.Text = ColorTranslator.ToHtml(SelectedColor);
            TextBoxRGBColor.Text =
                SelectedColor.R.ToString()
                + ", "
                + SelectedColor.G.ToString()
                + ", "
                + SelectedColor.B.ToString();
            ScrollBarRed.Value = SelectedColor.R;
            ScrollBarGreen.Value = SelectedColor.G;
            ScrollBarBlue.Value = SelectedColor.B;
        }
    }
}
