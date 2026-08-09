namespace QuillsModManagerV2.Util.Controls.ModManager.Saves
{
    partial class FormSteamUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSteamUser));
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelContent = new System.Windows.Forms.Panel();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.FormSteamUserEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.vScroll = new QuillsModManagerV2.Util.Controls.CustomVScrollBar();
            this.PanelBody.SuspendLayout();
            this.PanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.SuspendLayout();
                                             this.PanelBody.AccessibleName = "PanelBody";
            this.PanelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBody.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelBody.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBody.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBody.BorderThickness = 1;
            this.PanelBody.Controls.Add(this.PanelContent);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBody.Location = new System.Drawing.Point(10, 54);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Padding = new System.Windows.Forms.Padding(8);
            this.PanelBody.Size = new System.Drawing.Size(400, 291);
            this.PanelBody.TabIndex = 22;
                                             this.PanelContent.AccessibleName = "PanelContent";
            this.PanelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelContent.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelContent.Controls.Add(this.vScroll);
            this.PanelContent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelContent.Location = new System.Drawing.Point(8, 8);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(384, 275);
            this.PanelContent.TabIndex = 15;
                                             this.PanelHeader.AccessibleName = "PanelHeader";
            this.PanelHeader.AutoSize = true;
            this.PanelHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelHeader.BorderThickness = 1;
            this.PanelHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelHeader.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Enabled = false;
            this.PanelHeader.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.PanelHeader.Location = new System.Drawing.Point(10, 10);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.PanelHeader.MaximumSize = new System.Drawing.Size(0, 34);
            this.PanelHeader.MinimumSize = new System.Drawing.Size(0, 34);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Padding = new System.Windows.Forms.Padding(1);
            this.PanelHeader.Size = new System.Drawing.Size(400, 34);
            this.PanelHeader.TabIndex = 21;
                                             this.ImgIcon.AccessibleName = "ImgIcon";
            this.ImgIcon.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.ImgIcon.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ImgIcon.Image = global::QuillsModManagerV2.Properties.Resources.AnimatedQuillDiamond;
            this.ImgIcon.Location = new System.Drawing.Point(16, 12);
            this.ImgIcon.Name = "ImgIcon";
            this.ImgIcon.Size = new System.Drawing.Size(30, 30);
            this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgIcon.TabIndex = 25;
            this.ImgIcon.TabStop = false;
                                             this.BtnClose.AccessibleName = "BtnClose";
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Animated = true;
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))));
            this.BtnClose.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnClose.CustomClick = true;
            this.BtnClose.CustomizableEdges.BottomLeft = false;
            this.BtnClose.CustomizableEdges.TopLeft = false;
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("IconColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnClose.IconColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnClose.Location = new System.Drawing.Point(372, 11);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 24;
            this.BtnClose.UseTransparentBackground = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
                                             this.FormSteamUserEProperties.ContainerControl = this;
            this.FormSteamUserEProperties.DockForm = false;
            this.FormSteamUserEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormSteamUserEProperties.DragForm = false;
            this.FormSteamUserEProperties.ResizeForm = false;
            this.FormSteamUserEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormSteamUserEProperties.TransparentWhileDrag = true;
                                             this.LabelTitle.AccessibleName = "LabelTitle";
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.Enabled = false;
            this.LabelTitle.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold);
            this.LabelTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTitle.Location = new System.Drawing.Point(48, 16);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(109, 21);
            this.LabelTitle.TabIndex = 23;
            this.LabelTitle.Text = "QMM / STEAM";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                                             this.vScroll.AccessibleName = "vScroll";
            this.vScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.vScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScroll.ForeColor = System.Drawing.Color.White;
            this.vScroll.LargeChange = 10;
            this.vScroll.Location = new System.Drawing.Point(374, 0);
            this.vScroll.Maximum = 100;
            this.vScroll.Minimum = 0;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(10, 275);
            this.vScroll.SmallChange = 1;
            this.vScroll.TabIndex = 14;
            this.vScroll.Value = 0;
                                             this.AccessibleDescription = "This form is for choosing which Steam account you want to use.";
            this.AccessibleName = "FormSteamUser";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(420, 355);
            this.Controls.Add(this.ImgIcon);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.PanelHeader);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormSteamUser";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormSteamUser";
            this.PanelBody.ResumeLayout(false);
            this.PanelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private System.Windows.Forms.Panel PanelContent;
        private CustomVScrollBar vScroll;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private System.Windows.Forms.PictureBox ImgIcon;
        public CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2BorderlessForm FormSteamUserEProperties;
    }
}