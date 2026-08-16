namespace QuillsModManagerV2.InfoForms
{
    partial class FormToast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToast));
            this.ToastBox = new Guna.UI2.WinForms.Guna2Panel();
            this.ImgIcon = new System.Windows.Forms.PictureBox();
            this.TxtToast = new System.Windows.Forms.Label();
            this.ToastProgressbar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.ToastBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // ToastBox
            // 
            this.ToastBox.AccessibleName = "ToastBox";
            this.ToastBox.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.ToastBox.BorderThickness = 1;
            this.ToastBox.Controls.Add(this.ImgIcon);
            this.ToastBox.Controls.Add(this.TxtToast);
            this.ToastBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToastBox.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToastBox.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToastBox.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToastBox.Location = new System.Drawing.Point(0, 0);
            this.ToastBox.Name = "ToastBox";
            this.ToastBox.Size = new System.Drawing.Size(388, 41);
            this.ToastBox.TabIndex = 0;
            this.ToastBox.Click += new System.EventHandler(this.ToastBox_Click);
            this.ToastBox.MouseEnter += new System.EventHandler(this.ToastBox_MouseHover);
            this.ToastBox.MouseLeave += new System.EventHandler(this.ToastBox_MouseLeave);
            // 
            // ImgIcon
            // 
            this.ImgIcon.AccessibleName = "ImgIcon";
            this.ImgIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImgIcon.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ImgIcon.Image = global::QuillsModManagerV2.Properties.Resources.AnimatedQuillDiamond;
            this.ImgIcon.Location = new System.Drawing.Point(7, 5);
            this.ImgIcon.Name = "ImgIcon";
            this.ImgIcon.Size = new System.Drawing.Size(32, 32);
            this.ImgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgIcon.TabIndex = 2;
            this.ImgIcon.TabStop = false;
            this.ImgIcon.Click += new System.EventHandler(this.ToastBox_Click);
            this.ImgIcon.MouseEnter += new System.EventHandler(this.ToastBox_MouseHover);
            this.ImgIcon.MouseLeave += new System.EventHandler(this.ToastBox_MouseLeave);
            // 
            // TxtToast
            // 
            this.TxtToast.AccessibleName = "TxtToast";
            this.TxtToast.AutoSize = true;
            this.TxtToast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TxtToast.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtToast.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtToast.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtToast.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TxtToast.Location = new System.Drawing.Point(42, 10);
            this.TxtToast.Name = "TxtToast";
            this.TxtToast.Size = new System.Drawing.Size(75, 21);
            this.TxtToast.TabIndex = 0;
            this.TxtToast.Text = "Toast Text";
            this.TxtToast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TxtToast.Click += new System.EventHandler(this.ToastBox_Click);
            this.TxtToast.MouseEnter += new System.EventHandler(this.ToastBox_MouseHover);
            this.TxtToast.MouseLeave += new System.EventHandler(this.ToastBox_MouseLeave);
            // 
            // ToastProgressbar
            // 
            this.ToastProgressbar.AccessibleName = "ToastProgressbar";
            this.ToastProgressbar.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.ToastProgressbar.BorderThickness = 1;
            this.ToastProgressbar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToastProgressbar.CustomizableEdges.TopLeft = false;
            this.ToastProgressbar.CustomizableEdges.TopRight = false;
            this.ToastProgressbar.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToastProgressbar.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToastProgressbar.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ToastProgressbar.Location = new System.Drawing.Point(0, 40);
            this.ToastProgressbar.Maximum = 3000;
            this.ToastProgressbar.Name = "ToastProgressbar";
            this.ToastProgressbar.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.Solid;
            this.ToastProgressbar.ProgressColor = System.Drawing.Color.Red;
            this.ToastProgressbar.ProgressColor2 = System.Drawing.Color.Black;
            this.ToastProgressbar.Size = new System.Drawing.Size(388, 7);
            this.ToastProgressbar.TabIndex = 0;
            this.ToastProgressbar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.ToastProgressbar.Value = 3000;
            this.ToastProgressbar.Click += new System.EventHandler(this.ToastBox_Click);
            this.ToastProgressbar.MouseEnter += new System.EventHandler(this.ToastBox_MouseHover);
            this.ToastProgressbar.MouseLeave += new System.EventHandler(this.ToastBox_MouseLeave);
            // 
            // FormToast
            // 
            this.AccessibleName = "FormToast";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(388, 47);
            this.Controls.Add(this.ToastBox);
            this.Controls.Add(this.ToastProgressbar);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "FormToast";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Toast";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.ToastBox_Click);
            this.MouseEnter += new System.EventHandler(this.ToastBox_MouseHover);
            this.MouseLeave += new System.EventHandler(this.ToastBox_MouseLeave);
            this.ToastBox.ResumeLayout(false);
            this.ToastBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel ToastBox;
        private System.Windows.Forms.PictureBox ImgIcon;
        private System.Windows.Forms.Label TxtToast;
        private Guna.UI2.WinForms.Guna2ProgressBar ToastProgressbar;
    }
}