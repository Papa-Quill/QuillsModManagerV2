namespace QuillsModManagerV2.infoforms.UserInput
{
    partial class FormColorSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColorSelector));
            this.FormColorSelectorEProperties = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.PanelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.PanelBody = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelRGBUnderline = new System.Windows.Forms.Panel();
            this.TextBoxRGBColor = new Guna.UI2.WinForms.Guna2TextBox();
            this.PanelHexUnderline = new System.Windows.Forms.Panel();
            this.TextBoxHexColor = new Guna.UI2.WinForms.Guna2TextBox();
            this.BtnColorPreview = new Guna.UI2.WinForms.Guna2Button();
            this.BtnColorPicker = new Guna.UI2.WinForms.Guna2Button();
            this.ScrollBarBlue = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.ScrollBarGreen = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.ScrollBarRed = new Guna.UI2.WinForms.Guna2HScrollBar();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.PanelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.SuspendLayout();
                                             this.FormColorSelectorEProperties.ContainerControl = this;
            this.FormColorSelectorEProperties.DockForm = false;
            this.FormColorSelectorEProperties.DockIndicatorTransparencyValue = 0.6D;
            this.FormColorSelectorEProperties.DragForm = false;
            this.FormColorSelectorEProperties.ResizeForm = false;
            this.FormColorSelectorEProperties.ShadowColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailActive;
            this.FormColorSelectorEProperties.TransparentWhileDrag = true;
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
            this.PanelHeader.Size = new System.Drawing.Size(235, 34);
            this.PanelHeader.TabIndex = 20;
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
            this.BtnClose.Location = new System.Drawing.Point(207, 11);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.MaximumSize = new System.Drawing.Size(37, 32);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(37, 32);
            this.BtnClose.TabIndex = 21;
            this.BtnClose.UseTransparentBackground = true;
                                             this.PanelBody.AccessibleName = "PanelBody";
            this.PanelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBody.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.PanelBody.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelBody.BorderRadius = global::QuillsModManagerV2.Properties.Settings.Default.BorderRadius;
            this.PanelBody.BorderThickness = 1;
            this.PanelBody.Controls.Add(this.PanelRGBUnderline);
            this.PanelBody.Controls.Add(this.TextBoxRGBColor);
            this.PanelBody.Controls.Add(this.PanelHexUnderline);
            this.PanelBody.Controls.Add(this.TextBoxHexColor);
            this.PanelBody.Controls.Add(this.BtnColorPreview);
            this.PanelBody.Controls.Add(this.BtnColorPicker);
            this.PanelBody.Controls.Add(this.ScrollBarBlue);
            this.PanelBody.Controls.Add(this.ScrollBarGreen);
            this.PanelBody.Controls.Add(this.ScrollBarRed);
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("BorderRadius", global::QuillsModManagerV2.Properties.Settings.Default, "BorderRadius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelBody.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.PanelBody.Location = new System.Drawing.Point(10, 54);
            this.PanelBody.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelBody.Name = "PanelBody";
            this.PanelBody.Padding = new System.Windows.Forms.Padding(9);
            this.PanelBody.Size = new System.Drawing.Size(235, 196);
            this.PanelBody.TabIndex = 25;
                                             this.PanelRGBUnderline.AccessibleName = "PanelHexUnderline";
            this.PanelRGBUnderline.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelRGBUnderline.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelRGBUnderline.Location = new System.Drawing.Point(122, 132);
            this.PanelRGBUnderline.Name = "PanelRGBUnderline";
            this.PanelRGBUnderline.Size = new System.Drawing.Size(101, 2);
            this.PanelRGBUnderline.TabIndex = 11;
                                             this.TextBoxRGBColor.AccessibleName = "TextBoxRGBColor";
            this.TextBoxRGBColor.Animated = true;
            this.TextBoxRGBColor.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.TextBoxRGBColor.BorderThickness = 0;
            this.TextBoxRGBColor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxRGBColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxRGBColor.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxRGBColor.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxRGBColor.DataBindings.Add(new System.Windows.Forms.Binding("PlaceholderForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxRGBColor.DefaultText = "";
            this.TextBoxRGBColor.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TextBoxRGBColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TextBoxRGBColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxRGBColor.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxRGBColor.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.TextBoxRGBColor.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxRGBColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TextBoxRGBColor.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TextBoxRGBColor.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxRGBColor.Location = new System.Drawing.Point(122, 100);
            this.TextBoxRGBColor.MaxLength = 9;
            this.TextBoxRGBColor.Name = "TextBoxRGBColor";
            this.TextBoxRGBColor.PasswordChar = '\0';
            this.TextBoxRGBColor.PlaceholderForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.TextBoxRGBColor.PlaceholderText = "RGB";
            this.TextBoxRGBColor.SelectedText = "";
            this.TextBoxRGBColor.Size = new System.Drawing.Size(101, 32);
            this.TextBoxRGBColor.TabIndex = 10;
            this.TextBoxRGBColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                                             this.PanelHexUnderline.AccessibleName = "PanelHexUnderline";
            this.PanelHexUnderline.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.PanelHexUnderline.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.PanelHexUnderline.Location = new System.Drawing.Point(52, 132);
            this.PanelHexUnderline.Name = "PanelHexUnderline";
            this.PanelHexUnderline.Size = new System.Drawing.Size(64, 2);
            this.PanelHexUnderline.TabIndex = 11;
                                             this.TextBoxHexColor.AccessibleName = "TextBoxHexColor";
            this.TextBoxHexColor.Animated = true;
            this.TextBoxHexColor.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.TextBoxHexColor.BorderThickness = 0;
            this.TextBoxHexColor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxHexColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxHexColor.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "ButtonColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxHexColor.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxHexColor.DataBindings.Add(new System.Windows.Forms.Binding("PlaceholderForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "PlaceholderColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TextBoxHexColor.DefaultText = "";
            this.TextBoxHexColor.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TextBoxHexColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TextBoxHexColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxHexColor.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TextBoxHexColor.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.ButtonColor;
            this.TextBoxHexColor.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxHexColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TextBoxHexColor.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TextBoxHexColor.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TextBoxHexColor.Location = new System.Drawing.Point(52, 100);
            this.TextBoxHexColor.MaxLength = 9;
            this.TextBoxHexColor.Name = "TextBoxHexColor";
            this.TextBoxHexColor.PasswordChar = '\0';
            this.TextBoxHexColor.PlaceholderForeColor = global::QuillsModManagerV2.Properties.Settings.Default.PlaceholderColor;
            this.TextBoxHexColor.PlaceholderText = "Hex";
            this.TextBoxHexColor.SelectedText = "";
            this.TextBoxHexColor.Size = new System.Drawing.Size(64, 32);
            this.TextBoxHexColor.TabIndex = 10;
            this.TextBoxHexColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.BtnColorPreview.Location = new System.Drawing.Point(11, 144);
            this.BtnColorPreview.Name = "BtnColorPreview";
            this.BtnColorPreview.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnColorPreview.Size = new System.Drawing.Size(212, 45);
            this.BtnColorPreview.TabIndex = 9;
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
            this.BtnColorPicker.Location = new System.Drawing.Point(11, 100);
            this.BtnColorPicker.Name = "BtnColorPicker";
            this.BtnColorPicker.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.BtnColorPicker.Size = new System.Drawing.Size(34, 34);
            this.BtnColorPicker.TabIndex = 8;
            this.BtnColorPicker.UseTransparentBackground = true;
            this.BtnColorPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnColorPicker_MouseDown);
            this.BtnColorPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnColorPicker_MouseUp);
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
            this.ScrollBarBlue.Location = new System.Drawing.Point(11, 72);
            this.ScrollBarBlue.Maximum = 255;
            this.ScrollBarBlue.Name = "ScrollBarBlue";
            this.ScrollBarBlue.ScrollbarSize = 18;
            this.ScrollBarBlue.Size = new System.Drawing.Size(212, 18);
            this.ScrollBarBlue.TabIndex = 5;
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
            this.ScrollBarGreen.Location = new System.Drawing.Point(11, 44);
            this.ScrollBarGreen.Maximum = 255;
            this.ScrollBarGreen.Name = "ScrollBarGreen";
            this.ScrollBarGreen.ScrollbarSize = 18;
            this.ScrollBarGreen.Size = new System.Drawing.Size(212, 18);
            this.ScrollBarGreen.TabIndex = 6;
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
            this.ScrollBarRed.Location = new System.Drawing.Point(11, 16);
            this.ScrollBarRed.Maximum = 255;
            this.ScrollBarRed.Name = "ScrollBarRed";
            this.ScrollBarRed.ScrollbarSize = 18;
            this.ScrollBarRed.Size = new System.Drawing.Size(212, 18);
            this.ScrollBarRed.TabIndex = 7;
            this.ScrollBarRed.ThumbColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.ScrollBarRed.ThumbSize = 15F;
            this.ScrollBarRed.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            this.ScrollBarRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
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
            this.LabelTitle.TabIndex = 26;
            this.LabelTitle.Text = "QMM / COLOR";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ImgIcon.TabIndex = 27;
            this.ImgIcon.TabStop = false;
                                             this.AccessibleName = "FormColorSelector";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(255, 260);
            this.Controls.Add(this.ImgIcon);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.PanelBody);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.PanelHeader);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormColorSelector";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Color Selector";
            this.PanelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm FormColorSelectorEProperties;
        private Guna.UI2.WinForms.Guna2Panel PanelHeader;
        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private Guna.UI2.WinForms.Guna2Panel PanelBody;
        public Util.Controls.CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarBlue;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarGreen;
        private Guna.UI2.WinForms.Guna2HScrollBar ScrollBarRed;
        private Guna.UI2.WinForms.Guna2Button BtnColorPicker;
        private Guna.UI2.WinForms.Guna2Button BtnColorPreview;
        private Guna.UI2.WinForms.Guna2TextBox TextBoxHexColor;
        private System.Windows.Forms.Panel PanelHexUnderline;
        private System.Windows.Forms.Panel PanelRGBUnderline;
        private Guna.UI2.WinForms.Guna2TextBox TextBoxRGBColor;
        private System.Windows.Forms.PictureBox ImgIcon;
    }
}