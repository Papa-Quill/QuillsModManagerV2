using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.UserControls
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            UpdateTheme.RegisterIcons(this);
            FormUtil.ApplyRoundedForm(this, Settings.Default.BorderRadius);
            BtnGithub.HoverState.BorderColor = Settings.Default.DetailActive;
            BtnGithub.HoverState.FillColor = UpdateTheme.AdjustBrightness(
                Settings.Default.ButtonColor,
                1.2F
            );

            FormAboutEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
            if (!Settings.Default.FormShadows)
                FormAboutEProperties.ShadowColor = Color.Black;
            else
                FormAboutEProperties.ShadowColor = Settings.Default.DetailActive;
        }

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
                    Settings.Default.HotKeyForm = "Default";
                    FormUtil.ShowForm<FormHotKeys>();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FormUtil.Close(this);
        }

        private void BtnGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/papa-quill/QuillsModManagerV2");
        }
    }
}
