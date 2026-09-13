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
using QuillsModManagerV2.Util;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls
{
    public class ModBrowser : UserControl
    {
        private readonly ModListView listViewMods;
        private readonly ImageList imageListIcons;
        private readonly ContextMenuStrip contextMenu;
        private readonly ToolStripMenuItem renameSeparatorMenuItem;
        private readonly ToolStripMenuItem setSeparatorColorMenuItem;
        private readonly ToolStripMenuItem deleteSeparatorMenuItem;

        private BindingList<ModEntry> mods = new BindingList<ModEntry>();
        private BindingList<ModEntry> _displayMods = null;
        private string _lastModPath = null;
        private string _lastSearch = null;
        private bool _suspendProfileSave = false;
        private bool _isApplyingProfile = false;
        private FileSystemWatcher _modPathWatcher;
        private Timer _fileRefreshTimer;

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
            listViewMods.WarningClicked += (idx) => RepairMissingDataFolder(idx);
            listViewMods.SeparatorRenamed += (separator) =>
            {
                ModRegistry.Save(mods);
                listViewMods.Invalidate(false);
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
                    bool isSeparator = listViewMods.SelectedItem != null
                        && listViewMods.SelectedItem.IsSeparator;
                    renameSeparatorMenuItem.Visible = isSeparator;
                    setSeparatorColorMenuItem.Visible = isSeparator;
                    deleteSeparatorMenuItem.Visible = isSeparator;
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
            contextMenu.Items.Add("Add separator", null, (s, e) => Context_AddSeparator());
            renameSeparatorMenuItem = new ToolStripMenuItem(
                "Rename separator",
                null,
                (s, e) => Context_RenameSeparator()
            );
            setSeparatorColorMenuItem = new ToolStripMenuItem(
                "Set separator color",
                null,
                (s, e) => Context_SetSeparatorColor()
            );
            deleteSeparatorMenuItem = new ToolStripMenuItem(
                "Delete separator",
                null,
                (s, e) => Context_DeleteSeparator()
            );
            contextMenu.Items.Add(renameSeparatorMenuItem);
            contextMenu.Items.Add(setSeparatorColorMenuItem);
            contextMenu.Items.Add(deleteSeparatorMenuItem);
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
            _fileRefreshTimer = new Timer { Interval = 350 };
            _fileRefreshTimer.Tick += (s, e) =>
            {
                _fileRefreshTimer.Stop();
                ReloadModsFromCurrentPath(false);
            };
            ConfigureModPathWatcher(_lastModPath);

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
                    ReloadModsFromCurrentPath(true);
                }
            }
            catch { }
        }

        private void ReloadModsFromCurrentPath(bool animate)
        {
            try
            {
                string path = Settings.Default.ModPath;
                ConfigureModPathWatcher(path);
                LoadModsFromFolder(path, applySavedEnabled: true);
                RefreshListView(animate);
                if (animate)
                    listViewMods.RefreshWithAnimation();
            }
            catch { }
        }

        private void ConfigureModPathWatcher(string path)
        {
            if (_modPathWatcher != null)
            {
                _modPathWatcher.EnableRaisingEvents = false;
                _modPathWatcher.Dispose();
                _modPathWatcher = null;
            }

            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                return;

            try
            {
                _modPathWatcher = new FileSystemWatcher(path)
                {
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.FileName
                        | NotifyFilters.DirectoryName
                        | NotifyFilters.LastWrite
                        | NotifyFilters.Size,
                    Filter = "*"
                };
                _modPathWatcher.Created += ModPathChanged;
                _modPathWatcher.Deleted += ModPathChanged;
                _modPathWatcher.Changed += ModPathChanged;
                _modPathWatcher.Renamed += ModPathChanged;
                _modPathWatcher.Error += (s, e) => ScheduleFileRefresh();
                _modPathWatcher.EnableRaisingEvents = true;
            }
            catch
            {
                _modPathWatcher?.Dispose();
                _modPathWatcher = null;
            }
        }

        private void ModPathChanged(object sender, FileSystemEventArgs e)
        {
            ScheduleFileRefresh();
        }

        private void ScheduleFileRefresh()
        {
            try
            {
                if (IsDisposed || !IsHandleCreated)
                    return;
                BeginInvoke((Action)(() =>
                {
                    if (_fileRefreshTimer != null)
                    {
                        _fileRefreshTimer.Stop();
                        _fileRefreshTimer.Start();
                    }
                }));
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
                    GetActualMods(),
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

        private void RepairMissingDataFolder(int index)
        {
            try
            {
                if (_displayMods == null || index < 0 || index >= _displayMods.Count)
                    return;

                var mod = _displayMods[index];
                if (mod == null || string.IsNullOrWhiteSpace(mod.Path))
                    return;

                string dataPath = Path.Combine(mod.Path, "data");
                Directory.CreateDirectory(dataPath);
                string[] foldersToMove =
                {
                    "bsps", "fonts", "game", "levels", "music", "pbplocal", "shaders", "sounds"
                };

                foreach (string folderName in foldersToMove)
                {
                    string sourcePath = Path.Combine(mod.Path, folderName);
                    if (!Directory.Exists(sourcePath))
                        continue;

                    string destinationPath = Path.Combine(dataPath, folderName);
                    MoveDirectoryContents(sourcePath, destinationPath);
                    if (!Directory.EnumerateFileSystemEntries(sourcePath).Any())
                        Directory.Delete(sourcePath);
                }

                ReloadModsFromCurrentPath(true);
            }
            catch (Exception ex)
            {
                try
                {
                    DebugUtil.Log(
                        DebugLevel.ERROR,
                        DebugFilter.MODMANAGER,
                        $"RepairMissingDataFolder: {ex.Message}"
                    );
                }
                catch { }
            }
        }

        private static void MoveDirectoryContents(string sourcePath, string destinationPath)
        {
            Directory.CreateDirectory(destinationPath);
            foreach (string filePath in Directory.GetFiles(sourcePath))
            {
                string destinationFile = Path.Combine(destinationPath, Path.GetFileName(filePath));
                if (!File.Exists(destinationFile))
                    File.Move(filePath, destinationFile);
            }

            foreach (string childDirectory in Directory.GetDirectories(sourcePath))
            {
                string destinationDirectory = Path.Combine(
                    destinationPath,
                    Path.GetFileName(childDirectory)
                );
                MoveDirectoryContents(childDirectory, destinationDirectory);
                if (!Directory.EnumerateFileSystemEntries(childDirectory).Any())
                    Directory.Delete(childDirectory);
            }
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
            {
                _suspendProfileSave = false;
                return;
            }

            var dirs = Directory.GetDirectories(folderPath);
            foreach (var dir in dirs)
            {
                try
                {
                    var infoPath = Path.Combine(dir, "modinfo.txt");
                    var iconPath = Path.Combine(dir, "modicon.png");

                    var entry = new ModEntry
                    {
                        Path = dir,
                        MissingDataFolder = !Directory.Exists(Path.Combine(dir, "data"))
                    };

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

            var layout = ModRegistry.LoadLayout(out var enabledMap);
            if (layout != null && layout.Count > 0)
            {
                var ordered = new List<ModEntry>();
                foreach (var item in layout)
                {
                    ModEntry found = null;
                    if (item.IsSeparator)
                    {
                        found = new ModEntry
                        {
                            IsSeparator = true,
                            SeparatorName = string.IsNullOrWhiteSpace(item.SeparatorName)
                                ? "Section"
                                : item.SeparatorName,
                            SeparatorColorArgb = item.SeparatorColorArgb == 0
                                ? Settings.Default.DetailColor.ToArgb()
                                : item.SeparatorColorArgb
                        };
                    }
                    else if (!string.IsNullOrWhiteSpace(item.Folder))
                    {
                        string path = Path.Combine(folderPath, item.Folder);
                        found = mods.FirstOrDefault(x =>
                            string.Equals(x.Path, path, StringComparison.OrdinalIgnoreCase)
                        );
                    }
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
                    if (!m.IsSeparator && enabledMap.TryGetValue(m.Path, out var en))
                        m.Enabled = en;
                }
            }
            else
            {
                foreach (var m in GetActualMods())
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    Settings.Default.PropertyChanged -= Settings_PropertyChanged;
                }
                catch { }
                _modPathWatcher?.Dispose();
                _fileRefreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private BindingList<ModEntry> GetActualMods()
        {
            return new BindingList<ModEntry>(mods.Where(m => !m.IsSeparator).ToList());
        }

        public List<ModEntry> GetMods() => GetActualMods().ToList();

        public List<string> GetModNames() => GetActualMods().Select(m => m.Title).ToList();

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
                var filtered = new List<ModEntry>();
                ModEntry pendingSeparator = null;
                foreach (var mod in mods)
                {
                    if (mod.IsSeparator)
                    {
                        pendingSeparator = mod;
                        continue;
                    }
                    if (
                        !string.IsNullOrWhiteSpace(mod.Title)
                        && mod.Title.Replace(" ", "").ToLowerInvariant().Contains(q)
                    )
                    {
                        if (pendingSeparator != null)
                        {
                            filtered.Add(pendingSeparator);
                            pendingSeparator = null;
                        }
                        filtered.Add(mod);
                    }
                }
                newDisplay = new BindingList<ModEntry>(filtered);
            }

            bool changed = _displayMods == null || !_displayMods.SequenceEqual(newDisplay);

            if (changed)
            {
                _displayMods = newDisplay;
                listViewMods.Items = _displayMods;

            }

            if (animate)
                listViewMods.RefreshWithAnimation();

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

        private void Context_AddSeparator()
        {
            int insertAt = mods.Count;
            var selected = listViewMods.SelectedItem;
            if (selected != null)
            {
                int selectedIndex = mods.IndexOf(selected);
                if (selectedIndex >= 0)
                    insertAt = selectedIndex + 1;
            }
            mods.Insert(insertAt, new ModEntry
            {
                IsSeparator = true,
                SeparatorName = "Section",
                SeparatorColorArgb = Settings.Default.BGTertiary.ToArgb()
            });
            ModRegistry.Save(mods);
            RefreshListView(true);
        }

        private void Context_RenameSeparator()
        {
            var separator = listViewMods.SelectedItem;
            if (separator == null || !separator.IsSeparator)
                return;
            listViewMods.BeginEditSelectedSeparator();
        }

        private void Context_SetSeparatorColor()
        {
            var separator = listViewMods.SelectedItem;
            if (separator == null || !separator.IsSeparator)
                return;
            var selector = new QuillsModManagerV2.infoforms.UserInput.FormColorSelector();
            Color initialColor = separator.SeparatorColorArgb == 0
                    ? Settings.Default.BGTertiary
                    : Color.FromArgb(separator.SeparatorColorArgb);
            selector.UpdateUIFromColor(initialColor);
            selector.ColorChanged += (color) =>
            {
                separator.SeparatorColorArgb = color.ToArgb();
                listViewMods.Invalidate(false);
            };
            selector.FormClosed += (s, e) => ModRegistry.Save(mods);
            Form owner = listViewMods.FindForm();
            if (owner == null)
            {
                selector.Dispose();
                return;
            }
            FormUtil.ShowModalForm(owner, selector, false);
        }

        private void Context_DeleteSeparator()
        {
            var separator = listViewMods.SelectedItem;
            if (separator == null || !separator.IsSeparator)
                return;
            mods.Remove(separator);
            ModRegistry.Save(mods);
            RefreshListView(true);
        }

        private void Context_ToggleSelected()
        {
            var sels = listViewMods.SelectedIndices.ToList();
            if (sels.Count == 0)
                return;

            var selected = sels.Where(i => i >= 0 && i < _displayMods.Count)
                .Select(i => _displayMods[i])
                .Where(m => !m.IsSeparator)
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
