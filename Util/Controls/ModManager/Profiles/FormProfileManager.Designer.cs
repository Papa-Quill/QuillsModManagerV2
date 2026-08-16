namespace QuillsModManagerV2.Util.Controls.ModManager
{
    partial class FormProfileManager
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
                _animation?.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProfileManager));
            this.BtnCreateProfile = new Guna.UI2.WinForms.Guna2Button();
            this.BtnDeleteProfile = new Guna.UI2.WinForms.Guna2Button();
            this.BtnFinishSelection = new Guna.UI2.WinForms.Guna2Button();
            this.BtnRenameProfile = new Guna.UI2.WinForms.Guna2Button();
            this.BtnDuplicateProfile = new Guna.UI2.WinForms.Guna2Button();
            this.vScroll = new QuillsModManagerV2.Util.Controls.CustomVScrollBar();
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.TextBoxUserInput = new Guna.UI2.WinForms.Guna2TextBox();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelContent = new System.Windows.Forms.Panel();
            this.ProfileManagerEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.BtnSteamProfile = new Guna.UI2.WinForms.Guna2Button();
            this.BtnOpenTutorial = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.PanelBody.SuspendLayout();
            this.PanelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCreateProfile
            // 
            this.BtnCreateProfile.AccessibleDescription = "Create a profile with the name written in the user input.";
            this.BtnCreateProfile.AccessibleName = "BtnCreateProfile";
            this.BtnCreateProfile.Animated = true;
            this.BtnCreateProfile.BackColor = System.Drawing.Color.Transparent;
            this.BtnCreateProfile.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnCreateProfile.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnCreateProfile.BorderThickness = 1;
            this.BtnCreateProfile.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnCreateProfile.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCreateProfile.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnCreateProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCreateProfile.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnCreateProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnCreateProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnCreateProfile.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnCreateProfile.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnCreateProfile.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnCreateProfile.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnCreateProfile.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnCreateProfile.Location = new System.Drawing.Point(10, 275);
            this.BtnCreateProfile.Name = "BtnCreateProfile";
            this.BtnCreateProfile.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnCreateProfile.Size = new System.Drawing.Size(120, 30);
            this.BtnCreateProfile.TabIndex = 0;
            this.BtnCreateProfile.Text = "Create Profile";
            this.BtnCreateProfile.UseTransparentBackground = true;
            this.BtnCreateProfile.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // BtnDeleteProfile
            // 
            this.BtnDeleteProfile.AccessibleDescription = "Delete the selected profile.";
            this.BtnDeleteProfile.AccessibleName = "BtnDeleteProfile";
            this.BtnDeleteProfile.Animated = true;
            this.BtnDeleteProfile.BackColor = System.Drawing.Color.Transparent;
            this.BtnDeleteProfile.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnDeleteProfile.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnDeleteProfile.BorderThickness = 1;
            this.BtnDeleteProfile.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnDeleteProfile.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteProfile.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDeleteProfile.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDeleteProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDeleteProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDeleteProfile.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDeleteProfile.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnDeleteProfile.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnDeleteProfile.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnDeleteProfile.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnDeleteProfile.Location = new System.Drawing.Point(10, 315);
            this.BtnDeleteProfile.Name = "BtnDeleteProfile";
            this.BtnDeleteProfile.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnDeleteProfile.Size = new System.Drawing.Size(120, 30);
            this.BtnDeleteProfile.TabIndex = 0;
            this.BtnDeleteProfile.Text = "Delete Profile";
            this.BtnDeleteProfile.UseTransparentBackground = true;
            this.BtnDeleteProfile.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnFinishSelection
            // 
            this.BtnFinishSelection.AccessibleDescription = "Load the selected profile.";
            this.BtnFinishSelection.AccessibleName = "BtnFinishSelection";
            this.BtnFinishSelection.Animated = true;
            this.BtnFinishSelection.BackColor = System.Drawing.Color.Transparent;
            this.BtnFinishSelection.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnFinishSelection.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnFinishSelection.BorderThickness = 1;
            this.BtnFinishSelection.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnFinishSelection.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFinishSelection.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnFinishSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnFinishSelection.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnFinishSelection.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnFinishSelection.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnFinishSelection.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnFinishSelection.Location = new System.Drawing.Point(150, 275);
            this.BtnFinishSelection.Name = "BtnFinishSelection";
            this.BtnFinishSelection.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnFinishSelection.Size = new System.Drawing.Size(120, 30);
            this.BtnFinishSelection.TabIndex = 0;
            this.BtnFinishSelection.Text = "Load Profile";
            this.BtnFinishSelection.UseTransparentBackground = true;
            this.BtnFinishSelection.Click += new System.EventHandler(this.BtnFinishSelection_Click);
            // 
            // BtnRenameProfile
            // 
            this.BtnRenameProfile.AccessibleDescription = "Rename the selected profile with the user input.";
            this.BtnRenameProfile.AccessibleName = "BtnRenameProfile";
            this.BtnRenameProfile.Animated = true;
            this.BtnRenameProfile.BackColor = System.Drawing.Color.Transparent;
            this.BtnRenameProfile.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnRenameProfile.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnRenameProfile.BorderThickness = 1;
            this.BtnRenameProfile.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnRenameProfile.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRenameProfile.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnRenameProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRenameProfile.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnRenameProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnRenameProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnRenameProfile.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnRenameProfile.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnRenameProfile.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnRenameProfile.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnRenameProfile.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnRenameProfile.Location = new System.Drawing.Point(150, 315);
            this.BtnRenameProfile.Name = "BtnRenameProfile";
            this.BtnRenameProfile.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnRenameProfile.Size = new System.Drawing.Size(120, 30);
            this.BtnRenameProfile.TabIndex = 0;
            this.BtnRenameProfile.Text = "Rename Profile";
            this.BtnRenameProfile.UseTransparentBackground = true;
            this.BtnRenameProfile.Click += new System.EventHandler(this.BtnRename_Click);
            // 
            // BtnDuplicateProfile
            // 
            this.BtnDuplicateProfile.AccessibleDescription = "Duplicate the selected profile.";
            this.BtnDuplicateProfile.AccessibleName = "BtnDuplicateProfile";
            this.BtnDuplicateProfile.Animated = true;
            this.BtnDuplicateProfile.BackColor = System.Drawing.Color.Transparent;
            this.BtnDuplicateProfile.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnDuplicateProfile.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.BtnDuplicateProfile.BorderThickness = 1;
            this.BtnDuplicateProfile.CheckedState.CustomBorderColor = System.Drawing.Color.White;
            this.BtnDuplicateProfile.CheckedState.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDuplicateProfile.CheckedState.ForeColor = System.Drawing.Color.White;
            this.BtnDuplicateProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDuplicateProfile.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDuplicateProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDuplicateProfile.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDuplicateProfile.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnDuplicateProfile.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnDuplicateProfile.Font = new System.Drawing.Font("Gadugi", 9F);
            this.BtnDuplicateProfile.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnDuplicateProfile.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.BtnDuplicateProfile.Location = new System.Drawing.Point(290, 315);
            this.BtnDuplicateProfile.Name = "BtnDuplicateProfile";
            this.BtnDuplicateProfile.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnDuplicateProfile.Size = new System.Drawing.Size(120, 30);
            this.BtnDuplicateProfile.TabIndex = 0;
            this.BtnDuplicateProfile.Text = "Duplicate Profile";
            this.BtnDuplicateProfile.UseTransparentBackground = true;
            this.BtnDuplicateProfile.Click += new System.EventHandler(this.BtnDuplicate_Click);
            // 
            // vScroll
            // 
            this.vScroll.AccessibleName = "vScroll";
            this.vScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.vScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScroll.ForeColor = System.Drawing.Color.White;
            this.vScroll.LargeChange = 10;
            this.vScroll.Location = new System.Drawing.Point(374, 0);
            this.vScroll.Maximum = 100;
            this.vScroll.Minimum = 0;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(10, 195);
            this.vScroll.SmallChange = 1;
            this.vScroll.TabIndex = 0;
            this.vScroll.Value = 0;
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
            this.PanelHeader.Size = new System.Drawing.Size(400, 34);
            this.PanelHeader.TabIndex = 0;
            // 
            // ImgIcon
            // 
            this.ImgIcon.AccessibleName = "ImgIcon";
            this.ImgIcon.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGTertiary;
            this.ImgIcon.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGTertiary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ImgIcon.Image = global::QuillsModManagerV2.Properties.Resources.AnimatedQuillDiamond;
            this.ImgIcon.Location = new System.Drawing.Point(16, 12);
            this.ImgIcon.Name = "ImgIcon";
            this.ImgIcon.Size = new System.Drawing.Size(30, 30);
            this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgIcon.TabIndex = 18;
            this.ImgIcon.TabStop = false;
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
            this.LabelTitle.Location = new System.Drawing.Point(48, 16);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(136, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "QMM / PROFILES";
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
            this.BtnClose.Location = new System.Drawing.Point(372, 11);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.UseTransparentBackground = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // TextBoxUserInput
            // 
            this.TextBoxUserInput.AccessibleDescription = "The user input textbox for naming profiles.";
            this.TextBoxUserInput.AccessibleName = "TextBoxUserInput";
            this.TextBoxUserInput.Animated = true;
            this.TextBoxUserInput.BackColor = System.Drawing.Color.Transparent;
            this.TextBoxUserInput.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.TextBoxUserInput.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.TextBoxUserInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxUserInput.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxUserInput.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxUserInput.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxUserInput.DataBindings.Add(new System.Windows.Forms.Binding("PlaceholderForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxUserInput.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxUserInput.DefaultText = "";
            this.TextBoxUserInput.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TextBoxUserInput.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TextBoxUserInput.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxUserInput.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxUserInput.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.TextBoxUserInput.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(167)))), ((int)(((byte)(231)))));
            this.TextBoxUserInput.Font = new System.Drawing.Font("Gadugi", 9F);
            this.TextBoxUserInput.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TextBoxUserInput.Location = new System.Drawing.Point(290, 275);
            this.TextBoxUserInput.MaxLength = 255;
            this.TextBoxUserInput.Name = "TextBoxUserInput";
            this.TextBoxUserInput.PasswordChar = '\0';
            this.TextBoxUserInput.PlaceholderForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.TextBoxUserInput.PlaceholderText = "User Input";
            this.TextBoxUserInput.SelectedText = "";
            this.TextBoxUserInput.Size = new System.Drawing.Size(120, 30);
            this.TextBoxUserInput.TabIndex = 0;
            this.TextBoxUserInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PanelBody
            // 
            this.PanelBody.AccessibleName = "PanelBody";
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
            this.PanelBody.Size = new System.Drawing.Size(400, 211);
            this.PanelBody.TabIndex = 0;
            // 
            // PanelContent
            // 
            this.PanelContent.AccessibleName = "PanelContent";
            this.PanelContent.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelContent.Controls.Add(this.vScroll);
            this.PanelContent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelContent.Location = new System.Drawing.Point(8, 8);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(384, 195);
            this.PanelContent.TabIndex = 0;
            // 
            // ProfileManagerEProperties
            // 
            this.ProfileManagerEProperties.ContainerControl = this;
            this.ProfileManagerEProperties.DockForm = false;
            this.ProfileManagerEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.ProfileManagerEProperties.DragForm = false;
            this.ProfileManagerEProperties.ResizeForm = false;
            this.ProfileManagerEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.ProfileManagerEProperties.TransparentWhileDrag = true;
            // 
            // BtnSteamProfile
            // 
            this.BtnSteamProfile.AccessibleDescription = "Select your Steam profile.";
            this.BtnSteamProfile.AccessibleName = "BtnSteamProfile";
            this.BtnSteamProfile.Animated = true;
            this.BtnSteamProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(31)))));
            this.BtnSteamProfile.CustomizableEdges.BottomLeft = false;
            this.BtnSteamProfile.CustomizableEdges.BottomRight = false;
            this.BtnSteamProfile.CustomizableEdges.TopLeft = false;
            this.BtnSteamProfile.CustomizableEdges.TopRight = false;
            this.BtnSteamProfile.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnSteamProfile.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnSteamProfile.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this.BtnSteamProfile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnSteamProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this.BtnSteamProfile.HoverState.FillColor = System.Drawing.Color.Gray;
            this.BtnSteamProfile.Image = global::QuillsModManagerV2.Properties.Resources.Profile;
            this.BtnSteamProfile.ImageSize = new System.Drawing.Size(16, 16);
            this.BtnSteamProfile.Location = new System.Drawing.Point(340, 11);
            this.BtnSteamProfile.Name = "BtnSteamProfile";
            this.BtnSteamProfile.PressedColor = System.Drawing.Color.Transparent;
            this.BtnSteamProfile.Size = new System.Drawing.Size(32, 32);
            this.BtnSteamProfile.TabIndex = 0;
            this.BtnSteamProfile.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // BtnOpenTutorial
            // 
            this.BtnOpenTutorial.AccessibleDescription = "Open tutorial video in browser.";
            this.BtnOpenTutorial.AccessibleName = "BtnOpenTutorial";
            this.BtnOpenTutorial.Animated = true;
            this.BtnOpenTutorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(31)))));
            this.BtnOpenTutorial.CustomizableEdges.BottomLeft = false;
            this.BtnOpenTutorial.CustomizableEdges.BottomRight = false;
            this.BtnOpenTutorial.CustomizableEdges.TopLeft = false;
            this.BtnOpenTutorial.CustomizableEdges.TopRight = false;
            this.BtnOpenTutorial.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnOpenTutorial.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.BtnOpenTutorial.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this.BtnOpenTutorial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnOpenTutorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this.BtnOpenTutorial.HoverState.FillColor = System.Drawing.Color.Gray;
            this.BtnOpenTutorial.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpenTutorial.Image")));
            this.BtnOpenTutorial.Location = new System.Drawing.Point(307, 11);
            this.BtnOpenTutorial.Name = "BtnOpenTutorial";
            this.BtnOpenTutorial.PressedColor = System.Drawing.Color.Transparent;
            this.BtnOpenTutorial.Size = new System.Drawing.Size(32, 32);
            this.BtnOpenTutorial.TabIndex = 0;
            this.BtnOpenTutorial.MouseHover += new System.EventHandler(this.BtnToolTip_MouseEnter);
            // 
            // FormProfileManager
            // 
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(420, 355);
            this.Controls.Add(this.BtnOpenTutorial);
            this.Controls.Add(this.BtnSteamProfile);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.TextBoxUserInput);
            this.Controls.Add(this.ImgIcon);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.BtnDuplicateProfile);
            this.Controls.Add(this.BtnRenameProfile);
            this.Controls.Add(this.BtnFinishSelection);
            this.Controls.Add(this.BtnDeleteProfile);
            this.Controls.Add(this.BtnCreateProfile);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormProfileManager";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.PanelBody.ResumeLayout(false);
            this.PanelContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna.UI2.WinForms.Guna2Button BtnCreateProfile;
        private Guna.UI2.WinForms.Guna2Button BtnDeleteProfile;
        private Guna.UI2.WinForms.Guna2Button BtnFinishSelection;
        private Guna.UI2.WinForms.Guna2Button BtnRenameProfile;
        private Guna.UI2.WinForms.Guna2Button BtnDuplicateProfile;
        #endregion

        private CustomVScrollBar vScroll;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private System.Windows.Forms.PictureBox ImgIcon;
        public CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2TextBox TextBoxUserInput;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        private Guna.UI2.WinForms.Guna2BorderlessForm ProfileManagerEProperties;
        private System.Windows.Forms.Panel PanelContent;
        private Guna.UI2.WinForms.Guna2Button BtnSteamProfile;
        private Guna.UI2.WinForms.Guna2Button BtnOpenTutorial;
    }
}