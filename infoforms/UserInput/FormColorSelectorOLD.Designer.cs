namespace QuillsModManagerV2.InfoForms.UserInput
{
    partial class FormColorSelectorOLD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColorSelectorOLD));
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.TxtColorInputRGB = new Guna.UI2.WinForms.Guna2TextBox();
            this.TxtColorInputHex = new Guna.UI2.WinForms.Guna2TextBox();
            this.ScrollBarBlue = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.ScrollBarGreen = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.ScrollBarRed = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.BtnColorPreview = new Guna.UI2.WinForms.Guna2Button();
            this.BtnColorPicker = new Guna.UI2.WinForms.Guna2Button();
            this.BtnFinishSelection = new Guna.UI2.WinForms.Guna2Button();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.SuspendLayout();
                                             this.BtnClose.AccessibleName = "BtnClose";
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Animated = true;
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))));
            this.BtnClose.CustomClick = true;
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnClose.IconColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(314, 11);
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
            this.PanelHeader.Size = new System.Drawing.Size(379, 34);
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
            this.PanelBody.Controls.Add(this.TxtColorInputRGB);
            this.PanelBody.Controls.Add(this.TxtColorInputHex);
            this.PanelBody.Controls.Add(this.ScrollBarBlue);
            this.PanelBody.Controls.Add(this.ScrollBarGreen);
            this.PanelBody.Controls.Add(this.ScrollBarRed);
            this.PanelBody.Controls.Add(this.BtnColorPreview);
            this.PanelBody.Controls.Add(this.BtnColorPicker);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.Location = new System.Drawing.Point(10, 53);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Size = new System.Drawing.Size(379, 206);
            this.PanelBody.TabIndex = 7;
                                             this.TxtColorInputRGB.AccessibleName = "TxtColorInputRGB";
            this.TxtColorInputRGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtColorInputRGB.Animated = true;
            this.TxtColorInputRGB.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.TxtColorInputRGB.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.TxtColorInputRGB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("PlaceholderForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputRGB.DefaultText = "";
            this.TxtColorInputRGB.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtColorInputRGB.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtColorInputRGB.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtColorInputRGB.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtColorInputRGB.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.TxtColorInputRGB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.TxtColorInputRGB.Font = new System.Drawing.Font("Gadugi", 9F);
            this.TxtColorInputRGB.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TxtColorInputRGB.Location = new System.Drawing.Point(195, 95);
            this.TxtColorInputRGB.MaxLength = 255;
            this.TxtColorInputRGB.Name = "TxtColorInputRGB";
            this.TxtColorInputRGB.PasswordChar = '\0';
            this.TxtColorInputRGB.PlaceholderForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.TxtColorInputRGB.PlaceholderText = "Color I/O RGB";
            this.TxtColorInputRGB.SelectedText = "";
            this.TxtColorInputRGB.Size = new System.Drawing.Size(173, 45);
            this.TxtColorInputRGB.TabIndex = 10;
            this.TxtColorInputRGB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtColorInputRGB.Leave += new System.EventHandler(this.TxtColorInputRGB_Leave);
                                             this.TxtColorInputHex.AccessibleName = "TxtColorInputHex";
            this.TxtColorInputHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtColorInputHex.Animated = true;
            this.TxtColorInputHex.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.TxtColorInputHex.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.TxtColorInputHex.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("PlaceholderForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtColorInputHex.DefaultText = "";
            this.TxtColorInputHex.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtColorInputHex.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtColorInputHex.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtColorInputHex.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtColorInputHex.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.TxtColorInputHex.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.TxtColorInputHex.Font = new System.Drawing.Font("Gadugi", 9F);
            this.TxtColorInputHex.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TxtColorInputHex.Location = new System.Drawing.Point(11, 95);
            this.TxtColorInputHex.MaxLength = 255;
            this.TxtColorInputHex.Name = "TxtColorInputHex";
            this.TxtColorInputHex.PasswordChar = '\0';
            this.TxtColorInputHex.PlaceholderForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.TxtColorInputHex.PlaceholderText = "Color I/O Hex";
            this.TxtColorInputHex.SelectedText = "";
            this.TxtColorInputHex.Size = new System.Drawing.Size(173, 45);
            this.TxtColorInputHex.TabIndex = 10;
            this.TxtColorInputHex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtColorInputHex.Leave += new System.EventHandler(this.TxtColorInputHex_Leave);
                                             this.ScrollBarBlue.AccessibleName = "ScrollBarBlue";
            this.ScrollBarBlue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollBarBlue.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.ScrollBarBlue.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.ScrollBarBlue.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarBlue.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarBlue.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarBlue.DataBindings.Add(new System.Windows.Forms.Binding("ThumbColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarBlue.FillColor = System.Drawing.Color.Black;
            this.ScrollBarBlue.InUpdate = false;
            this.ScrollBarBlue.LargeChange = 10;
            this.ScrollBarBlue.Location = new System.Drawing.Point(11, 67);
            this.ScrollBarBlue.Maximum = 255;
            this.ScrollBarBlue.Name = "ScrollBarBlue";
            this.ScrollBarBlue.ScrollbarSize = 18;
            this.ScrollBarBlue.Size = new System.Drawing.Size(357, 18);
            this.ScrollBarBlue.TabIndex = 4;
            this.ScrollBarBlue.ThumbColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.ScrollBarBlue.ThumbSize = 15F;
            this.ScrollBarBlue.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            this.ScrollBarBlue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
                                             this.ScrollBarGreen.AccessibleName = "ScrollBarGreen";
            this.ScrollBarGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollBarGreen.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.ScrollBarGreen.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.ScrollBarGreen.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarGreen.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarGreen.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarGreen.DataBindings.Add(new System.Windows.Forms.Binding("ThumbColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarGreen.FillColor = System.Drawing.Color.Black;
            this.ScrollBarGreen.InUpdate = false;
            this.ScrollBarGreen.LargeChange = 10;
            this.ScrollBarGreen.Location = new System.Drawing.Point(11, 39);
            this.ScrollBarGreen.Maximum = 255;
            this.ScrollBarGreen.Name = "ScrollBarGreen";
            this.ScrollBarGreen.ScrollbarSize = 18;
            this.ScrollBarGreen.Size = new System.Drawing.Size(357, 18);
            this.ScrollBarGreen.TabIndex = 4;
            this.ScrollBarGreen.ThumbColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.ScrollBarGreen.ThumbSize = 15F;
            this.ScrollBarGreen.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            this.ScrollBarGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
                                             this.ScrollBarRed.AccessibleName = "ScrollBarRed";
            this.ScrollBarRed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollBarRed.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.ScrollBarRed.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.ScrollBarRed.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarRed.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarRed.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarRed.DataBindings.Add(new System.Windows.Forms.Binding("ThumbColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ScrollBarRed.FillColor = System.Drawing.Color.Black;
            this.ScrollBarRed.InUpdate = false;
            this.ScrollBarRed.LargeChange = 10;
            this.ScrollBarRed.Location = new System.Drawing.Point(11, 11);
            this.ScrollBarRed.Maximum = 255;
            this.ScrollBarRed.Name = "ScrollBarRed";
            this.ScrollBarRed.ScrollbarSize = 18;
            this.ScrollBarRed.Size = new System.Drawing.Size(357, 18);
            this.ScrollBarRed.TabIndex = 4;
            this.ScrollBarRed.ThumbColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.ScrollBarRed.ThumbSize = 15F;
            this.ScrollBarRed.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            this.ScrollBarRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
                                             this.BtnColorPreview.AccessibleName = "BtnColorPreview";
            this.BtnColorPreview.Animated = true;
            this.BtnColorPreview.BackColor = System.Drawing.Color.Transparent;
            this.BtnColorPreview.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnColorPreview.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnColorPreview.BorderThickness = 1;
            this.BtnColorPreview.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnColorPreview.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColorPreview.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnColorPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnColorPreview.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnColorPreview.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnColorPreview.FillColor = System.Drawing.Color.Black;
            this.BtnColorPreview.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnColorPreview.ForeColor = System.Drawing.Color.White;
            this.BtnColorPreview.Location = new System.Drawing.Point(66, 150);
            this.BtnColorPreview.Name = "BtnColorPreview";
            this.BtnColorPreview.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnColorPreview.Size = new System.Drawing.Size(302, 45);
            this.BtnColorPreview.TabIndex = 1;
            this.BtnColorPreview.UseTransparentBackground = true;
                                             this.BtnColorPicker.AccessibleDescription = "Hold left click on this button then drag your mouse to the color you want on your" +
    " screen.";
            this.BtnColorPicker.AccessibleName = "BtnColorPicker";
            this.BtnColorPicker.Animated = true;
            this.BtnColorPicker.BackColor = System.Drawing.Color.Transparent;
            this.BtnColorPicker.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnColorPicker.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnColorPicker.BorderThickness = 1;
            this.BtnColorPicker.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnColorPicker.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColorPicker.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnColorPicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnColorPicker.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnColorPicker.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnColorPicker.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnColorPicker.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnColorPicker.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnColorPicker.ForeColor = System.Drawing.Color.White;
            this.BtnColorPicker.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("BtnColorPicker.Image")));
            this.BtnColorPicker.Location = new System.Drawing.Point(11, 150);
            this.BtnColorPicker.Name = "BtnColorPicker";
            this.BtnColorPicker.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnColorPicker.Size = new System.Drawing.Size(45, 45);
            this.BtnColorPicker.TabIndex = 0;
            this.BtnColorPicker.UseTransparentBackground = true;
            this.BtnColorPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnColorPicker_MouseDown);
            this.BtnColorPicker.MouseEnter += new System.EventHandler(this.BtnToolTip_MouseEnter);
            this.BtnColorPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnColorPicker_MouseUp);
                                             this.BtnFinishSelection.AccessibleDescription = "Apply selected color to input.";
            this.BtnFinishSelection.AccessibleName = "BtnFinishSelection";
            this.BtnFinishSelection.Animated = true;
            this.BtnFinishSelection.BackColor = System.Drawing.Color.Transparent;
            this.BtnFinishSelection.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnFinishSelection.CustomizableEdges.BottomLeft = false;
            this.BtnFinishSelection.CustomizableEdges.TopLeft = false;
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnFinishSelection.FocusedColor = System.Drawing.Color.Gray;
            this.BtnFinishSelection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnFinishSelection.ForeColor = System.Drawing.Color.White;
            this.BtnFinishSelection.HoverState.FillColor = System.Drawing.Color.SeaGreen;
            this.BtnFinishSelection.Image = ((System.Drawing.Image)(resources.GetObject("BtnFinishSelection.Image")));
            this.BtnFinishSelection.ImageSize = new System.Drawing.Size(14, 12);
            this.BtnFinishSelection.Location = new System.Drawing.Point(351, 11);
            this.BtnFinishSelection.Name = "BtnFinishSelection";
            this.BtnFinishSelection.Size = new System.Drawing.Size(37, 32);
            this.BtnFinishSelection.TabIndex = 34;
            this.BtnFinishSelection.UseTransparentBackground = true;
            this.BtnFinishSelection.Click += new System.EventHandler(this.BtnFinishSelection_Click);
            this.BtnFinishSelection.MouseEnter += new System.EventHandler(this.BtnToolTip_MouseEnter);
                                             this.LabelTitle.AccessibleName = "LabelTitle";
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.Enabled = false;
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTitle.Location = new System.Drawing.Point(52, 11);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(145, 28);
            this.LabelTitle.TabIndex = 5;
            this.LabelTitle.Text = "Color Selector";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                                             this.AccessibleName = "FormColorSelector";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(400, 269);
            this.Controls.Add(this.BtnFinishSelection);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.PanelBody);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormColorSelector";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 11, 10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Color Selector";
            this.TopMost = true;
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
        private Guna.UI2.WinForms.Guna2Button BtnColorPreview;
        private Guna.UI2.WinForms.Guna2Button BtnColorPicker;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarRed;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarBlue;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarGreen;
        private Guna.UI2.WinForms.Guna2TextBox TxtColorInputRGB;
        private Guna.UI2.WinForms.Guna2TextBox TxtColorInputHex;
        private Guna.UI2.WinForms.Guna2Button BtnFinishSelection;
    }
}