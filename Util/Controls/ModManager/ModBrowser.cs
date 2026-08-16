using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls
{
    public class ModBrowser : UserControl
    {
        private readonly ModListView listViewMods;
        private readonly ImageList imageListIcons;
        private readonly ContextMenuStrip contextMenu;

        private BindingList<ModEntry> mods = new BindingList<ModEntry>();
        private BindingList<ModEntry> _displayMods = null;
        private string _lastModPath = null;
        private string _lastSearch = null;
        private bool _suspendProfileSave = false;
        private bool _isApplyingProfile = false;

        public ModBrowser()
        {
            imageListIcons = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit,
                TransparentColor = Color.Transparent
            };

            listViewMods = new ModListView { Dock = DockStyle.Fill };

            var set = Settings.Default;
            listViewMods.BackColor = set.BGTertiary;
            listViewMods.ForeColor = set.TextColor;
            listViewMods.Font = new Font("Segoe UI", 9F);

            contextMenu = new ContextMenuStrip
            {
                Renderer = new CustomContextMenuStrip(),
                BackColor = set.BGSecondary,
                ForeColor = set.TextColor,
                ShowImageMargin = false
            };

            _displayMods = mods;
            listViewMods.Items = _displayMods;
            listViewMods.ItemsReordered += () =>
            {
                listViewMods.Invalidate(false);

                ModRegistry.Save(mods);

                if (!_suspendProfileSave && !_isApplyingProfile)
                    SaveCurrentProfileIfSet();
            };
            listViewMods.ToggleChanged += (idx, enabled) =>
            {
                CheckConflicts();
                listViewMods.Invalidate(false);
                ModRegistry.Save(mods);

                if (!_suspendProfileSave && !_isApplyingProfile)
                    SaveCurrentProfileIfSet();
            };

            mods.ListChanged += Mods_ListChanged;
            listViewMods.ItemRightClicked += (idx) =>
            {
                if (idx >= 0)
                {
                    listViewMods.Invalidate();
                    contextMenu.Show(listViewMods, listViewMods.PointToClient(Cursor.Position));
                }
            };

            contextMenu.Items.Add("Open folder", null, (s, e) => Context_OpenFolder());
            contextMenu.Items.Add("Edit info", null, (s, e) => Context_Edit());
            contextMenu.Items.Add(
                "Refresh mod list",
                null,
                (s, e) =>
                {
                    RefreshListView();
                    listViewMods.RefreshWithAnimation();
                }
            );
            contextMenu.Items.Add("Toggle mod(s)", null, (s, e) => Context_ToggleSelected());
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Deploy mods", null, async (s, e) => await DeployModsAsync());
            contextMenu.Items.Add("Undeploy mods", null, async (s, e) => await UndeployModsAsync());

            Controls.Add(listViewMods);

            listViewMods.Visible = false;

            try
            {
                Settings.Default.PropertyChanged += Settings_PropertyChanged;
            }
            catch { }

            _lastModPath = Settings.Default.ModPath;

            this.HandleCreated += (s, e) =>
            {
                BeginInvoke(
                    new Action(() =>
                    {
                        listViewMods.Visible = true;
                        listViewMods.PlayEntryAnimation();
                    })
                );
            };
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "ModPath")
                {
                    if (this.InvokeRequired)
                    {
                        try
                        {
                            this.BeginInvoke((Action)(() => Settings_PropertyChanged(sender, e)));
                        }
                        catch { }
                        return;
                    }

                    string current = Settings.Default.ModPath;
                    if (string.Equals(current, _lastModPath, StringComparison.OrdinalIgnoreCase))
                        return;

                    _lastModPath = current;

                    try
                    {
                        LoadModsFromFolder(current, applySavedEnabled: true);
                        RefreshListView(false);
                        listViewMods.RefreshWithAnimation();
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void Mods_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!_suspendProfileSave && !_isApplyingProfile)
                SaveCurrentProfileIfSet();
        }

        private void SaveCurrentProfileIfSet()
        {
            try
            {
                var steamUser = Settings.Default.CurrentSteamUser;
                if (string.IsNullOrWhiteSpace(steamUser))
                    return;
                var cur = ProfileManager.GetCurrentProfileName(steamUser);
                if (!string.IsNullOrWhiteSpace(cur))
                {
                    ProfileManager.SaveProfile(steamUser, cur, mods);
                }
            }
            catch { }
        }

        public async Task DeployModsAsync(
            IProgress<DeploymentManager.DeploymentProgress> progress = null
        )
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        ToastUtil.CreateToast(Color.Orange, "A deployment is already running.");
                    }
                    catch { }
                    return;
                }

                var meta = await DeploymentManager.DeployWithSafetyAsync(
                    mods,
                    Settings.Default.GamePath,
                    progress
                );
            }
            catch (Exception ex)
            {
                try
                {
                    DebugUtil.Log(
                        DebugLevel.ERROR,
                        DebugFilter.DEPLOYMENT,
                        $"DeployModsAsync: error: {ex.Message}"
                    );
                }
                catch { }
            }
        }

        public async Task UndeployModsAsync(
            IProgress<DeploymentManager.DeploymentProgress> progress = null
        )
        {
            try
            {
                if (DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        ToastUtil.CreateToast(Color.Orange, "A deployment is already running.");
                    }
                    catch { }
                    return;
                }

                await DeploymentManager.UndeployAsync(progress);
            }
            catch { }
        }

        public void LoadModsFromFolder(string folderPath, bool applySavedEnabled = true)
        {
            try
            {
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEPLOYMENT,
                    $"LoadModsFromFolder: {folderPath}, applySavedEnabled={applySavedEnabled}"
                );
            }
            catch { }
            _suspendProfileSave = true;
            mods.Clear();
            imageListIcons.Images.Clear();

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
                return;

            var dirs = Directory.GetDirectories(folderPath);
            foreach (var dir in dirs)
            {
                try
                {
                    var infoPath = Path.Combine(dir, "modinfo.txt");
                    var iconPath = Path.Combine(dir, "modicon.png");

                    var entry = new ModEntry { Path = dir };

                    if (File.Exists(infoPath))
                    {
                        var lines = File.ReadAllLines(infoPath);
                        foreach (var raw in lines)
                        {
                            var line = raw.Trim();
                            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                                continue;
                            var idx = line.IndexOf('=');
                            if (idx <= 0)
                                continue;
                            var key = line.Substring(0, idx).Trim();
                            var val = line.Substring(idx + 1).Trim();
                            switch (key)
                            {
                                case "ModTitle":
                                    entry.Title = val;
                                    break;
                                case "ModAuthor":
                                    entry.Author = val;
                                    break;
                                case "ModVersion":
                                    entry.Version = val;
                                    break;
                                case "ModDescription":
                                    entry.Description = val;
                                    break;
                            }
                        }
                    }

                    if (File.Exists(iconPath))
                    {
                        try
                        {
                            entry.Icon = Image.FromFile(iconPath);
                        }
                        catch
                        {
                            entry.Icon = Resources.PictureMedium;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(entry.Title))
                        entry.Title = Path.GetFileName(dir);
                    if (string.IsNullOrWhiteSpace(entry.Author))
                        entry.Author = "Unknown";
                    if (string.IsNullOrWhiteSpace(entry.Version))
                        entry.Version = string.Empty;
                    if (string.IsNullOrWhiteSpace(entry.Description))
                        entry.Description = string.Empty;

                    mods.Add(entry);
                }
                catch (Exception ex)
                {
                    DebugUtil.Log(
                        DebugLevel.ERROR,
                        DebugFilter.MODMANAGER,
                        $"LoadModsFromFolder: Error loading mod from {dir}: {ex.Message}"
                    );
                }
            }

            var order = ModRegistry.LoadOrder(out var enabledMap);
            if (order != null && order.Count > 0)
            {
                var ordered = new List<ModEntry>();
                foreach (var p in order)
                {
                    var found = mods.FirstOrDefault(x =>
                        string.Equals(x.Path, p, StringComparison.OrdinalIgnoreCase)
                    );
                    if (found != null)
                        ordered.Add(found);
                }
                foreach (var m in mods.ToList())
                    if (!ordered.Contains(m))
                        ordered.Add(m);
                mods.Clear();
                foreach (var m in ordered)
                    mods.Add(m);
            }
            if (applySavedEnabled)
            {
                foreach (var m in mods)
                {
                    if (enabledMap.TryGetValue(m.Path, out var en))
                        m.Enabled = en;
                }
            }
            else
            {
                foreach (var m in mods)
                    m.Enabled = false;
            }

            CheckConflicts();
            RefreshListView();
            try
            {
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.UI,
                    $"LoadModsFromFolder: loaded {mods.Count} mods"
                );
            }
            catch { }
            _suspendProfileSave = false;
        }

        public List<ModEntry> GetMods() => mods.ToList();

        public List<string> GetModNames() => mods.Select(m => m.Title).ToList();

        private void RefreshListView(bool animate = false)
        {
            BindingList<ModEntry> newDisplay;

            if (string.IsNullOrWhiteSpace(_lastSearch))
            {
                newDisplay = mods;
            }
            else
            {
                string q = new string(
                    _lastSearch.Where(c => !char.IsWhiteSpace(c)).ToArray()
                ).ToLowerInvariant();

                newDisplay = new BindingList<ModEntry>(
                    mods.Where(m =>
                            !string.IsNullOrWhiteSpace(m.Title)
                            && m.Title.Replace(" ", "").ToLowerInvariant().Contains(q)
                        )
                        .ToList()
                );
            }

            bool changed = _displayMods == null || !_displayMods.SequenceEqual(newDisplay);

            if (changed)
            {
                _displayMods = newDisplay;
                listViewMods.Items = _displayMods;

                if (animate)
                    listViewMods.RefreshWithAnimation();
            }

            listViewMods.Invalidate(false);
        }

        public void Search(string query)
        {
            _lastSearch = query ?? "";
            RefreshListView(true);
        }

        public void RefreshDisplayWithAnimation()
        {
            try
            {
                RefreshListView(true);
                try
                {
                    listViewMods.RefreshWithAnimation();
                }
                catch { }
            }
            catch { }
        }

        public void ApplyProfile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;
            try
            {
                try
                {
                    DebugUtil.Log(DebugLevel.INFO, DebugFilter.PROFILE, $"ApplyProfile: {name}");
                }
                catch { }
                _isApplyingProfile = true;
                var steamUser = Settings.Default.CurrentSteamUser;
                ProfileManager.LoadProfile(steamUser, name, mods);
                CheckConflicts();
                RefreshListView();
                ModRegistry.Save(mods);
                try
                {
                    DebugUtil.Log(
                        DebugLevel.INFO,
                        DebugFilter.PROFILE,
                        $"ApplyProfile: {name} applied"
                    );
                }
                catch { }
                _isApplyingProfile = false;
            }
            catch { }
        }

        private void Context_ToggleSelected()
        {
            var sels = listViewMods.SelectedIndices.ToList();
            if (sels.Count == 0)
                return;

            var selected = sels.Where(i => i >= 0 && i < _displayMods.Count)
                .Select(i => _displayMods[i])
                .ToList();

            if (selected.Count == 0)
                return;

            bool newState = !selected.First().Enabled;

            foreach (var mod in selected)
            {
                mod.Enabled = newState;
            }

            CheckConflicts();

            listViewMods.Invalidate(false);

            ModRegistry.Save(mods);

            if (!_suspendProfileSave && !_isApplyingProfile)
                SaveCurrentProfileIfSet();
        }

        private void CheckConflicts()
        {
            foreach (var m in mods)
                m.Conflicts.Clear();
            var fileMap = new Dictionary<string, List<ModEntry>>(StringComparer.OrdinalIgnoreCase);
            foreach (var m in mods)
            {
                if (string.IsNullOrWhiteSpace(m.Path))
                    continue;
                var dataDir = Path.Combine(m.Path, "data");
                if (!Directory.Exists(dataDir))
                    continue;
                try
                {
                    var files = Directory.GetFiles(dataDir, "*", SearchOption.AllDirectories);
                    foreach (var f in files)
                    {
                        var name = Path.GetFileName(f).ToLowerInvariant();
                        if (!fileMap.TryGetValue(name, out var list))
                        {
                            list = new List<ModEntry>();
                            fileMap[name] = list;
                        }
                        list.Add(m);
                    }
                }
                catch { }
            }
            foreach (var kv in fileMap)
            {
                var list = kv.Value;
                var enabledList = list.Where(x => x.Enabled).ToList();
                if (enabledList.Count <= 1)
                    continue;
                foreach (var m in enabledList)
                {
                    foreach (var other in enabledList)
                    {
                        if (other == m)
                            continue;
                        if (!m.Conflicts.TryGetValue(other.Title, out var files))
                        {
                            files = new List<string>();
                            m.Conflicts[other.Title] = files;
                        }
                        if (!files.Contains(kv.Key))
                            files.Add(kv.Key);
                    }
                }
            }
        }

        #region Context menu and interactions
        private void Context_OpenFolder()
        {
            var sel = GetSelectedEntry();
            if (sel == null)
                return;
            try
            {
                Process.Start(sel.Path);
            }
            catch { }
        }

        private void Context_Edit()
        {
            var sel = GetSelectedEntry();
            if (sel == null)
                return;
            var infoPath = Path.Combine(sel.Path, "modinfo.txt");
            try
            {
                if (!File.Exists(infoPath))
                    File.WriteAllText(infoPath, "ModTitle=" + sel.Title + Environment.NewLine);
                Process.Start(infoPath);
            }
            catch { }
        }

        private ModEntry GetSelectedEntry()
        {
            return listViewMods.SelectedItem;
        }

        public void PlayEntryAnimation()
        {
            listViewMods.PlayEntryAnimation();
        }
        #endregion
    }

    internal class ThemeColorTable : ProfessionalColorTable
    {
        private readonly Settings s;

        public ThemeColorTable(Settings settings)
        {
            s = settings ?? Settings.Default;
        }

        public override Color ToolStripGradientBegin => s.BGSecondary;
        public override Color ToolStripGradientEnd => s.BGTertiary;
        public override Color MenuItemSelected => s.BGTertiary;
        public override Color MenuItemSelectedGradientBegin => s.BGTertiary;
        public override Color MenuItemSelectedGradientEnd => s.BGTertiary;
        public override Color MenuItemPressedGradientBegin => s.DetailActive;
        public override Color MenuItemPressedGradientEnd => s.DetailActive;
        public override Color ToolStripDropDownBackground => s.BGSecondary;
        public override Color ImageMarginGradientBegin => s.BGSecondary;
        public override Color ImageMarginGradientMiddle => s.BGSecondary;
        public override Color ImageMarginGradientEnd => s.BGSecondary;
        public override Color MenuBorder => s.DetailColor;
        public override Color SeparatorDark => s.DetailColor;
        public override Color SeparatorLight => s.BGSecondary;
    }
}
