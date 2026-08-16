using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.InfoForms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util.Controls.AnimatedList;
using QuillsModManagerV2.Util.Controls.ModManager.Saves;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls.ModManager
{
    public partial class FormProfileManager : Form
    {
        private Action<string> _onLoad;
        private Func<BindingList<ModEntry>> _getMods;
        private List<string> _profiles = new List<string>();
        private Panel innerPanel;
        private AnimatedListController _animation;
        private string _lastAppliedProfile = null;
        private int _selectedIndex = -1;

        public FormProfileManager(Func<BindingList<ModEntry>> getMods, Action<string> onLoad)
        {
            _getMods = getMods;
            _onLoad = onLoad;
            InitializeComponent();
            if (!Settings.Default.FormShadows)
                ProfileManagerEProperties.ShadowColor = Color.Black;
            ProfileManagerEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
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
            _animation = new AnimatedListController();
            _animation.AnimationUpdated += Animation_AnimationUpdated;
            vScroll.Scroll += (s, e) =>
            {
                innerPanel.Top = -vScroll.Value;
                innerPanel.Invalidate();
            };
            RefreshSettingsTheme();
            try
            {
                BtnSteamProfile.Click += (s, e) =>
                {
                    string defaultCurrentSteamUser = Settings.Default.CurrentSteamUser;

                    var chooser = new FormSteamUser(
                        () => _getMods != null ? _getMods() : new BindingList<ModEntry>()
                    );

                    chooser.SelectedSteamUserChanged += (s3, e3) =>
                    {
                        try
                        {
                            RefreshProfileList();
                        }
                        catch { }
                    };

                    chooser.FormClosed += (s2, e2) =>
                    {
                        if (defaultCurrentSteamUser != Settings.Default.CurrentSteamUser)
                        {
                            RefreshProfileList();
                        }
                    };

                    FormUtil.ShowModalForm(this, chooser);
                };
            }
            catch { }
            RefreshProfileList();
            UpdateTheme.RegisterIcons(this);
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

        private void BtnToolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.AccessibleDescription is string tooltipText)
                ToolTipUtil.SetToolTip(button, tooltipText);
        }

        private void RefreshSettingsTheme()
        {
            var controlsToModify = new Control[]
            {
                BtnCreateProfile,
                BtnDeleteProfile,
                BtnDuplicateProfile,
                BtnFinishSelection,
                BtnRenameProfile,
                TextBoxUserInput
            };

            UpdateTheme.Refresh(this, controlsToModify);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            FormUtil.Close(this);
        }

        private void Animation_AnimationUpdated()
        {
            foreach (AnimatedItem item in _animation.AnimatedItems)
            {
                if (item.Panel == null)
                    continue;

                item.Panel.Top = (int)item.CurrentY;
                item.Panel.Visible = item.Visible;
            }

            innerPanel.Invalidate();
        }

        private void SyncAnimatedItems(bool animateNewRows)
        {
            Dictionary<object, AnimatedItem> existing = new Dictionary<object, AnimatedItem>(
                _animation.AnimationLookup
            );

            _animation.ClearItems();

            for (int i = 0; i < innerPanel.Controls.Count; i++)
            {
                if (!(innerPanel.Controls[i] is AnimatedPanel panel))
                    continue;

                string profile = panel.Tag as string;

                if (existing.TryGetValue(profile, out AnimatedItem row))
                {
                    row.Panel = panel;
                    row.Key = profile;
                    if (Math.Abs(row.CurrentY) < 0.1f && Math.Abs(row.TargetY) < 0.1f)
                    {
                        row.CurrentY = animateNewRows ? i * 28 + 55 : i * 28;
                    }

                    row.TargetY = i * 28;
                }
                else
                {
                    row = new AnimatedItem
                    {
                        Key = profile,
                        Panel = panel,

                        CurrentY = animateNewRows ? i * 28 + 55 : i * 28
                    };
                    if (Math.Abs(row.CurrentY) < 0.1f && Math.Abs(row.TargetY) < 0.1f)
                    {
                        row.CurrentY = animateNewRows ? i * 28 + 55 : i * 28;
                    }

                    row.TargetY = i * 28;
                    if (animateNewRows)
                    {
                        row.Opacity = 0f;
                        row.OffsetY = 8f;
                    }
                    else
                    {
                        row.Opacity = 1f;
                        row.OffsetY = 0f;
                    }

                    row.AnimationStart = _animation.AnimationClock.ElapsedMilliseconds;

                    row.Delay = i * 45;
                }

                _animation.RegisterItem(row);
            }

            _animation.Start();
        }

        private void RefreshProfileList()
        {
            string currentSteamUser = Settings.Default.CurrentSteamUser;
            if (string.IsNullOrWhiteSpace(currentSteamUser))
            {
                innerPanel.Controls.Clear();
                var lbl = new Label
                {
                    Text = "No Steam user selected. Click the profile icon to choose.",
                    Location = new Point(innerPanel.Width / 2 - 150, innerPanel.Height / 2 - 10),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    ForeColor = Settings.Default.PlaceholderColor
                };
                innerPanel.Controls.Add(lbl);
                return;
            }

            _profiles = ProfileManager.ListProfiles(currentSteamUser);

            try
            {
                string currentProfile = ProfileManager.GetCurrentProfileName(currentSteamUser);
                if (
                    !string.IsNullOrWhiteSpace(currentProfile) && _profiles.Contains(currentProfile)
                )
                {
                    if (_lastAppliedProfile != currentProfile)
                    {
                        _onLoad?.Invoke(currentProfile);
                        _lastAppliedProfile = currentProfile;
                    }
                }
                else
                {
                    _lastAppliedProfile = null;
                }
            }
            catch { }
            var oldRows = innerPanel
                .Controls.Cast<Control>()
                .Where(c => c.Tag is string)
                .ToDictionary(c => c.Tag as string, c => c);
            int idx = 0;
            var profileSet = new HashSet<string>(_profiles);

            foreach (Control c in innerPanel.Controls.Cast<Control>().ToList())
            {
                if (!profileSet.Contains(c.Tag as string))
                    innerPanel.Controls.Remove(c);
            }

            foreach (var p in _profiles)
            {
                AnimatedPanel row;

                if (oldRows.TryGetValue(p, out Control existing))
                {
                    row = (AnimatedPanel)existing;
                    row.Controls.Clear();
                }
                else
                {
                    row = new AnimatedPanel
                    {
                        Left = 0,
                        Top = 0,
                        Width = innerPanel.Width,
                        Height = 28
                    };

                    innerPanel.Controls.Add(row);
                }

                row.Tag = p;
                row.BackColor = Settings.Default.BGSecondary;
                PictureBox pbSave = null;
                Label lblCheck = null;
                var lbl = new Label
                {
                    Text = p,
                    Left = 6,
                    Top = 0,
                    Width = row.Width - 60,
                    Height = 28,
                    BackColor = Color.Transparent,
                    ForeColor = Settings.Default.TextColor,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                try
                {
                    string currentProfile = ProfileManager.GetCurrentProfileName(currentSteamUser);
                    bool isSelected =
                        !string.IsNullOrWhiteSpace(currentProfile) && currentProfile == p;
                    lblCheck = new Label
                    {
                        Text = isSelected ? "✔" : "",
                        ForeColor = Settings.Default.DetailActive,
                        Left = 6,
                        Top = 6,
                        Width = 20,
                        Height = 16,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.Transparent
                    };
                    row.Controls.Add(lblCheck);

                    if (SaveManager.HasProfileSave(currentSteamUser, p))
                    {
                        pbSave = new PictureBox
                        {
                            Image = Resources.SaveIcon,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Left = lblCheck.Right + 6,
                            Top = 6,
                            Width = 16,
                            Height = 16,
                            BackColor = Color.Transparent
                        };
                        row.Controls.Add(pbSave);
                        try
                        {
                            try
                            {
                                pbSave.Image?.Dispose();
                            }
                            catch { }
                            pbSave.Image = UpdateTheme.RecolorIcon(
                                Resources.SaveIcon,
                                Settings.Default.TextColor
                            );
                        }
                        catch { }
                        try
                        {
                            UpdateTheme.RegisterIcons(pbSave);
                        }
                        catch { }
                        lbl.Left = pbSave.Right + 6;
                        lbl.Width = row.Width - 60 - (pbSave.Width + 6) - (lblCheck.Width + 6);
                    }
                    else
                    {
                        lbl.Left = lblCheck.Right + 6;
                        lbl.Width = row.Width - 60 - (lblCheck.Width + 6);
                    }
                }
                catch { }

                lbl.Tag = idx;
                lbl.Click += (s, e) =>
                {
                    SelectIndex((int)((Label)s).Tag);
                };
                lbl.DoubleClick += (s, e) =>
                {
                    SelectIndex((int)((Label)s).Tag);
                    BtnFinishSelection.PerformClick();
                };

                int count = ProfileManager.GetEnabledFolders(currentSteamUser, p).Count;
                var lblCount = new Label
                {
                    Text = count.ToString(),
                    Width = 48,
                    Height = 28,
                    Left = row.Width - 74,
                    Top = 0,
                    TextAlign = ContentAlignment.MiddleRight,
                    ForeColor = Settings.Default.TextColor,
                    BackColor = Color.Transparent
                };

                row.Controls.Add(lbl);
                row.Controls.Add(lblCount);

                var menu = new ContextMenuStrip { Renderer = new CustomContextMenuStrip() };
                bool hasProfileSave = false;
                try
                {
                    hasProfileSave = SaveManager.HasProfileSave(currentSteamUser, p);
                }
                catch { }
                string toggleText = hasProfileSave
                    ? "Disable profile-specific save"
                    : "Enable profile-specific save";
                menu.Items.Add(
                    toggleText,
                    null,
                    (s, e) =>
                    {
                        TryToggleProfileSave(p, currentSteamUser);
                    }
                );

                if (hasProfileSave)
                {
                    menu.Items.Add(
                        "Delete profile-specific save",
                        null,
                        (s, e) =>
                        {
                            TryDeleteProfileSave(p, currentSteamUser);
                        }
                    );
                }

                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add(
                    "Load profile",
                    null,
                    (s, e) =>
                    {
                        TryLoadProfile(p, currentSteamUser);
                    }
                );
                menu.Items.Add(
                    "Open profile location",
                    null,
                    (s, e) =>
                    {
                        try
                        {
                            string profileDir = Path.Combine(
                                ProfileManager.UsersDirectory,
                                currentSteamUser,
                                p
                            );
                            System.Diagnostics.Process.Start("explorer.exe", profileDir);
                        }
                        catch { }
                    }
                );
                menu.Items.Add(
                    "Open backup location",
                    null,
                    (s, e) =>
                    {
                        try
                        {
                            string backupDir = SaveManager.GetProfileBackupDirectory(
                                currentSteamUser,
                                p
                            );
                            System.Diagnostics.Process.Start("explorer.exe", backupDir);
                        }
                        catch { }
                    }
                );
                menu.Items.Add(
                    "Rename profile",
                    null,
                    (s, e) =>
                    {
                        TryRenameProfile(p, currentSteamUser);
                    }
                );
                menu.Items.Add(
                    "Duplicate profile",
                    null,
                    (s, e) =>
                    {
                        TryDuplicateProfile(p, currentSteamUser);
                    }
                );
                menu.Items.Add(
                    "Delete profile",
                    null,
                    (s, e) =>
                    {
                        TryDeleteProfile(p, currentSteamUser);
                    }
                );

                row.ContextMenuStrip = menu;
                row.MouseEnter += (s, e) =>
                {
                    row.BackColor = Settings.Default.BGTertiary;
                };
                row.MouseLeave += (s, e) =>
                {
                    if (_selectedIndex != idx)
                        row.BackColor = Settings.Default.BGSecondary;
                };

                idx++;
            }
            try
            {
                var ordered = new List<Control>();
                foreach (var p in _profiles)
                {
                    var found = innerPanel
                        .Controls.Cast<Control>()
                        .FirstOrDefault(c =>
                            string.Equals(c.Tag as string, p, StringComparison.Ordinal)
                        );
                    if (found != null)
                        ordered.Add(found);
                }
                innerPanel.Controls.Clear();
                for (int i2 = 0; i2 < ordered.Count; i2++)
                {
                    var panel = ordered[i2];
                    panel.Top = i2 * 28;
                    innerPanel.Controls.Add(panel);
                }
            }
            catch { }

            innerPanel.Height = Math.Max(PanelContent.Height, _profiles.Count * 28);
            UpdateScroll();
            SyncAnimatedItems(true);
        }

        private void TryLoadProfile(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryLoadProfile(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Loading the profile will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                if (_getMods != null)
                {
                    try
                    {
                        ProfileManager.LoadProfile(steamUserId, profileName, _getMods());
                    }
                    catch { }
                }
                ProfileManager.SetCurrentProfileName(steamUserId, profileName);
                _onLoad?.Invoke(profileName);
                try
                {
                    RefreshModBrowsers();
                }
                catch { }
                RefreshProfileList();
            }
            catch { }
        }

        private void TryRenameProfile(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryRenameProfile(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Renaming will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                var inputDialog = new FormUserInputDialog("Rename", profileName);
                inputDialog.FormClosed += (s, e) =>
                {
                    try
                    {
                        if (!inputDialog.Confirmed)
                            return;

                        string userInput = inputDialog.TxtUserInput.Text;
                        if (!string.IsNullOrWhiteSpace(userInput) && userInput != profileName)
                        {
                            if (ProfileManager.RenameProfile(steamUserId, profileName, userInput))
                            {
                                RefreshProfileList();
                                try
                                {
                                    int newIndex = _profiles.IndexOf(userInput);
                                    if (newIndex >= 0)
                                        SelectIndex(newIndex);
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        try
                        {
                            inputDialog.Dispose();
                        }
                        catch { }
                    }
                };

                FormUtil.ShowModalForm(this, inputDialog, false);
            }
            catch { }
        }

        private void TryDuplicateProfile(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryDuplicateProfile(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Duplicating will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                string baseName = profileName + "_copy";
                string newName = baseName;
                int i = 1;
                var existing = ProfileManager.ListProfiles(steamUserId);
                while (existing.Contains(newName) && i < 1000)
                {
                    newName = baseName + i.ToString();
                    i++;
                }

                if (ProfileManager.DuplicateProfile(steamUserId, profileName, newName))
                    RefreshProfileList();
            }
            catch { }
        }

        private void TryDeleteProfile(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryDeleteProfile(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Deletion will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                if (
                    MessageBoxUtil.Show(
                        "Delete Profile",
                        $"Are you sure you want to delete profile '{profileName}'?",
                        true
                    ) != DialogResult.Yes
                )
                    return;

                if (ProfileManager.DeleteProfile(steamUserId, profileName))
                    RefreshProfileList();
                else
                    ToastUtil.CreateToast(Color.Red, "Cannot delete the last profile.");
            }
            catch { }
        }

        private void TryToggleProfileSave(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryToggleProfileSave(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Toggling profile save will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                bool has = SaveManager.HasProfileSave(steamUserId, profileName);
                if (!has)
                {
                    var userDataDir = Settings.Default.UserDataPath;
                    if (!string.IsNullOrWhiteSpace(userDataDir))
                    {
                        DebugUtil.Log(
                            DebugLevel.INFO,
                            DebugFilter.PROFILE,
                            $"TryToggleProfileSave: enabling profile save for {steamUserId}/{profileName}, userDataDir={userDataDir}"
                        );
                        bool ok = SaveManager.CaptureCurrentSaveToProfile(
                            userDataDir,
                            steamUserId,
                            profileName
                        );
                        if (!ok)
                        {
                            MessageBoxUtil.Show(
                                "Profile Save",
                                "Unable to create profile-specific save. Make sure the Steam userdata and global save exist.",
                                false
                            );
                        }
                        else
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.PROFILE,
                                $"TryToggleProfileSave: created profile save {SaveManager.GetProfileSavePath(steamUserId, profileName)}"
                            );
                    }
                }
                else
                {
                    try
                    {
                        string profileSave = SaveManager.GetProfileSavePath(
                            steamUserId,
                            profileName
                        );
                        if (File.Exists(profileSave))
                        {
                            string bakDir = SaveManager.GetProfileBackupDirectory(
                                steamUserId,
                                profileName
                            );
                            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                            string dest = Path.Combine(bakDir, $"{timestamp} cc_save.dat.disabled");
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.PROFILE,
                                $"TryToggleProfileSave: moving {profileSave} -> {dest}"
                            );
                            File.Move(profileSave, dest);
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.PROFILE,
                                $"TryToggleProfileSave: moved profile save to backups"
                            );
                        }
                    }
                    catch { }
                }

                RefreshProfileList();
            }
            catch { }
        }

        private void TryDeleteProfileSave(string profileName, string steamUserId)
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                TryDeleteProfileSave(profileName, steamUserId);
                            }
                            catch { }
                        });
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is in progress. Deleting profile save will be performed after it completes."
                        );
                    }
                    catch { }
                    return;
                }
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                if (
                    MessageBoxUtil.Show(
                        "Delete Profile Save",
                        $"Are you sure you want to permanently delete the profile-specific save for '{profileName}'?",
                        true
                    ) != DialogResult.Yes
                )
                    return;

                if (SaveManager.DeleteProfileSave(steamUserId, profileName))
                    RefreshProfileList();
                else
                    MessageBox.Show(
                        "Unable to delete profile save.",
                        "Delete Profile Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }
            catch { }
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

        private void UpdateScroll()
        {
            int total = innerPanel.Height;
            int view = PanelContent.Height;
            vScroll.Minimum = 0;
            vScroll.Maximum = Math.Max(0, total - view);
            vScroll.LargeChange = view;
            vScroll.Value = Math.Max(0, Math.Min(vScroll.Value, vScroll.Maximum));
            innerPanel.Top = -vScroll.Value;

            vScroll.Enabled = total > view;

            if (!vScroll.Enabled)
            {
                vScroll.Value = 0;
            }
            else
            {
                vScroll.Value = Math.Min(vScroll.Value, vScroll.Maximum);
            }
        }

        private void SelectIndex(int i)
        {
            if (i < 0 || i >= _profiles.Count)
                return;
            _selectedIndex = i;
            for (int j = 0; j < innerPanel.Controls.Count; j++)
            {
                if (!(innerPanel.Controls[j] is AnimatedPanel row))
                    continue;
                if (j == i)
                {
                    row.BackColor = Settings.Default.BGTertiary;
                }
                else
                {
                    row.BackColor = Settings.Default.BGSecondary;
                }

                if (_animation.AnimationLookup.TryGetValue((string)row.Tag, out AnimatedItem anim))
                {
                    anim.TargetY = j * 28;
                }
            }
            _animation.Start();
        }

        private void BtnFinishSelection_Click(object sender, EventArgs e)
        {
            if (_selectedIndex < 0 || _selectedIndex >= _profiles.Count)
                return;
            var sel = _profiles[_selectedIndex];
            if (string.IsNullOrWhiteSpace(sel))
                return;
            try
            {
                _onLoad?.Invoke(sel);
                try
                {
                    RefreshModBrowsers();
                }
                catch { }
            }
            catch { }
            this.Close();
        }

        private void RefreshModBrowsers()
        {
            try
            {
                var open = FormUtil.GetAllOpenForms();
                foreach (var f in open)
                {
                    try
                    {
                        foreach (Control c in f.Controls)
                        {
                            var mb = FindModBrowserRecursive(c);
                            if (mb != null)
                            {
                                try
                                {
                                    mb.RefreshDisplayWithAnimation();
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private ModBrowser FindModBrowserRecursive(Control root)
        {
            if (root == null)
                return null;
            if (root is ModBrowser mb)
                return mb;
            foreach (Control c in root.Controls)
            {
                var found = FindModBrowserRecursive(c);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string currentSteamUser = Settings.Default.CurrentSteamUser;
            if (string.IsNullOrWhiteSpace(currentSteamUser))
            {
                MessageBox.Show(
                    "No Steam user selected. Click the profile icon to choose.",
                    "Create Profile",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var name = TextBoxUserInput.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                name = "NewProfile (" + _profiles.Count + ")";
            if (name.Length > 32)
                name = name.Substring(0, 32);

            try
            {
                if (!ProfileManager.ListProfiles(currentSteamUser).Contains(name))
                {
                    if (_getMods != null)
                    {
                        try
                        {
                            ProfileManager.SaveProfile(currentSteamUser, name, _getMods());
                        }
                        catch
                        {
                            ProfileManager.SaveProfile(
                                currentSteamUser,
                                name,
                                new BindingList<ModEntry>()
                            );
                        }
                    }
                    else
                    {
                        ProfileManager.SaveProfile(
                            currentSteamUser,
                            name,
                            new BindingList<ModEntry>()
                        );
                    }
                }
            }
            catch { }
            RefreshProfileList();
            TextBoxUserInput.Text = string.Empty;
        }

        private void BtnDuplicate_Click(object sender, EventArgs e)
        {
            string currentSteamUser = Settings.Default.CurrentSteamUser;
            if (string.IsNullOrWhiteSpace(currentSteamUser))
                return;

            if (_selectedIndex < 0 || _selectedIndex >= _profiles.Count)
                return;
            var sel = _profiles[_selectedIndex];
            if (string.IsNullOrWhiteSpace(sel))
                return;
            TryDuplicateProfile(sel, currentSteamUser);
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            string currentSteamUser = Settings.Default.CurrentSteamUser;
            if (string.IsNullOrWhiteSpace(currentSteamUser))
                return;

            if (_selectedIndex < 0 || _selectedIndex >= _profiles.Count)
                return;
            var sel = _profiles[_selectedIndex];
            if (string.IsNullOrWhiteSpace(sel))
                return;
            TryRenameProfile(sel, currentSteamUser);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string currentSteamUser = Settings.Default.CurrentSteamUser;
            if (string.IsNullOrWhiteSpace(currentSteamUser))
                return;

            if (_selectedIndex < 0 || _selectedIndex >= _profiles.Count)
                return;
            var sel = _profiles[_selectedIndex];
            if (string.IsNullOrWhiteSpace(sel))
                return;
            TryDeleteProfile(sel, currentSteamUser);
        }
    }
}
