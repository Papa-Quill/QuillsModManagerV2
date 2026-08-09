namespace QuillsModManagerV2.UserControls
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelAboutHelp = new System.Windows.Forms.Label();
            this.BtnGithub = new Guna.UI2.WinForms.Guna2Button();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.FormAboutEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.SuspendLayout();
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
            this.BtnClose.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnClose.IconColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(338, 11);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 8;
            this.BtnClose.UseTransparentBackground = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
                                             this.PanelHeader.AccessibleName = "PanelHeader";
            this.PanelHeader.AutoSize = true;
            this.PanelHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelHeader.BorderThickness = 1;
            this.PanelHeader.Controls.Add(this.ImgIcon);
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
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Padding = new System.Windows.Forms.Padding(1);
            this.PanelHeader.Size = new System.Drawing.Size(366, 34);
            this.PanelHeader.TabIndex = 6;
                                             this.ImgIcon.AccessibleName = "ImgIcon";
            this.ImgIcon.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.ImgIcon.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ImgIcon.Image = global::QuillsModManagerV2.Properties.Resources.AnimatedQuillDiamond;
            this.ImgIcon.Location = new System.Drawing.Point(7, 3);
            this.ImgIcon.Name = "ImgIcon";
            this.ImgIcon.Size = new System.Drawing.Size(30, 30);
            this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgIcon.TabIndex = 0;
            this.ImgIcon.TabStop = false;
                                             this.PanelBody.AccessibleName = "PanelBody";
            this.PanelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBody.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelBody.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBody.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBody.BorderThickness = 1;
            this.PanelBody.Controls.Add(this.LabelAboutHelp);
            this.PanelBody.Controls.Add(this.BtnGithub);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.Location = new System.Drawing.Point(10, 53);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Size = new System.Drawing.Size(366, 177);
            this.PanelBody.TabIndex = 7;
                                             this.LabelAboutHelp.AccessibleName = "LabelAboutHelp";
            this.LabelAboutHelp.BackColor = System.Drawing.Color.Transparent;
            this.LabelAboutHelp.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelAboutHelp.Font = new System.Drawing.Font("Gadugi", 9F);
            this.LabelAboutHelp.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelAboutHelp.Location = new System.Drawing.Point(10, 10);
            this.LabelAboutHelp.MaximumSize = new System.Drawing.Size(522, 1000);
            this.LabelAboutHelp.Name = "LabelAboutHelp";
            this.LabelAboutHelp.Size = new System.Drawing.Size(345, 100);
            this.LabelAboutHelp.TabIndex = 18;
            this.LabelAboutHelp.Text = resources.GetString("LabelAboutHelp.Text");
            this.LabelAboutHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                                             this.BtnGithub.AccessibleDescription = "Open the Github page for QMMv2";
            this.BtnGithub.AccessibleName = "BtnGithub";
            this.BtnGithub.Animated = true;
            this.BtnGithub.BackColor = System.Drawing.Color.Transparent;
            this.BtnGithub.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnGithub.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnGithub.BorderThickness = 1;
            this.BtnGithub.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnGithub.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGithub.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnGithub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGithub.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGithub.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGithub.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGithub.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGithub.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnGithub.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnGithub.ForeColor = System.Drawing.Color.White;
            this.BtnGithub.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnGithub.Image = ((System.Drawing.Image)(resources.GetObject("BtnGithub.Image")));
            this.BtnGithub.Location = new System.Drawing.Point(129, 121);
            this.BtnGithub.Name = "BtnGithub";
            this.BtnGithub.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnGithub.Size = new System.Drawing.Size(108, 45);
            this.BtnGithub.TabIndex = 3;
            this.BtnGithub.UseTransparentBackground = true;
            this.BtnGithub.Click += new System.EventHandler(this.BtnGithub_Click);
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
            this.LabelTitle.TabIndex = 5;
            this.LabelTitle.Text = "QMM / ABOUT";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                                             this.FormAboutEProperties.ContainerControl = this;
            this.FormAboutEProperties.DockForm = false;
            this.FormAboutEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormAboutEProperties.DragForm = false;
            this.FormAboutEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormAboutEProperties.TransparentWhileDrag = true;
                                             this.AccessibleName = "FormAbout";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(387, 240);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelBody);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAbout";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 11, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About";
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.PanelBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Util.Controls.CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private System.Windows.Forms.PictureBox ImgIcon;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private Guna.UI2.WinForms.Guna2Button BtnGithub;
        private System.Windows.Forms.Label LabelAboutHelp;
        private Guna.UI2.WinForms.Guna2BorderlessForm FormAboutEProperties;
    }
}