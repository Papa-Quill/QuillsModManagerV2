namespace QuillsModManagerV2.InfoForms
{
    partial class FormMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessageBox));
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelMessage = new System.Windows.Forms.Label();
            this.BtnNo = new Guna.UI2.WinForms.Guna2ControlBox();
            this.BtnYes = new Guna.UI2.WinForms.Guna2Button();
            this.FormMessageBoxEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelTitle
            // 
            this.LabelTitle.AccessibleName = "LabelTitle";
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.Enabled = false;
            this.LabelTitle.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold);
            this.LabelTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTitle.Location = new System.Drawing.Point(54, 16);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(100, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "LabelTitle";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnClose
            // 
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
            this.BtnClose.Location = new System.Drawing.Point(512, 11);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 8;
            this.BtnClose.UseTransparentBackground = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PanelHeader
            // 
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
            this.PanelHeader.Size = new System.Drawing.Size(540, 34);
            this.PanelHeader.TabIndex = 0;
            // 
            // ImgIcon
            // 
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
            // 
            // PanelBody
            // 
            this.PanelBody.AccessibleName = "PanelBody";
            this.PanelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBody.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBody.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBody.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBody.BorderThickness = 1;
            this.PanelBody.Controls.Add(this.LabelMessage);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.PanelBody.Location = new System.Drawing.Point(10, 53);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Size = new System.Drawing.Size(540, 209);
            this.PanelBody.TabIndex = 0;
            // 
            // LabelMessage
            // 
            this.LabelMessage.AccessibleName = "LabelMessage";
            this.LabelMessage.AutoSize = true;
            this.LabelMessage.BackColor = System.Drawing.Color.Transparent;
            this.LabelMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::QuillsModManagerV2.Properties.Settings.Default, "TxtNotif", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelMessage.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LabelMessage.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelMessage.Location = new System.Drawing.Point(10, 10);
            this.LabelMessage.MaximumSize = new System.Drawing.Size(522, 1000);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(64, 21);
            this.LabelMessage.TabIndex = 0;
            this.LabelMessage.Text = global::QuillsModManagerV2.Properties.Settings.Default.TxtNotif;
            // 
            // BtnNo
            // 
            this.BtnNo.AccessibleName = "BtnNo";
            this.BtnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNo.Animated = true;
            this.BtnNo.BackColor = System.Drawing.Color.Transparent;
            this.BtnNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))));
            this.BtnNo.CustomClick = true;
            this.BtnNo.CustomizableEdges.BottomLeft = false;
            this.BtnNo.CustomizableEdges.TopLeft = false;
            this.BtnNo.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnNo.DataBindings.Add(new System.Windows.Forms.Binding("IconColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnNo.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnNo.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnNo.IconColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnNo.Location = new System.Drawing.Point(474, 11);
            this.BtnNo.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnNo.Name = "BtnNo";
            this.BtnNo.Size = new System.Drawing.Size(37, 32);
            this.BtnNo.TabIndex = 0;
            this.BtnNo.UseTransparentBackground = true;
            this.BtnNo.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnYes
            // 
            this.BtnYes.AccessibleName = "BtnYes";
            this.BtnYes.Animated = true;
            this.BtnYes.BackColor = System.Drawing.Color.Transparent;
            this.BtnYes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))));
            this.BtnYes.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnYes.CustomizableEdges.BottomLeft = false;
            this.BtnYes.CustomizableEdges.TopLeft = false;
            this.BtnYes.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnYes.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnYes.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnYes.FocusedColor = System.Drawing.Color.Gray;
            this.BtnYes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnYes.ForeColor = System.Drawing.Color.White;
            this.BtnYes.HoverState.FillColor = System.Drawing.Color.SeaGreen;
            this.BtnYes.Image = ((System.Drawing.Image)(resources.GetObject("BtnYes.Image")));
            this.BtnYes.ImageSize = new System.Drawing.Size(14, 12);
            this.BtnYes.Location = new System.Drawing.Point(512, 11);
            this.BtnYes.Name = "BtnYes";
            this.BtnYes.Size = new System.Drawing.Size(37, 32);
            this.BtnYes.TabIndex = 0;
            this.BtnYes.UseTransparentBackground = true;
            this.BtnYes.Click += new System.EventHandler(this.BtnYes_Click);
            // 
            // FormMessageBoxEProperties
            // 
            this.FormMessageBoxEProperties.ContainerControl = this;
            this.FormMessageBoxEProperties.DockForm = false;
            this.FormMessageBoxEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormMessageBoxEProperties.DragForm = false;
            this.FormMessageBoxEProperties.ResizeForm = false;
            this.FormMessageBoxEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormMessageBoxEProperties.TransparentWhileDrag = true;
            // 
            // FormMessageBox
            // 
            this.AccessibleName = "FormMessageBox";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(561, 272);
            this.Controls.Add(this.BtnYes);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnNo);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelBody);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMessageBox";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 11, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Box";
            this.TopMost = true;
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.PanelBody.ResumeLayout(false);
            this.PanelBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Util.Controls.CClickThroughLabel LabelTitle;
        public Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private System.Windows.Forms.PictureBox ImgIcon;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private System.Windows.Forms.Label LabelMessage;
        public Guna.UI2.WinForms.Guna2ControlBox BtnNo;
        public Guna.UI2.WinForms.Guna2Button BtnYes;
        private Guna.UI2.WinForms.Guna2BorderlessForm FormMessageBoxEProperties;
    }
}