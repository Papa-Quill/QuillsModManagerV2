using System.Drawing;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util.Controls
{
    public class CClickThroughLabel : Label
    {
        public CClickThroughLabel()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color textColor = Enabled ? ForeColor : Properties.Settings.Default.TextColor;

            using (SolidBrush backgroundBrush = new SolidBrush(Color.Transparent))
            {
                e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }

            TextFormatFlags flags = TextFormatFlags.Default;

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    flags = TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    flags = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopRight:
                    flags = TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleLeft:
                    flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.BottomLeft:
                    flags = TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    flags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomRight:
                    flags = TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, textColor, flags);
        }
    }
}
