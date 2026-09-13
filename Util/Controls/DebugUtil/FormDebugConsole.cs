using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util.Debug
{
    public partial class FormDebugConsole : Form
    {
        public FormDebugConsole()
        {
            InitializeComponent();
            DebugUtil.SetConsole(this);
        }

        public void SaveLogsToFile()
        {
            try
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "QuillsModManagerV2",
                    "logs"
                );
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string file = Path.Combine(
                    dir,
                    $"debugoutput {DateTime.Now:yyyy-MM-dd HH-mm-ss-fff}.md"
                );
                File.WriteAllText(file, RichTextConsoleOutput.Text);
                DebugUtil.Log(DebugLevel.INFO, DebugFilter.NONE, $"Log saved as \"{file}\"");
            }
            catch { }
        }

        internal void AppendLog(DebugLevel level, DebugFilter filter, string message)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action<DebugLevel, DebugFilter, string>(AppendLog),
                    level,
                    filter,
                    message
                );
            }
            else
            {
                RichTextConsoleOutput.Select(RichTextConsoleOutput.TextLength, 0);

                RichTextConsoleOutput.SelectionColor = Settings.Default.PlaceholderColor;
                RichTextConsoleOutput.SelectedText = $"[{DateTime.Now:HH:mm:ss}]";
                RichTextConsoleOutput.SelectionColor = Settings.Default.TextColor;
                if (level == DebugLevel.ERROR)
                {
                    RichTextConsoleOutput.SelectionFont = new Font(
                        RichTextConsoleOutput.Font,
                        FontStyle.Bold
                    );
                    RichTextConsoleOutput.SelectionColor = Color.Red;
                }
                RichTextConsoleOutput.SelectedText = " [";

                if (level == DebugLevel.ERROR)
                {
                    RichTextConsoleOutput.SelectedText = "✖]";
                }
                else if (level == DebugLevel.WARNING)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Yellow;
                    RichTextConsoleOutput.SelectedText = "⚠";
                }
                else if (level == DebugLevel.INFO)
                {
                    RichTextConsoleOutput.SelectionColor = Color.LightBlue;
                    RichTextConsoleOutput.SelectedText = "🛈";
                }

                if (level != DebugLevel.ERROR)
                {
                    RichTextConsoleOutput.SelectedText = "]";
                }

                RichTextConsoleOutput.SelectionFont = RichTextConsoleOutput.Font;
                RichTextConsoleOutput.SelectionColor = Settings.Default.TextColor;

                RichTextConsoleOutput.SelectedText = $" [";
                if (filter == DebugFilter.NONE)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Gray;
                }
                else if (filter == DebugFilter.PROCESSHOOK)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Cyan;
                }
                else if (filter == DebugFilter.DEPLOYMENT)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Lime;
                }
                else if (filter == DebugFilter.UI)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Magenta;
                }
                else if (filter == DebugFilter.SETTINGS)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Orange;
                }
                else if (filter == DebugFilter.PROFILE)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Purple;
                }
                else if (filter == DebugFilter.MODMANAGER)
                {
                    RichTextConsoleOutput.SelectionColor = Color.Green;
                }
                else if (filter == DebugFilter.DEV)
                {
                    RichTextConsoleOutput.SelectionColor = Color.LightBlue;
                }

                RichTextConsoleOutput.SelectedText = filter
                    .ToString()
                    .Substring(0, Math.Min(4, filter.ToString().Length));

                RichTextConsoleOutput.SelectionColor = Settings.Default.TextColor;
                RichTextConsoleOutput.SelectedText = $"] {message}{Environment.NewLine}";

                RichTextConsoleOutput.SelectionStart = RichTextConsoleOutput.Text.Length;
                RichTextConsoleOutput.ScrollToCaret();
            }
        }

        private void FormDebugConsole_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Enum.GetValues(typeof(DebugLevel)).Length; i++)
            {
                DebugUtil.EnableLevel((DebugLevel)i);
            }

            for (int i = 0; i < Enum.GetValues(typeof(DebugFilter)).Length; i++)
            {
                DebugUtil.EnableFilter((DebugFilter)i);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
