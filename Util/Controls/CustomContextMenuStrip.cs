using System;
using System.Drawing;
using System.Windows.Forms;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util.Controls
{
    class CustomContextMenuStrip : ToolStripProfessionalRenderer
    {
        public CustomContextMenuStrip()
            : base(new CustomProfessionalColors()) { }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Settings.Default.TextColor;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            e.ToolStrip.BackColor = Settings.Default.BGTertiary;
            base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Settings.Default.TextColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }
    }

    public class NoMarginToolStripMenuItem : ToolStripMenuItem
    {
        public NoMarginToolStripMenuItem(string text)
            : base(text) { }

        protected override void OnDropDownShow(EventArgs e)
        {
            if (DropDown is ToolStripDropDownMenu menu)
            {
                menu.ShowImageMargin = false;
                menu.ShowCheckMargin = false;
            }

            base.OnDropDownShow(e);
        }
    }

    public class CustomProfessionalColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Settings.Default.ButtonColor;
        public override Color MenuItemSelectedGradientBegin => Settings.Default.ButtonColor;
        public override Color MenuItemSelectedGradientEnd => Settings.Default.ButtonColor;
        public override Color MenuItemBorder => Settings.Default.DetailActive;
    }
}
