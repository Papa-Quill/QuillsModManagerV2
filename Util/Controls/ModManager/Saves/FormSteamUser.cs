using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util.Controls.ModManager.Saves
{
    public partial class FormSteamUser : Form
    {
        public event EventHandler SelectedSteamUserChanged;

        private List<SteamUserRow> rows = new List<SteamUserRow>();
        private Func<BindingList<ModEntry>> _getMods;
        private Panel innerPanel;

        public FormSteamUser(Func<BindingList<ModEntry>> getMods = null)
        {
            _getMods = getMods;

            InitializeComponent();

            innerPanel = new Panel
            {
                Left = 0,
                Top = 0,
                Width = PanelContent.Width,
                Height = PanelContent.Height,
                BackColor = Settings.Default.BGSecondary,
                AutoSize = false
            };

            PanelContent.Controls.Add(innerPanel);

            vScroll.Scroll += (s, e) =>
            {
                innerPanel.Top = -vScroll.Value;
                innerPanel.Invalidate();
            };

            LoadUsers();
            if (!Settings.Default.FormShadows)
                FormSteamUserEProperties.ShadowColor = Color.Black;
            FormSteamUserEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.Control | Keys.W:
                    BtnClose.PerformClick();
                    return true;

                case Keys.Control | Keys.K:
                    Settings.Default.HotKeyForm = "Default";
                    FormUtil.ShowForm<FormHotKeys>();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int delta = e.Delta > 0 ? -12 : 12;

            int newVal = Math.Max(
                vScroll.Minimum,
                Math.Min(vScroll.Maximum, vScroll.Value + delta)
            );

            vScroll.Value = newVal;
            Invalidate(false);
        }

        private void LoadUsers()
        {
            innerPanel.Controls.Clear();
            rows.Clear();

            string userDataDir = Settings.Default.UserDataPath;
            if (string.IsNullOrWhiteSpace(userDataDir))
            {
                var lbl = new Label
                {
                    Text = "Steam userdata folder not configured. Check settings.",
                    ForeColor = Settings.Default.TextColor,
                    Left = 8,
                    Top = 8,
                    AutoSize = true
                };
                innerPanel.Controls.Add(lbl);
                return;
            }

            var steamUsers = SteamUserManager.GetSteamUsers(userDataDir);
            if (steamUsers.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No Steam users with Castle Crashers found.",
                    ForeColor = Settings.Default.TextColor,
                    Left = 8,
                    Top = 8,
                    AutoSize = true
                };
                innerPanel.Controls.Add(lbl);
                return;
            }

            /*var testUsers = new List<SteamUserManager.SteamUser>(steamUsers);

 for (int i = 1; i <= 20; i++)
 {
     foreach (var user in steamUsers)
     {
         testUsers.Add(new SteamUserManager.SteamUser
         {
             PersonaName = $"{user.PersonaName} Test {i}",
             SteamID64 = $"{user.SteamID64}_{i}",
             UserDataId = $"{user.UserDataId}_{i}",
             AvatarPath = user.AvatarPath
         });
     }
 }

 steamUsers = testUsers;*/

            int y = 8;
            foreach (var user in steamUsers)
            {
                var row = new SteamUserRow
                {
                    Left = 4,
                    Top = y,
                    Width = innerPanel.Width - 8,
                    Height = 56
                };

                string currentSteamUser = Settings.Default.CurrentSteamUser;
                bool isSelected =
                    !string.IsNullOrWhiteSpace(currentSteamUser)
                    && currentSteamUser == user.UserDataId;

                row.Initialize(
                    user,
                    isSelected,
                    () =>
                    {
                        Settings.Default.CurrentSteamUser = user.UserDataId;
                        Settings.Default.Save();
                        SelectedSteamUserChanged?.Invoke(this, EventArgs.Empty);
                        BtnClose.PerformClick();
                    }
                );

                innerPanel.Controls.Add(row);
                rows.Add(row);
                y += row.Height + 6;
            }

            UpdateScroll();
        }

        private void UpdateScroll()
        {
            innerPanel.Height = Math.Max(PanelContent.Height, rows.Count * 62);

            int total = innerPanel.Height;
            int view = PanelContent.Height;

            vScroll.Minimum = 0;
            vScroll.Maximum = Math.Max(0, total - view);
            vScroll.LargeChange = view;
            vScroll.Value = Math.Max(0, Math.Min(vScroll.Value, vScroll.Maximum));

            innerPanel.Top = -vScroll.Value;

            vScroll.Enabled = total > view;
        }

        private class SteamUserRow : Panel
        {
            private Label lblName;
            private Label lblId;
            private PictureBox picAvatar;
            private Label lblCheck;

            private void SetHover(bool hovered)
            {
                this.BackColor = hovered
                    ? Settings.Default.BGTertiary
                    : Settings.Default.ButtonColor;
            }

            private void ChildMouseLeave(object sender, EventArgs e)
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                    SetHover(false);
            }

            public void Initialize(
                SteamUserManager.SteamUser user,
                bool isSelected,
                Action onSelect
            )
            {
                this.BackColor = Settings.Default.ButtonColor;

                lblCheck = new Label
                {
                    Text = isSelected ? "✔" : "",
                    ForeColor = Settings.Default.DetailActive,
                    Left = 6,
                    Top = 16,
                    Width = 20,
                    Height = 24,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                picAvatar = new PictureBox
                {
                    Left = 32,
                    Top = 8,
                    Width = 40,
                    Height = 40,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                if (
                    !string.IsNullOrWhiteSpace(user.AvatarPath)
                    && System.IO.File.Exists(user.AvatarPath)
                )
                {
                    try
                    {
                        picAvatar.Image = Image.FromFile(user.AvatarPath);
                    }
                    catch
                    {
                        picAvatar.Image = SystemIcons.Question.ToBitmap();
                    }
                }
                else
                {
                    picAvatar.Image = SystemIcons.Question.ToBitmap();
                }

                lblName = new Label
                {
                    Text = user.PersonaName ?? "Unknown",
                    Left = 80,
                    Top = 8,
                    Width = this.Width - 100,
                    Height = 24,
                    Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold),
                    ForeColor = Settings.Default.TextColor,
                    BackColor = Color.Transparent
                };

                lblId = new Label
                {
                    Text = $"ID: {user.SteamID64}",
                    Left = 80,
                    Top = 34,
                    Width = this.Width - 100,
                    Height = 20,
                    Font = new Font(this.Font.FontFamily, 8),
                    ForeColor = Settings.Default.PlaceholderColor,
                    BackColor = Color.Transparent
                };

                this.Controls.Add(lblCheck);
                this.Controls.Add(picAvatar);
                this.Controls.Add(lblName);
                this.Controls.Add(lblId);

                this.Click += (s, e) => onSelect?.Invoke();
                lblName.Click += (s, e) => onSelect?.Invoke();
                lblId.Click += (s, e) => onSelect?.Invoke();
                picAvatar.Click += (s, e) => onSelect?.Invoke();

                this.MouseEnter += (s, e) => SetHover(true);
                this.MouseLeave += (s, e) => SetHover(false);

                foreach (Control ctrl in this.Controls)
                {
                    ctrl.MouseEnter += (s, e) => SetHover(true);
                    ctrl.MouseLeave += ChildMouseLeave;
                }
            }
        }

        public void BtnClose_Click(object sender, EventArgs e)
        {
            FormUtil.Close(this);
        }
    }
}
