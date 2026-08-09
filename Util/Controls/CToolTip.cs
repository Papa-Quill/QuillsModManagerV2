using System.Drawing;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util
{
    public class CustomToolTip : ToolTip
    {
        public CustomToolTip()
        {
            OwnerDraw = true;
            Draw += CustomToolTip_Draw;
        }

        private Color BorderColor { get; set; } = Properties.Settings.Default.DetailColor;
        private Color TextColor { get; set; } = Properties.Settings.Default.TextColor;
        private Color BgColor { get; set; } = Properties.Settings.Default.BGTertiary;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Draw -= CustomToolTip_Draw;
            base.Dispose(disposing);
        }

        private void CustomToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (SolidBrush backBrush = new SolidBrush(BgColor))
            using (Pen borderPen = new Pen(BorderColor, 1))
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                Graphics g = e.Graphics;

                g.FillRectangle(backBrush, e.Bounds);
                g.DrawRectangle(
                    borderPen,
                    new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1)
                );
                g.DrawString(
                    e.ToolTipText,
                    e.Font,
                    textBrush,
                    new PointF(e.Bounds.X + 2, e.Bounds.Y + 2)
                );
            }
        }
    }
}
