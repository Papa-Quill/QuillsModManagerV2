using System.Windows.Forms;

namespace QuillsModManagerV2.Util
{
    public static class ToolTipUtil
    {
        private static CustomToolTip customToolTip;

        public static void SetToolTip(Control control, string tooltipText)
        {
            if (Properties.Settings.Default.ToolTips)
            {
                customToolTip?.Dispose();
                customToolTip = new CustomToolTip();
                customToolTip.SetToolTip(control, tooltipText);
            }
        }

        public static void SetToolTip(Control control, string tooltipText, bool Bypass)
        {
            if (Bypass)
            {
                customToolTip?.Dispose();
                customToolTip = new CustomToolTip();
                customToolTip.SetToolTip(control, tooltipText);
            }
        }
    }
}
