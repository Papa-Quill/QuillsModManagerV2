using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;

namespace QuillsModManagerV2.InfoForms
{
    public partial class FormMessageBox : Form
    {
        #region Variables
        private bool ExitCode = false;
        private readonly Timer fadeTimer = new Timer();

        public string BoxMessage
        {
            get { return LabelMessage.Text; }
            set { LabelMessage.Text = value; }
        }
        public string BoxCaption
        {
            get { return LabelTitle.Text; }
            set { LabelTitle.Text = value; }
        }
        public int BoxHeight
        {
            get { return LabelMessage.Height; }
            set { LabelMessage.Height = value; }
        }
        #endregion

        public FormMessageBox(string caption, string message, bool dialog)
        {
            InitializeComponent();
            BtnClose.Visible = !dialog;
            BtnNo.Visible = dialog;
            BtnYes.Visible = dialog;
            BoxCaption = caption;
            BoxMessage = message;
            if (!Settings.Default.FormShadows)
                FormMessageBoxEProperties.ShadowColor = Color.Black;
            FormMessageBoxEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
            UpdateTheme.RegisterIcons(this);
            InitializeMessageBoxForm();
            InitializeTimers();
            try
            {
                if (BtnYes != null)
                {
                    BtnYes.Click -= BtnYes_Click;
                    BtnYes.Click += (s, e) =>
                    {
                        try
                        {
                            ExitCode = true;
                            FormUtil.CloseDialogOk(this, DialogResult.Yes);
                        }
                        catch
                        {
                            BtnYes_Click(s, e);
                        }
                    };
                }

                if (BtnNo != null)
                {
                    BtnNo.Click -= BtnClose_Click;
                    BtnNo.Click += (s, e) =>
                    {
                        try
                        {
                            ExitCode = false;
                            FormUtil.CloseDialogOk(this, DialogResult.No);
                        }
                        catch
                        {
                            BtnClose_Click(s, e);
                        }
                    };
                }
            }
            catch { }

            try
            {
                EventHandler shownHandler = null;
                shownHandler = (s, e) =>
                {
                    try
                    {
                        this.Shown -= shownHandler;
                    }
                    catch { }

                    try
                    {
                        Control target = null;
                        try
                        {
                            target = this.Controls.Find("BtnYes", true).FirstOrDefault();
                        }
                        catch { }
                        if (target == null)
                            try
                            {
                                target = this.Controls.Find("BtnClose", true).FirstOrDefault();
                            }
                            catch { }

                        try
                        {
                            this.Activate();
                            this.BringToFront();
                        }
                        catch { }

                        if (target != null)
                        {
                            try
                            {
                                target.Focus();
                                target.Select();
                            }
                            catch { }
                        }
                    }
                    catch { }
                };

                try
                {
                    this.Shown += shownHandler;
                }
                catch { }
            }
            catch { }
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

                case Keys.Enter:
                    try
                    {
                        ExitCode = true;
                        try
                        {
                            DialogResult = DialogResult.Yes;
                        }
                        catch { }
                        FormUtil.CloseDialogOk(this, DialogResult.Yes);
                        return true;
                    }
                    catch
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Form base
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams shadowParams = base.CreateParams;
                shadowParams.ClassStyle |= 0x00020000;
                return shadowParams;
            }
        }

        private void InitializeMessageBoxForm()
        {
            Settings.Default.TxtNotif = LabelMessage.Text;
            int NotifHeight = CountLines(LabelMessage.Text);
            Height = NotifHeight * 24 + 82;
            PanelBody.Height = NotifHeight * 24 + 19;
            BtnYes.HoverState.FillColor = Color.SeaGreen;
        }

        public void ApplyBoxHeight()
        {
            try
            {
                if (BoxHeight <= 0)
                    return;
                try
                {
                    LabelMessage.AutoSize = false;
                }
                catch { }
                LabelMessage.Height = BoxHeight;
                int bodyHeight = BoxHeight + 24;
                int chrome = Math.Max(0, this.Height - PanelBody.Height);
                PanelBody.Height = bodyHeight;
                this.Height = Math.Max(120, chrome + PanelBody.Height);
                PanelBody.Visible = true;
                LabelMessage.Visible = true;
                PanelBody.PerformLayout();
                LabelMessage.Refresh();
            }
            catch { }
        }
        #endregion

        #region Functions
        private int CountLines(string text)
        {
            int count = 1;
            int position = -1;

            while ((position = text.IndexOf('\n', position + 1)) != -1)
            {
                count++;
            }
            return count;
        }

        private void InitializeTimers()
        {
            fadeTimer.Interval = 50;
            fadeTimer.Tick += FadeOut;
        }

        private void FadeClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            fadeTimer.Start();
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                fadeTimer.Stop();
                if (!ExitCode)
                {
                    DialogResult = DialogResult.No;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                }
            }
            else
                Opacity -= 0.3;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (fadeTimer.Enabled)
                e.Cancel = true;
            else
                base.OnFormClosing(e);
        }
        #endregion

        #region Button functions
        private void BtnClose_Click(object sender, EventArgs e)
        {
            FormUtil.Close(this);
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            try
            {
                ExitCode = true;
                FormUtil.CloseDialogOk(this, DialogResult.Yes);
            }
            catch
            {
                ExitCode = true;
                try
                {
                    DialogResult = DialogResult.Yes;
                }
                catch { }
                FadeClosing(new FormClosingEventArgs(CloseReason.None, false));
            }
        }
        #endregion
    }
}
