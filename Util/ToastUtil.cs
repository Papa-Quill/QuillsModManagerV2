using System.Drawing;
using System.Windows.Forms;
using QuillsModManagerV2.InfoForms;

namespace QuillsModManagerV2.Util
{
    internal class ToastUtil
    {
        public static void CreateToast(Color ToastColor, string ToastText)
        {
            Form OriginalForm = Form.ActiveForm;
            Properties.Settings.Default.NotifColor = ToastColor;
            Properties.Settings.Default.TxtNotif = ToastText;
            FormToast toast = new FormToast();
            toast.Show();
            OriginalForm?.Focus();
        }

        public static void CreateToast(string ToastText)
        {
            Form OriginalForm = Form.ActiveForm;
            Properties.Settings.Default.NotifColor = Color.White;
            Properties.Settings.Default.TxtNotif = ToastText;
            FormToast toast = new FormToast();
            toast.Show();
            OriginalForm?.Focus();
        }
    }
}
