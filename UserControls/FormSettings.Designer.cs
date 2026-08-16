namespace QuillsModManagerV2.UserControls
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.FormSettingsEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.BtnResetSettings = new Guna.UI2.WinForms.Guna2Button();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelBools = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelContent = new System.Windows.Forms.Panel();
            this.vScroll = new QuillsModManagerV2.Util.Controls.CustomVScrollBar();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnChooseGamePath = new Guna.UI2.WinForms.Guna2Button();
            this.BtnChooseUserDataPath = new Guna.UI2.WinForms.Guna2Button();
            this.BtnGamePathLabel = new Guna.UI2.WinForms.Guna2Button();
            this.BtnUserDataPathLabel = new Guna.UI2.WinForms.Guna2Button();
            this.BtnModLibraryPathLabel = new Guna.UI2.WinForms.Guna2Button();
            this.BtnChooseModPath = new Guna.UI2.WinForms.Guna2Button();
            this.BtnTheme = new Guna.UI2.WinForms.Guna2Button();
            this.PanelThemeHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelThemeTitle = new System.Windows.Forms.Label();
            this.PanelTogglesHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelTogglesTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBools.SuspendLayout();
            this.PanelContent.SuspendLayout();
            this.PanelBody.SuspendLayout();
            this.PanelThemeHeader.SuspendLayout();
            this.PanelTogglesHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormSettingsEProperties
            // 
            this.FormSettingsEProperties.ContainerControl = this;
            this.FormSettingsEProperties.DockForm = false;
            this.FormSettingsEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormSettingsEProperties.DragForm = false;
            this.FormSettingsEProperties.DragStartTransparencyValue = 1D;
            this.FormSettingsEProperties.ResizeForm = false;
            this.FormSettingsEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormSettingsEProperties.TransparentWhileDrag = true;
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
            this.ImgIcon.TabIndex = 15;
            this.ImgIcon.TabStop = false;
            // 
            // BtnResetSettings
            // 
            this.BtnResetSettings.AccessibleDescription = "Reset all settings.";
            this.BtnResetSettings.AccessibleName = "BtnResetSettings";
            this.BtnResetSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnResetSettings.Animated = true;
            this.BtnResetSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(31)))));
            this.BtnResetSettings.CustomizableEdges.BottomLeft = false;
            this.BtnResetSettings.CustomizableEdges.BottomRight = false;
            this.BtnResetSettings.CustomizableEdges.TopLeft = false;
            this.BtnResetSettings.CustomizableEdges.TopRight = false;
            this.BtnResetSettings.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnResetSettings.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnResetSettings.FocusedColor = System.Drawing.Color.Gray;
            this.BtnResetSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnResetSettings.ForeColor = System.Drawing.Color.White;
            this.BtnResetSettings.HoverState.FillColor = System.Drawing.Color.Gray;
            this.BtnResetSettings.Image = ((System.Drawing.Image)(resources.GetObject("BtnResetSettings.Image")));
            this.BtnResetSettings.Location = new System.Drawing.Point(506, 11);
            this.BtnResetSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnResetSettings.Name = "BtnResetSettings";
            this.BtnResetSettings.Size = new System.Drawing.Size(32, 32);
            this.BtnResetSettings.TabIndex = 0;
            this.BtnResetSettings.Click += new System.EventHandler(this.BtnResetSettings_Click);
            this.BtnResetSettings.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
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
            this.LabelTitle.Size = new System.Drawing.Size(136, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "QMM / SETTINGS";
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
            this.BtnClose.Location = new System.Drawing.Point(539, 11);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 0;
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
            this.PanelHeader.Size = new System.Drawing.Size(567, 34);
            this.PanelHeader.TabIndex = 0;
            // 
            // PanelBools
            // 
            this.PanelBools.AccessibleName = "PanelBools";
            this.PanelBools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBools.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBools.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBools.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBools.BorderThickness = 1;
            this.PanelBools.Controls.Add(this.PanelContent);
            this.PanelBools.CustomizableEdges.TopLeft = false;
            this.PanelBools.CustomizableEdges.TopRight = false;
            this.PanelBools.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBools.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBools.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBools.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBools.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelBools.Location = new System.Drawing.Point(286, 30);
            this.PanelBools.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelBools.Name = "PanelBools";
            this.PanelBools.Padding = new System.Windows.Forms.Padding(9);
            this.PanelBools.Size = new System.Drawing.Size(270, 216);
            this.PanelBools.TabIndex = 23;
            // 
            // PanelContent
            // 
            this.PanelContent.AccessibleName = "PanelContent";
            this.PanelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelContent.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelContent.Controls.Add(this.vScroll);
            this.PanelContent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelContent.Location = new System.Drawing.Point(11, 11);
            this.PanelContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(248, 194);
            this.PanelContent.TabIndex = 0;
            // 
            // vScroll
            // 
            this.vScroll.AccessibleName = "vScroll";
            this.vScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.vScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScroll.ForeColor = System.Drawing.Color.White;
            this.vScroll.LargeChange = 10;
            this.vScroll.Location = new System.Drawing.Point(236, 0);
            this.vScroll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vScroll.Maximum = 100;
            this.vScroll.Minimum = 0;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(12, 194);
            this.vScroll.SmallChange = 1;
            this.vScroll.TabIndex = 0;
            this.vScroll.Value = 0;
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
            this.PanelBody.Controls.Add(this.BtnChooseGamePath);
            this.PanelBody.Controls.Add(this.BtnChooseUserDataPath);
            this.PanelBody.Controls.Add(this.BtnGamePathLabel);
            this.PanelBody.Controls.Add(this.BtnUserDataPathLabel);
            this.PanelBody.Controls.Add(this.BtnModLibraryPathLabel);
            this.PanelBody.Controls.Add(this.BtnChooseModPath);
            this.PanelBody.Controls.Add(this.BtnTheme);
            this.PanelBody.Controls.Add(this.PanelThemeHeader);
            this.PanelBody.Controls.Add(this.PanelTogglesHeader);
            this.PanelBody.Controls.Add(this.PanelBools);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBody.Location = new System.Drawing.Point(10, 54);
            this.PanelBody.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Padding = new System.Windows.Forms.Padding(9);
            this.PanelBody.Size = new System.Drawing.Size(567, 257);
            this.PanelBody.TabIndex = 0;
            // 
            // BtnChooseGamePath
            // 
            this.BtnChooseGamePath.AccessibleDescription = "Set path to your game directory (castle.exe).";
            this.BtnChooseGamePath.AccessibleName = "BtnChooseGamePath";
            this.BtnChooseGamePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChooseGamePath.Animated = true;
            this.BtnChooseGamePath.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnChooseGamePath.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnChooseGamePath.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnChooseGamePath.BorderThickness = 1;
            this.BtnChooseGamePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChooseGamePath.CustomizableEdges.BottomLeft = false;
            this.BtnChooseGamePath.CustomizableEdges.TopLeft = false;
            this.BtnChooseGamePath.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseGamePath.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseGamePath.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseGamePath.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseGamePath.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseGamePath.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseGamePath.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseGamePath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnChooseGamePath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnChooseGamePath.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnChooseGamePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChooseGamePath.ForeColor = System.Drawing.Color.White;
            this.BtnChooseGamePath.Location = new System.Drawing.Point(239, 11);
            this.BtnChooseGamePath.Name = "BtnChooseGamePath";
            this.BtnChooseGamePath.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnChooseGamePath.Size = new System.Drawing.Size(37, 36);
            this.BtnChooseGamePath.TabIndex = 0;
            this.BtnChooseGamePath.Text = "...";
            this.BtnChooseGamePath.TextOffset = new System.Drawing.Point(0, -4);
            this.BtnChooseGamePath.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnChooseGamePath.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnChooseUserDataPath
            // 
            this.BtnChooseUserDataPath.AccessibleDescription = "Set path to your user data directory (save files).";
            this.BtnChooseUserDataPath.AccessibleName = "BtnChooseUserDataPath";
            this.BtnChooseUserDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChooseUserDataPath.Animated = true;
            this.BtnChooseUserDataPath.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnChooseUserDataPath.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnChooseUserDataPath.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnChooseUserDataPath.BorderThickness = 1;
            this.BtnChooseUserDataPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChooseUserDataPath.CustomizableEdges.BottomLeft = false;
            this.BtnChooseUserDataPath.CustomizableEdges.TopLeft = false;
            this.BtnChooseUserDataPath.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseUserDataPath.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseUserDataPath.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseUserDataPath.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseUserDataPath.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseUserDataPath.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseUserDataPath.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseUserDataPath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnChooseUserDataPath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnChooseUserDataPath.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnChooseUserDataPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChooseUserDataPath.ForeColor = System.Drawing.Color.White;
            this.BtnChooseUserDataPath.Location = new System.Drawing.Point(239, 57);
            this.BtnChooseUserDataPath.Name = "BtnChooseUserDataPath";
            this.BtnChooseUserDataPath.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnChooseUserDataPath.Size = new System.Drawing.Size(37, 36);
            this.BtnChooseUserDataPath.TabIndex = 0;
            this.BtnChooseUserDataPath.Text = "...";
            this.BtnChooseUserDataPath.TextOffset = new System.Drawing.Point(0, -4);
            this.BtnChooseUserDataPath.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnChooseUserDataPath.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnGamePathLabel
            // 
            this.BtnGamePathLabel.AccessibleDescription = "Set path to your game directory (castle.exe).";
            this.BtnGamePathLabel.AccessibleName = "BtnGamePathLabel";
            this.BtnGamePathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGamePathLabel.Animated = true;
            this.BtnGamePathLabel.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnGamePathLabel.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnGamePathLabel.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnGamePathLabel.BorderThickness = 1;
            this.BtnGamePathLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnGamePathLabel.CustomizableEdges.BottomRight = false;
            this.BtnGamePathLabel.CustomizableEdges.TopRight = false;
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnGamePathLabel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnGamePathLabel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnGamePathLabel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnGamePathLabel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnGamePathLabel.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnGamePathLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnGamePathLabel.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.BtnGamePathLabel.Location = new System.Drawing.Point(11, 11);
            this.BtnGamePathLabel.Name = "BtnGamePathLabel";
            this.BtnGamePathLabel.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnGamePathLabel.Size = new System.Drawing.Size(229, 36);
            this.BtnGamePathLabel.TabIndex = 0;
            this.BtnGamePathLabel.Text = "Game Path";
            this.BtnGamePathLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BtnGamePathLabel.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnGamePathLabel.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnUserDataPathLabel
            // 
            this.BtnUserDataPathLabel.AccessibleDescription = "Set path to your user data directory (save files).";
            this.BtnUserDataPathLabel.AccessibleName = "BtnUserDataPathLabel";
            this.BtnUserDataPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnUserDataPathLabel.Animated = true;
            this.BtnUserDataPathLabel.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnUserDataPathLabel.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnUserDataPathLabel.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnUserDataPathLabel.BorderThickness = 1;
            this.BtnUserDataPathLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnUserDataPathLabel.CustomizableEdges.BottomRight = false;
            this.BtnUserDataPathLabel.CustomizableEdges.TopRight = false;
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnUserDataPathLabel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnUserDataPathLabel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnUserDataPathLabel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnUserDataPathLabel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnUserDataPathLabel.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnUserDataPathLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnUserDataPathLabel.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.BtnUserDataPathLabel.Location = new System.Drawing.Point(11, 57);
            this.BtnUserDataPathLabel.Name = "BtnUserDataPathLabel";
            this.BtnUserDataPathLabel.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnUserDataPathLabel.Size = new System.Drawing.Size(229, 36);
            this.BtnUserDataPathLabel.TabIndex = 0;
            this.BtnUserDataPathLabel.Text = "User Data Path";
            this.BtnUserDataPathLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BtnUserDataPathLabel.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnUserDataPathLabel.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnModLibraryPathLabel
            // 
            this.BtnModLibraryPathLabel.AccessibleDescription = "Set path to your mod collection/library (select folder).";
            this.BtnModLibraryPathLabel.AccessibleName = "BtnModLibraryPathLabel";
            this.BtnModLibraryPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnModLibraryPathLabel.Animated = true;
            this.BtnModLibraryPathLabel.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnModLibraryPathLabel.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnModLibraryPathLabel.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnModLibraryPathLabel.BorderThickness = 1;
            this.BtnModLibraryPathLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnModLibraryPathLabel.CustomizableEdges.BottomRight = false;
            this.BtnModLibraryPathLabel.CustomizableEdges.TopRight = false;
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnModLibraryPathLabel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnModLibraryPathLabel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnModLibraryPathLabel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnModLibraryPathLabel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnModLibraryPathLabel.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnModLibraryPathLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnModLibraryPathLabel.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.BtnModLibraryPathLabel.Location = new System.Drawing.Point(11, 103);
            this.BtnModLibraryPathLabel.Name = "BtnModLibraryPathLabel";
            this.BtnModLibraryPathLabel.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnModLibraryPathLabel.Size = new System.Drawing.Size(229, 36);
            this.BtnModLibraryPathLabel.TabIndex = 0;
            this.BtnModLibraryPathLabel.Text = "Mod Library Path";
            this.BtnModLibraryPathLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BtnModLibraryPathLabel.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnModLibraryPathLabel.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnChooseModPath
            // 
            this.BtnChooseModPath.AccessibleDescription = "Set path to your mod collection/library (select folder).";
            this.BtnChooseModPath.AccessibleName = "BtnChooseModPath";
            this.BtnChooseModPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChooseModPath.Animated = true;
            this.BtnChooseModPath.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnChooseModPath.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnChooseModPath.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnChooseModPath.BorderThickness = 1;
            this.BtnChooseModPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnChooseModPath.CustomizableEdges.BottomLeft = false;
            this.BtnChooseModPath.CustomizableEdges.TopLeft = false;
            this.BtnChooseModPath.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseModPath.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseModPath.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseModPath.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseModPath.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnChooseModPath.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseModPath.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnChooseModPath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnChooseModPath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnChooseModPath.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnChooseModPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChooseModPath.ForeColor = System.Drawing.Color.White;
            this.BtnChooseModPath.Location = new System.Drawing.Point(239, 103);
            this.BtnChooseModPath.Name = "BtnChooseModPath";
            this.BtnChooseModPath.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnChooseModPath.Size = new System.Drawing.Size(37, 36);
            this.BtnChooseModPath.TabIndex = 0;
            this.BtnChooseModPath.Text = "...";
            this.BtnChooseModPath.TextOffset = new System.Drawing.Point(0, -4);
            this.BtnChooseModPath.Click += new System.EventHandler(this.BtnChoosePath_Click);
            this.BtnChooseModPath.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnTheme
            // 
            this.BtnTheme.AccessibleDescription = "Customize the theme/look of the appication.";
            this.BtnTheme.AccessibleName = "BtnTheme";
            this.BtnTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTheme.Animated = true;
            this.BtnTheme.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.BtnTheme.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnTheme.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnTheme.BorderThickness = 1;
            this.BtnTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnTheme.CustomizableEdges.TopLeft = false;
            this.BtnTheme.CustomizableEdges.TopRight = false;
            this.BtnTheme.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnTheme.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnTheme.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnTheme.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnTheme.DataBindings.Add(new System.Windows.Forms.Binding("PressedColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailActive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnTheme.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnTheme.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnTheme.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnTheme.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnTheme.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnTheme.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnTheme.ForeColor = System.Drawing.Color.White;
            this.BtnTheme.Image = ((System.Drawing.Image)(resources.GetObject("BtnTheme.Image")));
            this.BtnTheme.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnTheme.Location = new System.Drawing.Point(11, 168);
            this.BtnTheme.Name = "BtnTheme";
            this.BtnTheme.PressedColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.BtnTheme.Size = new System.Drawing.Size(265, 78);
            this.BtnTheme.TabIndex = 0;
            this.BtnTheme.Click += new System.EventHandler(this.BtnTheme_Click);
            this.BtnTheme.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // PanelThemeHeader
            // 
            this.PanelThemeHeader.AccessibleName = "PanelThemeHeader";
            this.PanelThemeHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelThemeHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelThemeHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelThemeHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelThemeHeader.BorderThickness = 1;
            this.PanelThemeHeader.Controls.Add(this.LabelThemeTitle);
            this.PanelThemeHeader.CustomizableEdges.BottomLeft = false;
            this.PanelThemeHeader.CustomizableEdges.BottomRight = false;
            this.PanelThemeHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelThemeHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelThemeHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelThemeHeader.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelThemeHeader.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.PanelThemeHeader.Location = new System.Drawing.Point(11, 149);
            this.PanelThemeHeader.Name = "PanelThemeHeader";
            this.PanelThemeHeader.Size = new System.Drawing.Size(265, 20);
            this.PanelThemeHeader.TabIndex = 0;
            // 
            // LabelThemeTitle
            // 
            this.LabelThemeTitle.AccessibleName = "LabelThemeTitle";
            this.LabelThemeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelThemeTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelThemeTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelThemeTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelThemeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelThemeTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelThemeTitle.Location = new System.Drawing.Point(21, 2);
            this.LabelThemeTitle.Name = "LabelThemeTitle";
            this.LabelThemeTitle.Size = new System.Drawing.Size(218, 15);
            this.LabelThemeTitle.TabIndex = 0;
            this.LabelThemeTitle.Text = "Theme Designer";
            this.LabelThemeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelTogglesHeader
            // 
            this.PanelTogglesHeader.AccessibleName = "PanelTogglesHeader";
            this.PanelTogglesHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelTogglesHeader.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelTogglesHeader.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelTogglesHeader.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelTogglesHeader.BorderThickness = 1;
            this.PanelTogglesHeader.Controls.Add(this.LabelTogglesTitle);
            this.PanelTogglesHeader.CustomizableEdges.BottomLeft = false;
            this.PanelTogglesHeader.CustomizableEdges.BottomRight = false;
            this.PanelTogglesHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelTogglesHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelTogglesHeader.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelTogglesHeader.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelTogglesHeader.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.PanelTogglesHeader.Location = new System.Drawing.Point(286, 11);
            this.PanelTogglesHeader.Name = "PanelTogglesHeader";
            this.PanelTogglesHeader.Size = new System.Drawing.Size(270, 20);
            this.PanelTogglesHeader.TabIndex = 0;
            // 
            // LabelTogglesTitle
            // 
            this.LabelTogglesTitle.AccessibleName = "LabelTogglesTitle";
            this.LabelTogglesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTogglesTitle.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.LabelTogglesTitle.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTogglesTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTogglesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTogglesTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTogglesTitle.Location = new System.Drawing.Point(26, 2);
            this.LabelTogglesTitle.Name = "LabelTogglesTitle";
            this.LabelTogglesTitle.Size = new System.Drawing.Size(218, 15);
            this.LabelTogglesTitle.TabIndex = 27;
            this.LabelTogglesTitle.Text = "Toggle Settings";
            this.LabelTogglesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSettings
            // 
            this.AccessibleName = "FormSettings";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(587, 321);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.ImgIcon);
            this.Controls.Add(this.BtnResetSettings);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormSettings";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormSettings";
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.PanelBools.ResumeLayout(false);
            this.PanelContent.ResumeLayout(false);
            this.PanelBody.ResumeLayout(false);
            this.PanelThemeHeader.ResumeLayout(false);
            this.PanelTogglesHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm FormSettingsEProperties;
        private System.Windows.Forms.PictureBox ImgIcon;
        private Guna.UI2.WinForms.Guna2Button BtnResetSettings;
        public Util.Controls.CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private Guna.UI2.WinForms.Guna2Panel PanelBools;
        private System.Windows.Forms.Panel PanelContent;
        private Util.Controls.CustomVScrollBar vScroll;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private Guna.UI2.WinForms.Guna2Panel PanelTogglesHeader;
        private System.Windows.Forms.Label LabelTogglesTitle;
        private Guna.UI2.WinForms.Guna2Button BtnTheme;
        private Guna.UI2.WinForms.Guna2Panel PanelThemeHeader;
        private System.Windows.Forms.Label LabelThemeTitle;
        private Guna.UI2.WinForms.Guna2Button BtnChooseGamePath;
        private Guna.UI2.WinForms.Guna2Button BtnChooseUserDataPath;
        private Guna.UI2.WinForms.Guna2Button BtnChooseModPath;
        private Guna.UI2.WinForms.Guna2Button BtnModLibraryPathLabel;
        private Guna.UI2.WinForms.Guna2Button BtnUserDataPathLabel;
        private Guna.UI2.WinForms.Guna2Button BtnGamePathLabel;
    }
}