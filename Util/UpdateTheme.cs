using Guna.UI2.WinForms;
using QuillsModManagerV2.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util
{
    public static class UpdateTheme
    {
        #region Refresh functions
        private const float HoverBrightnessFactor = 1.2f;
        private const float CheckedBrightnessFactor = 1.5f;

        public static void Refresh(Form form, Control[] controlsToModify)
        {
            var settings = Settings.Default;
            int borderRadius = Math.Max(settings.BorderRadius, 1);
            Color detailActiveColor = settings.DetailActive;
            Color buttonColor = settings.ButtonColor;

            FormUtil.ApplyRoundedForm(form, borderRadius);
            RefreshControls(controlsToModify, borderRadius, detailActiveColor, buttonColor);
            ApplyTheme(form);
        }

        public static void Refresh(Form form, Control[] controlsToModify, Form backgroundForm = null)
        {
            var settings = Settings.Default;
            int borderRadius = Math.Max(settings.BorderRadius, 1);
            Color detailActiveColor = settings.DetailActive;
            Color buttonColor = settings.ButtonColor;

            FormUtil.ApplyRoundedForm(form, borderRadius);

            if (backgroundForm != null)
                FormUtil.ApplyRoundedForm(backgroundForm, borderRadius);

            RefreshControls(controlsToModify, borderRadius, detailActiveColor, buttonColor);
            ApplyTheme(form);
        }

        private static void RefreshControls(Control[] controls, int borderRadius, Color detailActiveColor, Color buttonColor)
        {
            foreach (Control control in controls)
            {
                RefreshControl(control, detailActiveColor, buttonColor);

                if (control.HasChildren)
                    RefreshControls(control.Controls.Cast<Control>().ToArray(), borderRadius, detailActiveColor, buttonColor);
            }
        }

        private static void RefreshControl(Control control, Color detailActiveColor, Color buttonColor)
        {
            switch (control)
            {
                case Guna2Button button:
                    SetButtonProperties(button, detailActiveColor, buttonColor);
                    break;
                case Guna2TextBox textBox:
                    SetTextBoxProperties(textBox, detailActiveColor, buttonColor);
                    break;
                case Guna2ComboBox comboBox:
                    SetComboBoxProperties(comboBox, detailActiveColor, buttonColor);
                    break;
            }
        }

        private static void SetButtonProperties(Guna2Button button, Color detailActiveColor, Color buttonColor)
        {
            button.CustomBorderColor = detailActiveColor;
            button.HoverState.BorderColor = detailActiveColor;
            button.CheckedState.BorderColor = detailActiveColor;
            button.CheckedState.ForeColor = Settings.Default.TextColor;
            button.CheckedState.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.DisabledState.BorderColor = detailActiveColor;
            button.DisabledState.ForeColor = Settings.Default.TextColor;
            button.HoverState.FillColor = AdjustBrightness(buttonColor, HoverBrightnessFactor);
            button.CheckedState.FillColor = AdjustBrightness(buttonColor, CheckedBrightnessFactor);
            button.DisabledState.FillColor = AdjustBrightness(buttonColor, CheckedBrightnessFactor);
            button.PressedColor = Settings.Default.DetailActive;
        }

        private static void SetTextBoxProperties(Guna2TextBox textBox, Color detailActiveColor, Color buttonColor)
        {
            textBox.HoverState.BorderColor = detailActiveColor;
            textBox.FocusedState.BorderColor = detailActiveColor;
            textBox.HoverState.FillColor = AdjustBrightness(buttonColor, HoverBrightnessFactor);
            textBox.FocusedState.FillColor = AdjustBrightness(buttonColor, CheckedBrightnessFactor);
        }

        private static void SetComboBoxProperties(Guna2ComboBox comboBox, Color detailActiveColor, Color buttonColor)
        {
            comboBox.HoverState.BorderColor = detailActiveColor;
            comboBox.FocusedState.BorderColor = detailActiveColor;
            comboBox.HoverState.FillColor = AdjustBrightness(buttonColor, HoverBrightnessFactor);
            comboBox.FocusedState.FillColor = AdjustBrightness(buttonColor, CheckedBrightnessFactor);
        }

        public static void RefreshColors(Control[] controlsToModify)
        {
            var settings = Properties.Settings.Default;
            Color detailActiveColor = settings.DetailActive;
            Color buttonColor = settings.ButtonColor;

            foreach (var control in controlsToModify)
            {
                RefreshControl(control, detailActiveColor, buttonColor);
            }
        }
        #endregion

        #region Helper functions
        public static Color AdjustBrightness(Color color, float factor)
        {
            factor = Math.Max(0, factor);

            int r = (int)Math.Min(255, color.R * factor);
            int g = (int)Math.Min(255, color.G * factor);
            int b = (int)Math.Min(255, color.B * factor);

            return Color.FromArgb(color.A, r, g, b);
        }

        private static void InvalidateContextMenus(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.ContextMenuStrip?.Invalidate();

                if (control.HasChildren)
                    InvalidateContextMenus(control.Controls);
            }
        }

        private static readonly Dictionary<Control, Image> OriginalIcons = new Dictionary<Control, Image>();

        public static void RegisterIcons(Control root)
        {
            CacheIcons(root);
            ApplyTheme(root);
        }

        public static void ApplyTheme(Control root)
        {
            Color color = Settings.Default.TextColor;

            foreach (Control c in GetControls(root))
            {
                if (c is Guna2Button btn &&
                    OriginalIcons.TryGetValue(btn, out Image original))
                {
                    btn.Image?.Dispose();
                    btn.Image = RecolorIcon(original, color);
                }
            }
        }

        private static void CacheIcons(Control root)
        {
            foreach (Control c in GetControls(root))
            {
                if (c is Guna2Button btn && btn.Image != null && !OriginalIcons.ContainsKey(btn))
                {
                    try { OriginalIcons[btn] = (Image)btn.Image.Clone(); } catch { }
                }

                if (c is PictureBox pb && pb.Image != null && !OriginalIcons.ContainsKey(pb))
                {
                    try { OriginalIcons[pb] = (Image)pb.Image.Clone(); } catch { }
                }
            }
        }

        private static IEnumerable<Control> GetControls(Control parent)
        {
            if (parent == null) yield break;

            yield return parent;

            foreach (Control c in parent.Controls)
            {
                foreach (Control child in GetControls(c))
                    yield return child;
            }
        }

        public static Bitmap RecolorIcon(Image source, Color targetColor)
        {
            Bitmap src = new Bitmap(source);
            Bitmap dst = new Bitmap(src.Width, src.Height);

            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    Color p = src.GetPixel(x, y);

                    dst.SetPixel(x, y, Color.FromArgb(p.A, targetColor.R, targetColor.G, targetColor.B));
                }
            }

            src.Dispose();
            return dst;
        }

        public static void Unregister(Control root)
        {
            foreach (Control c in GetControls(root))
            {
                try
                {
                    if (OriginalIcons.TryGetValue(c, out Image img))
                    {
                        try { img.Dispose(); } catch { }
                        OriginalIcons.Remove(c);
                    }
                }
                catch { }
            }
        }
    }
    #endregion
}