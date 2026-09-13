namespace QuillsModManagerV2.Util.Debug
{
    partial class FormDebugConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDebugConsole));
            this.BtnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.RichTextConsoleOutput = new System.Windows.Forms.RichTextBox();
            this.LabelTitle = new QuillsModManagerV2.Util.Controls.CClickThroughLabel();
            this.BtnMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.BorderlessDebugConsole = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.AccessibleDescription = "Close the debug console form.";
            this.BtnClose.AccessibleName = "BtnClose";
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.CustomClick = true;
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.DataBindings.Add(new System.Windows.Forms.Binding("IconColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnClose.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.BtnClose.IconColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnClose.Location = new System.Drawing.Point(468, 2);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(30, 20);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // RichTextConsoleOutput
            // 
            this.RichTextConsoleOutput.AccessibleDescription = "A rich text box for console output.";
            this.RichTextConsoleOutput.AccessibleName = "RichTextConsoleOutput";
            this.RichTextConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextConsoleOutput.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGSecondary;
            this.RichTextConsoleOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RichTextConsoleOutput.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGSecondary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RichTextConsoleOutput.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.RichTextConsoleOutput.ForeColor = System.Drawing.Color.White;
            this.RichTextConsoleOutput.Location = new System.Drawing.Point(12, 26);
            this.RichTextConsoleOutput.MaxLength = 65534;
            this.RichTextConsoleOutput.MinimumSize = new System.Drawing.Size(285, 190);
            this.RichTextConsoleOutput.Name = "RichTextConsoleOutput";
            this.RichTextConsoleOutput.ReadOnly = true;
            this.RichTextConsoleOutput.Size = new System.Drawing.Size(476, 261);
            this.RichTextConsoleOutput.TabIndex = 0;
            this.RichTextConsoleOutput.Text = "";
            // 
            // LabelTitle
            // 
            this.LabelTitle.AccessibleDescription = "Debug console title label.";
            this.LabelTitle.AccessibleName = "LabelTitle";
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.LabelTitle.Location = new System.Drawing.Point(9, 0);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(126, 21);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "Debug Console";
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.AccessibleDescription = "Minimize the debug console form.";
            this.BtnMinimize.AccessibleName = "BtnMinimize";
            this.BtnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.BtnMinimize.DataBindings.Add(new System.Windows.Forms.Binding("FillColor", global::QuillsModManagerV2.Properties.Settings.Default, "DetailColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnMinimize.DataBindings.Add(new System.Windows.Forms.Binding("IconColor", global::QuillsModManagerV2.Properties.Settings.Default, "TextColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BtnMinimize.FillColor = global::QuillsModManagerV2.Properties.Settings.Default.DetailColor;
            this.BtnMinimize.IconColor = global::QuillsModManagerV2.Properties.Settings.Default.TextColor;
            this.BtnMinimize.Location = new System.Drawing.Point(436, 2);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(30, 20);
            this.BtnMinimize.TabIndex = 0;
            // 
            // BorderlessDebugConsole
            // 
            this.BorderlessDebugConsole.ContainerControl = this;
            this.BorderlessDebugConsole.DockForm = false;
            this.BorderlessDebugConsole.DockIndicatorTransparencyValue = 0.6D;
            this.BorderlessDebugConsole.TransparentWhileDrag = true;
            // 
            // FormDebugConsole
            // 
            this.AccessibleDescription = "Console output for debug.";
            this.AccessibleName = "FormDebugConsole";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::QuillsModManagerV2.Properties.Settings.Default.BGPrimary;
            this.ClientSize = new System.Drawing.Size(500, 299);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.RichTextConsoleOutput);
            this.Controls.Add(this.BtnMinimize);
            this.Controls.Add(this.BtnClose);
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::QuillsModManagerV2.Properties.Settings.Default, "BGPrimary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "FormDebugConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormDebugConsole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ControlBox BtnClose;
        private System.Windows.Forms.RichTextBox RichTextConsoleOutput;
        private QuillsModManagerV2.Util.Controls.CClickThroughLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox BtnMinimize;
        private Guna.UI2.WinForms.Guna2BorderlessForm BorderlessDebugConsole;
    }
}