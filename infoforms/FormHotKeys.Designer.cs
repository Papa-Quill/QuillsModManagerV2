namespace QuillsModManagerV2.InfoForms
{
    partial class FormHotKeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHotKeys));
            this.PanelNotifColor = new Guna.UI2.WinForms.Guna2Panel();
            this.HotkeyBox = new Guna.UI2.WinForms.Guna2Panel();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.TxtNotif = new System.Windows.Forms.Label();
            this.HotkeyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelNotifColor
            // 
            this.PanelNotifColor.AccessibleName = "PanelNotifColor";
            this.PanelNotifColor.BackColor = System.Drawing.Color.White;
            this.PanelNotifColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelNotifColor.FillColor = System.Drawing.Color.White;
            this.PanelNotifColor.Location = new System.Drawing.Point(0, 0);
            this.PanelNotifColor.Name = "PanelNotifColor";
            this.PanelNotifColor.Size = new System.Drawing.Size(5, 313);
            this.PanelNotifColor.TabIndex = 0;
            // 
            // HotkeyBox
            // 
            this.HotkeyBox.AccessibleName = "HotkeyBox";
            this.HotkeyBox.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.HotkeyBox.BorderColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.HotkeyBox.BorderThickness = 1;
            this.HotkeyBox.Controls.Add(this.LabelTitle);
            this.HotkeyBox.Controls.Add(this.TxtNotif);
            this.HotkeyBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HotkeyBox.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.HotkeyBox.DataBindings.Add(new System.Windows.Forms.Binding("BorderColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.HotkeyBox.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.HotkeyBox.Location = new System.Drawing.Point(15, 10);
            this.HotkeyBox.Name = "HotkeyBox";
            this.HotkeyBox.Size = new System.Drawing.Size(220, 167);
            this.HotkeyBox.TabIndex = 0;
            // 
            // LabelTitle
            // 
            this.LabelTitle.AccessibleName = "LabelTitle";
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.BackColor = System.Drawing.Color.Transparent;
            this.LabelTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTitle.Location = new System.Drawing.Point(10, 10);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(132, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "Placeholder Title";
            this.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtNotif
            // 
            this.TxtNotif.AccessibleName = "TxtNotif";
            this.TxtNotif.AutoSize = true;
            this.TxtNotif.BackColor = System.Drawing.Color.Transparent;
            this.TxtNotif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TxtNotif.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TxtNotif.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TxtNotif.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.TxtNotif.Location = new System.Drawing.Point(10, 30);
            this.TxtNotif.Name = "TxtNotif";
            this.TxtNotif.Size = new System.Drawing.Size(200, 126);
            this.TxtNotif.TabIndex = 0;
            this.TxtNotif.Text = "Escape = Close Window\r\nCtrl + W = Close Window\r\nCtrl + S = Save Settings\r\nEnter =" +
    " Screenshot\r\nCtrl + C = Copy Screenshot\r\nCtrl + S = Save Screenshot";
            this.TxtNotif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormHotKeys
            // 
            this.AccessibleName = "FormHotKeys";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(245, 187);
            this.Controls.Add(this.PanelNotifColor);
            this.Controls.Add(this.HotkeyBox);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHotKeys";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hot Keys";
            this.TopMost = true;
            this.HotkeyBox.ResumeLayout(false);
            this.HotkeyBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel PanelNotifColor;
        private Guna.UI2.WinForms.Guna2Panel HotkeyBox;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Label TxtNotif;
    }
}