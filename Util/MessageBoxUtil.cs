using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util
{
    public class MessageBoxUtil
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public static DialogResult Show(string caption, string message, bool dialog = false)
        {
            Properties.Settings.Default.TxtNotif = message;

            int preferredLabelHeight = 0;
            try
            {
                using (var f = new Font("Segoe UI", 12F))
                {
                    Size proposed = new Size(522, int.MaxValue);
                    var size = TextRenderer.MeasureText(
                        message,
                        f,
                        proposed,
                        TextFormatFlags.WordBreak
                    );
                    preferredLabelHeight = System.Math.Max(16, size.Height);
                }
            }
            catch
            {
                preferredLabelHeight = 16;
            }

            using (
                InfoForms.FormMessageBox NewMessage = new InfoForms.FormMessageBox(
                    caption,
                    message,
                    dialog
                )
            )
            {
                try
                {
                    NewMessage.BoxHeight = preferredLabelHeight;
                    NewMessage.ApplyBoxHeight();
                }
                catch { }

                try
                {
                    Form baseForm =
                        Form.ActiveForm
                        ?? (Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null);
                    if (baseForm != null)
                    {
                        FormUtil.ShowModalForm(baseForm, NewMessage, false);

                        try
                        {
                            baseForm.Capture = false;
                        }
                        catch { }
                        try
                        {
                            ReleaseCapture();
                        }
                        catch { }

                        int attempts = 0;
                        const int maxAttempts = 20;
                        while (!NewMessage.IsDisposed && NewMessage.Visible)
                        {
                            try
                            {
                                if (attempts < maxAttempts)
                                {
                                    bool overlayPresent = false;
                                    try
                                    {
                                        foreach (Control c in NewMessage.Controls)
                                        {
                                            if (c == null)
                                                continue;
                                            string tname = c.GetType().Name;
                                            if (
                                                !string.IsNullOrEmpty(tname)
                                                && tname
                                                    .ToLowerInvariant()
                                                    .Contains("bufferedpanel")
                                                && c.Visible
                                            )
                                            {
                                                overlayPresent = true;
                                                break;
                                            }
                                        }
                                    }
                                    catch { }

                                    if (!overlayPresent)
                                    {
                                        Control target = null;
                                        try
                                        {
                                            target = NewMessage.Controls["BtnYes"];
                                        }
                                        catch { }
                                        if (target == null)
                                            try
                                            {
                                                target = NewMessage.Controls["BtnClose"];
                                            }
                                            catch { }

                                        try
                                        {
                                            NewMessage.TopMost = true;
                                            NewMessage.Activate();
                                            NewMessage.BringToFront();
                                            if (target != null && target.Visible && target.Enabled)
                                            {
                                                target.Select();
                                                target.Focus();
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            catch { }

                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                            attempts++;
                        }

                        return NewMessage.DialogResult;
                    }
                }
                catch { }

                return NewMessage.ShowDialog();
            }
        }
    }
}
