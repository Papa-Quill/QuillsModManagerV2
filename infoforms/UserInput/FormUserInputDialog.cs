using System;
using System.Windows.Forms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.InfoForms
{
    public partial class FormUserInputDialog : Form
    {
        private bool autoClose;
        public bool Confirmed { get; private set; }

        public FormUserInputDialog(string Prompt, string DefaultText = "", bool autoClose = true)
        {
            InitializeComponent();
            FormUserInputEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
            if (!Settings.Default.FormShadows)
                FormUserInputEProperties.ShadowColor = System.Drawing.Color.Black;
            TxtUserInput.PlaceholderText = Prompt;
            TxtUserInput.Text = DefaultText;
            TxtUserInput.HoverState.BorderColor = Settings.Default.DetailActive;
            TxtUserInput.FocusedState.BorderColor = Settings.Default.DetailActive;
            TxtUserInput.HoverState.FillColor = UpdateTheme.AdjustBrightness(
                Settings.Default.ButtonColor,
                1.2f
            );
            TxtUserInput.FocusedState.FillColor = UpdateTheme.AdjustBrightness(
                Settings.Default.ButtonColor,
                1.5f
            );
            Text = "User Input Dialog " + Prompt;
            this.autoClose = autoClose;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.Control | Keys.W:
                    DialogResult = DialogResult.Cancel;
                    BtnClose.PerformClick();
                    return true;

                case Keys.Enter:
                    if (autoClose)
                    {
                        Confirmed = true;
                        try
                        {
                            DialogResult = DialogResult.OK;
                        }
                        catch { }
                        FormUtil.CloseDialogOk(this, DialogResult.OK);
                    }
                    else
                        DialogResult = DialogResult.OK;
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FormUtil.Close(this);
            DialogResult = DialogResult.Cancel;
        }
    }
}
