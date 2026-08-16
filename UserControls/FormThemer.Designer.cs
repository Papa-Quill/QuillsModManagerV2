namespace QuillsModManagerV2.UserControls
{
    partial class FormThemer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThemer));
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.BtnResetTheme = new Guna.UI2.WinForms.Guna2Button();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.FormThemerEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelColorsHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelColorsTitle = new System.Windows.Forms.Label();
            this.PanelPresetsHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnAddPreset = new Guna.UI2.WinForms.Guna2Button();
            this.LabelPresetsTitle = new System.Windows.Forms.Label();
            this.PanelColors = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelColorsContent = new System.Windows.Forms.Panel();
            this.vScrollColors = new QuillsModManagerV2.Util.Controls.CustomVScrollBar();
            this.PanelPresets = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelPresetsContent = new System.Windows.Forms.Panel();
            this.vScrollPresets = new QuillsModManagerV2.Util.Controls.CustomVScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.PanelColorsHeader.SuspendLayout();
            this.PanelPresetsHeader.SuspendLayout();
            this.PanelColors.SuspendLayout();
            this.PanelColorsContent.SuspendLayout();
            this.PanelPresets.SuspendLayout();
            this.PanelPresetsContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImgIcon
            // 
            this.ImgIcon.AccessibleName = "ImgIcon";
            this.ImgIcon.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.ImgIcon.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ImgIcon.Image = global::QuillsModManagerV2.Properties.Resources.AnimatedQuillDiamond;
            this.ImgIcon.Location = new System.Drawing.Point(16, 12);
            this.ImgIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ImgIcon.MaximumSize = new System.Drawing.Size(30, 30);
            this.ImgIcon.MinimumSize = new System.Drawing.Size(30, 30);
            this.ImgIcon.Name = "ImgIcon";
            this.ImgIcon.Size = new System.Drawing.Size(30, 30);
            this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgIcon.TabIndex = 20;
            this.ImgIcon.TabStop = false;
            // 
            // BtnResetTheme
            // 
            this.BtnResetTheme.AccessibleDescription = "Reset your theme to default.";
            this.BtnResetTheme.AccessibleName = "BtnResetTheme";
            this.BtnResetTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnResetTheme.Animated = true;
            this.BtnResetTheme.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.BtnResetTheme.CustomizableEdges.BottomLeft = false;
            this.BtnResetTheme.CustomizableEdges.BottomRight = false;
            this.BtnResetTheme.CustomizableEdges.TopLeft = false;
            this.BtnResetTheme.CustomizableEdges.TopRight = false;
            this.BtnResetTheme.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnResetTheme.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnResetTheme.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnResetTheme.FocusedColor = System.Drawing.Color.Gray;
            this.BtnResetTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnResetTheme.ForeColor = System.Drawing.Color.White;
            this.BtnResetTheme.HoverState.FillColor = System.Drawing.Color.Gray;
            this.BtnResetTheme.Image = ((System.Drawing.Image)(resources.GetObject("BtnResetTheme.Image")));
            this.BtnResetTheme.Location = new System.Drawing.Point(511, 11);
            this.BtnResetTheme.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnResetTheme.Name = "BtnResetTheme";
            this.BtnResetTheme.Size = new System.Drawing.Size(32, 32);
            this.BtnResetTheme.TabIndex = 0;
            this.BtnResetTheme.Click += new System.EventHandler(this.BtnResetTheme_Click);
            this.BtnResetTheme.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
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
            this.LabelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(109, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "QMM / THEME";
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
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("IconColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnClose.IconColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnClose.Location = new System.Drawing.Point(544, 11);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.UseTransparentBackground = true;
            // 
            // PanelHeader
            // 
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
            this.PanelHeader.Size = new System.Drawing.Size(572, 34);
            this.PanelHeader.TabIndex = 0;
            // 
            // FormThemerEProperties
            // 
            this.FormThemerEProperties.ContainerControl = this;
            this.FormThemerEProperties.DockForm = false;
            this.FormThemerEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormThemerEProperties.DragForm = false;
            this.FormThemerEProperties.ResizeForm = false;
            this.FormThemerEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormThemerEProperties.TransparentWhileDrag = true;
            // 
            // PanelBody
            // 
            this.PanelBody.AccessibleName = "PanelBody";
            this.PanelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBody.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelBody.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBody.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBody.BorderThickness = 1;
            this.PanelBody.Controls.Add(this.PanelColorsHeader);
            this.PanelBody.Controls.Add(this.PanelPresetsHeader);
            this.PanelBody.Controls.Add(this.PanelColors);
            this.PanelBody.Controls.Add(this.PanelPresets);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBody.Location = new System.Drawing.Point(10, 54);
            this.PanelBody.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Padding = new System.Windows.Forms.Padding(9);
            this.PanelBody.Size = new System.Drawing.Size(572, 257);
            this.PanelBody.TabIndex = 0;
            // 
            // PanelColorsHeader
            // 
            this.PanelColorsHeader.AccessibleName = "PanelColorsHeader";
            this.PanelColorsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelColorsHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelColorsHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelColorsHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelColorsHeader.BorderThickness = 1;
            this.PanelColorsHeader.Controls.Add(this.LabelColorsTitle);
            this.PanelColorsHeader.CustomizableEdges.BottomLeft = false;
            this.PanelColorsHeader.CustomizableEdges.BottomRight = false;
            this.PanelColorsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColorsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColorsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColorsHeader.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColorsHeader.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.PanelColorsHeader.Location = new System.Drawing.Point(11, 11);
            this.PanelColorsHeader.Name = "PanelColorsHeader";
            this.PanelColorsHeader.Size = new System.Drawing.Size(270, 20);
            this.PanelColorsHeader.TabIndex = 0;
            // 
            // LabelColorsTitle
            // 
            this.LabelColorsTitle.AccessibleName = "LabelColorsTitle";
            this.LabelColorsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelColorsTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelColorsTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelColorsTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelColorsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelColorsTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelColorsTitle.Location = new System.Drawing.Point(26, 2);
            this.LabelColorsTitle.Name = "LabelColorsTitle";
            this.LabelColorsTitle.Size = new System.Drawing.Size(218, 15);
            this.LabelColorsTitle.TabIndex = 0;
            this.LabelColorsTitle.Text = "Theme Settings";
            this.LabelColorsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelPresetsHeader
            // 
            this.PanelPresetsHeader.AccessibleName = "PanelPresetsHeader";
            this.PanelPresetsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPresetsHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelPresetsHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelPresetsHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelPresetsHeader.BorderThickness = 1;
            this.PanelPresetsHeader.Controls.Add(this.BtnAddPreset);
            this.PanelPresetsHeader.Controls.Add(this.LabelPresetsTitle);
            this.PanelPresetsHeader.CustomizableEdges.BottomLeft = false;
            this.PanelPresetsHeader.CustomizableEdges.BottomRight = false;
            this.PanelPresetsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresetsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresetsHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresetsHeader.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresetsHeader.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.PanelPresetsHeader.Location = new System.Drawing.Point(291, 11);
            this.PanelPresetsHeader.Name = "PanelPresetsHeader";
            this.PanelPresetsHeader.Size = new System.Drawing.Size(270, 20);
            this.PanelPresetsHeader.TabIndex = 0;
            // 
            // BtnAddPreset
            // 
            this.BtnAddPreset.AccessibleDescription = "Add current theme to presets.";
            this.BtnAddPreset.AccessibleName = "BtnAddPreset";
            this.BtnAddPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddPreset.Animated = true;
            this.BtnAddPreset.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnAddPreset.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnAddPreset.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnAddPreset.BorderThickness = 1;
            this.BtnAddPreset.CustomizableEdges.BottomLeft = false;
            this.BtnAddPreset.CustomizableEdges.BottomRight = false;
            this.BtnAddPreset.CustomizableEdges.TopLeft = false;
            this.BtnAddPreset.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnAddPreset.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnAddPreset.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnAddPreset.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnAddPreset.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnAddPreset.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnAddPreset.FocusedColor = System.Drawing.Color.Gray;
            this.BtnAddPreset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnAddPreset.ForeColor = System.Drawing.Color.White;
            this.BtnAddPreset.HoverState.FillColor = System.Drawing.Color.Gray;
            this.BtnAddPreset.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddPreset.Image")));
            this.BtnAddPreset.ImageSize = new System.Drawing.Size(16, 16);
            this.BtnAddPreset.Location = new System.Drawing.Point(250, 0);
            this.BtnAddPreset.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAddPreset.Name = "BtnAddPreset";
            this.BtnAddPreset.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnAddPreset.Size = new System.Drawing.Size(20, 20);
            this.BtnAddPreset.TabIndex = 16;
            // 
            // LabelPresetsTitle
            // 
            this.LabelPresetsTitle.AccessibleName = "LabelPresetsTitle";
            this.LabelPresetsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPresetsTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelPresetsTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelPresetsTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelPresetsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPresetsTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelPresetsTitle.Location = new System.Drawing.Point(26, 2);
            this.LabelPresetsTitle.Name = "LabelPresetsTitle";
            this.LabelPresetsTitle.Size = new System.Drawing.Size(218, 15);
            this.LabelPresetsTitle.TabIndex = 0;
            this.LabelPresetsTitle.Text = "Theme Presets";
            this.LabelPresetsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelColors
            // 
            this.PanelColors.AccessibleName = "PanelColors";
            this.PanelColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelColors.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelColors.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelColors.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelColors.BorderThickness = 1;
            this.PanelColors.Controls.Add(this.PanelColorsContent);
            this.PanelColors.CustomizableEdges.TopLeft = false;
            this.PanelColors.CustomizableEdges.TopRight = false;
            this.PanelColors.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColors.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColors.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColors.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColors.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelColors.Location = new System.Drawing.Point(11, 30);
            this.PanelColors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelColors.Name = "PanelColors";
            this.PanelColors.Padding = new System.Windows.Forms.Padding(9);
            this.PanelColors.Size = new System.Drawing.Size(270, 216);
            this.PanelColors.TabIndex = 0;
            // 
            // PanelColorsContent
            // 
            this.PanelColorsContent.AccessibleName = "PanelColorsContent";
            this.PanelColorsContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelColorsContent.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelColorsContent.Controls.Add(this.vScrollColors);
            this.PanelColorsContent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelColorsContent.Location = new System.Drawing.Point(11, 11);
            this.PanelColorsContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelColorsContent.Name = "PanelColorsContent";
            this.PanelColorsContent.Size = new System.Drawing.Size(248, 194);
            this.PanelColorsContent.TabIndex = 0;
            // 
            // vScrollColors
            // 
            this.vScrollColors.AccessibleName = "vScrollColors";
            this.vScrollColors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.vScrollColors.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollColors.ForeColor = System.Drawing.Color.White;
            this.vScrollColors.LargeChange = 10;
            this.vScrollColors.Location = new System.Drawing.Point(236, 0);
            this.vScrollColors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vScrollColors.Maximum = 100;
            this.vScrollColors.Minimum = 0;
            this.vScrollColors.Name = "vScrollColors";
            this.vScrollColors.Size = new System.Drawing.Size(12, 194);
            this.vScrollColors.SmallChange = 1;
            this.vScrollColors.TabIndex = 0;
            this.vScrollColors.Value = 0;
            // 
            // PanelPresets
            // 
            this.PanelPresets.AccessibleName = "PanelPresets";
            this.PanelPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPresets.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelPresets.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelPresets.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelPresets.BorderThickness = 1;
            this.PanelPresets.Controls.Add(this.PanelPresetsContent);
            this.PanelPresets.CustomizableEdges.TopLeft = false;
            this.PanelPresets.CustomizableEdges.TopRight = false;
            this.PanelPresets.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresets.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresets.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresets.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresets.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelPresets.Location = new System.Drawing.Point(291, 30);
            this.PanelPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelPresets.Name = "PanelPresets";
            this.PanelPresets.Padding = new System.Windows.Forms.Padding(9);
            this.PanelPresets.Size = new System.Drawing.Size(270, 216);
            this.PanelPresets.TabIndex = 0;
            // 
            // PanelPresetsContent
            // 
            this.PanelPresetsContent.AccessibleName = "PanelPresetsContent";
            this.PanelPresetsContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPresetsContent.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelPresetsContent.Controls.Add(this.vScrollPresets);
            this.PanelPresetsContent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelPresetsContent.Location = new System.Drawing.Point(11, 11);
            this.PanelPresetsContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelPresetsContent.Name = "PanelPresetsContent";
            this.PanelPresetsContent.Size = new System.Drawing.Size(248, 194);
            this.PanelPresetsContent.TabIndex = 0;
            // 
            // vScrollPresets
            // 
            this.vScrollPresets.AccessibleName = "vScrollPresets";
            this.vScrollPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.vScrollPresets.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollPresets.ForeColor = System.Drawing.Color.White;
            this.vScrollPresets.LargeChange = 10;
            this.vScrollPresets.Location = new System.Drawing.Point(236, 0);
            this.vScrollPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vScrollPresets.Maximum = 100;
            this.vScrollPresets.Minimum = 0;
            this.vScrollPresets.Name = "vScrollPresets";
            this.vScrollPresets.Size = new System.Drawing.Size(12, 194);
            this.vScrollPresets.SmallChange = 1;
            this.vScrollPresets.TabIndex = 0;
            this.vScrollPresets.Value = 0;
            // 
            // FormThemer
            // 
            this.AccessibleName = "FormThemer";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(592, 321);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.ImgIcon);
            this.Controls.Add(this.BtnResetTheme);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormThemer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormThemer";
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.PanelBody.ResumeLayout(false);
            this.PanelColorsHeader.ResumeLayout(false);
            this.PanelPresetsHeader.ResumeLayout(false);
            this.PanelColors.ResumeLayout(false);
            this.PanelColorsContent.ResumeLayout(false);
            this.PanelPresets.ResumeLayout(false);
            this.PanelPresetsContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImgIcon;
        private Guna.UI2.WinForms.Guna2Button BtnResetTheme;
        public Util.Controls.CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private Guna.UI2.WinForms.Guna2BorderlessForm FormThemerEProperties;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private Guna.UI2.WinForms.Guna2Panel PanelPresetsHeader;
        private System.Windows.Forms.Label LabelPresetsTitle;
        private Guna.UI2.WinForms.Guna2Panel PanelPresets;
        private System.Windows.Forms.Panel PanelPresetsContent;
        private Util.Controls.CustomVScrollBar vScrollPresets;
        private Guna.UI2.WinForms.Guna2Button BtnAddPreset;
        private Guna.UI2.WinForms.Guna2Panel PanelColorsHeader;
        private System.Windows.Forms.Label LabelColorsTitle;
        private Guna.UI2.WinForms.Guna2Panel PanelColors;
        private System.Windows.Forms.Panel PanelColorsContent;
        private Util.Controls.CustomVScrollBar vScrollColors;
    }
}