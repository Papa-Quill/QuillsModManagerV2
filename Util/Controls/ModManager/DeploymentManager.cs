using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util.Controls.ModManager;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls
{
    public class DeploymentMetadata
    {
        public string BackupFolder { get; set; }
        public List<string> DeployedFiles { get; set; } = new List<string>();
        public string SavedGlobalSaveBackup { get; set; }
        public string SteamUserId { get; set; }
        public string ProfileName { get; set; }
        public bool ProfileSaveDeployed { get; set; }
    }

    public static class DeploymentManager
    {
        public static event Action<string, bool> OperationStatusChanged;

        private static int _activeOperations = 0;
        private static readonly object _queueLock = new object();
        private static readonly Queue<Action> _postOperationQueue = new Queue<Action>();

        public static bool IsOperationRunning => _activeOperations > 0;

        public static void QueuePostOperation(Action a)
        {
            if (a == null)
                return;
            lock (_queueLock)
            {
                _postOperationQueue.Enqueue(a);
            }
        }

        private static void RaiseOperationStatus(string opType, bool running)
        {
            try
            {
                if (running)
                {
                    System.Threading.Interlocked.Increment(ref _activeOperations);
                }
                else
                {
                    System.Threading.Interlocked.Decrement(ref _activeOperations);
                }
            }
            catch { }

            try
            {
                var ev = OperationStatusChanged;
                if (ev != null)
                {
                    var list = ev.GetInvocationList();
                    foreach (var del in list)
                    {
                        try
                        {
                            var handler = (Action<string, bool>)del;
                            if (
                                del.Target is System.Windows.Forms.Control target
                                && target.IsHandleCreated
                                && target.InvokeRequired
                            )
                            {
                                try
                                {
                                    target.BeginInvoke(
                                        (Action)(
                                            () =>
                                            {
                                                try
                                                {
                                                    handler(opType, running);
                                                }
                                                catch { }
                                            }
                                        )
                                    );
                                }
                                catch { }
                            }
                            else
                            {
                                try
                                {
                                    handler(opType, running);
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }

                try
                {
                    DebugUtil.Log(
                        DebugLevel.INFO,
                        DebugFilter.DEPLOYMENT,
                        $"OperationStatus: {opType} running={running}"
                    );
                }
                catch { }
            }
            catch { }

            try
            {
                if (!IsOperationRunning)
                {
                    Queue<Action> toRun = null;
                    lock (_queueLock)
                    {
                        if (_postOperationQueue.Count > 0)
                        {
                            toRun = new Queue<Action>(_postOperationQueue);
                            _postOperationQueue.Clear();
                        }
                    }

                    if (toRun != null)
                    {
                        try
                        {
                            var forms = FormUtil.GetAllOpenForms();
                            if (forms != null && forms.Count > 0)
                            {
                                var dispatcher = forms[0];
                                dispatcher.BeginInvoke(
                                    new Action(() =>
                                    {
                                        while (toRun.Count > 0)
                                        {
                                            try
                                            {
                                                toRun.Dequeue()?.Invoke();
                                            }
                                            catch { }
                                        }
                                    })
                                );
                            }
                            else
                            {
                                while (toRun.Count > 0)
                                {
                                    try
                                    {
                                        toRun.Dequeue()?.Invoke();
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private static string DeploymentsRoot
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "QuillsModManagerV2",
                    "deployments"
                );
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        public class DeploymentProgress
        {
            public int Percent { get; set; }
            public string Message { get; set; }
        }

        public static async Task<DeploymentMetadata> DeployAsync(
            IEnumerable<ModEntry> mods,
            string gameDir,
            IProgress<DeploymentProgress> progress = null
        )
        {
            try
            {
                var curPathFile = Path.Combine(DeploymentsRoot, "current_deploy.json");
                if (File.Exists(curPathFile))
                {
                    try
                    {
                        await UndeployAsync(null);
                    }
                    catch { }
                }
            }
            catch { }

            RaiseOperationStatus("deploy", true);
            try
            {
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEPLOYMENT,
                    $"DeployAsync: starting deploy to {gameDir}"
                );
            }
            catch { }

            var pendingToasts = new List<Tuple<Color, string>>();
            var toastLock = new object();

            var result = await Task.Run(async () =>
            {
                if (mods == null)
                    throw new ArgumentNullException(nameof(mods));
                if (string.IsNullOrWhiteSpace(gameDir))
                    throw new ArgumentNullException(nameof(gameDir));
                var enabled = mods.Where(m => m.Enabled).ToList();
                if (enabled.Count == 0)
                    return (DeploymentMetadata)null;

                string id = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string root = Path.Combine(DeploymentsRoot, id);
                Directory.CreateDirectory(root);
                string backupRoot = Path.Combine(root, "backup");
                Directory.CreateDirectory(backupRoot);

                try
                {
                    var steamUserDataDir = Settings.Default.UserDataPath;
                    var steamUserId = Settings.Default.CurrentSteamUser;
                    var profileName = ProfileManager.GetCurrentProfileName(steamUserId);
                    if (
                        !string.IsNullOrWhiteSpace(steamUserDataDir)
                        && !string.IsNullOrWhiteSpace(steamUserId)
                        && !string.IsNullOrWhiteSpace(profileName)
                    )
                    {
                        var profileSave = SaveManager.GetProfileSavePath(steamUserId, profileName);
                        DebugUtil.Log(
                            DebugLevel.INFO,
                            DebugFilter.DEPLOYMENT,
                            $"DeployAsync: profileSave={profileSave}"
                        );
                        if (File.Exists(profileSave))
                        {
                            var backupPath = SaveManager.BackupGlobalSaveForDeployment(
                                steamUserDataDir,
                                steamUserId,
                                backupRoot
                            );
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.DEPLOYMENT,
                                $"DeployAsync: backupPath={backupPath}"
                            );
                            if (!string.IsNullOrWhiteSpace(backupPath))
                            {
                                try
                                {
                                    var steamSavePath = SteamUserManager.GetGameSavePath(
                                        steamUserDataDir,
                                        steamUserId
                                    );
                                    DebugUtil.Log(
                                        DebugLevel.INFO,
                                        DebugFilter.DEPLOYMENT,
                                        $"DeployAsync: steamSavePath={steamSavePath}"
                                    );
                                    bool needCopy = true;
                                    if (File.Exists(steamSavePath))
                                    {
                                        try
                                        {
                                            var a = File.ReadAllBytes(steamSavePath);
                                            var b = File.ReadAllBytes(profileSave);
                                            if (a.Length == b.Length && a.SequenceEqual(b))
                                                needCopy = false;
                                        }
                                        catch (Exception ex)
                                        {
                                            DebugUtil.Log(
                                                DebugLevel.ERROR,
                                                DebugFilter.DEPLOYMENT,
                                                $"DeployAsync: compare error: {ex.Message}"
                                            );
                                        }
                                    }

                                    if (needCopy)
                                    {
                                        var destDir = Path.GetDirectoryName(steamSavePath);
                                        if (!Directory.Exists(destDir))
                                            Directory.CreateDirectory(destDir);
                                        File.Copy(profileSave, steamSavePath, true);
                                        DebugUtil.Log(
                                            DebugLevel.INFO,
                                            DebugFilter.DEPLOYMENT,
                                            $"DeployAsync: copied profile save {profileSave} -> {steamSavePath}"
                                        );
                                    }
                                }
                                catch (Exception ex)
                                {
                                    DebugUtil.Log(
                                        DebugLevel.ERROR,
                                        DebugFilter.DEPLOYMENT,
                                        $"DeployAsync: error copying profile save: {ex.Message}"
                                    );
                                }
                            }
                        }
                    }
                }
                catch { }

                var fileOwner = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < enabled.Count; i++)
                {
                    var m = enabled[i];
                    try
                    {
                        DebugUtil.Log(
                            DebugLevel.INFO,
                            DebugFilter.DEPLOYMENT,
                            $"DeployAsync: scanning mod {m.Path}"
                        );
                    }
                    catch { }

                    var dataDir = Path.Combine(m.Path, "data");
                    if (Directory.Exists(dataDir))
                    {
                        var files = Directory.GetFiles(dataDir, "*", SearchOption.AllDirectories);
                        foreach (var f in files)
                        {
                            var relInner = MakeRelativePath(dataDir, f)
                                .Replace('\\', '/')
                                .TrimStart('/');
                            var rel = ("data/" + relInner).Replace('\\', '/');
                            var fn = Path.GetFileName(f);
                            if (
                                string.Equals(fn, "modicon.png", StringComparison.OrdinalIgnoreCase)
                            )
                                continue;
                            if (
                                string.Equals(fn, "modinfo.txt", StringComparison.OrdinalIgnoreCase)
                            )
                                continue;
                            fileOwner[rel] = i;
                        }
                    }

                    try
                    {
                        var rootFiles = Directory.GetFiles(
                            m.Path,
                            "*",
                            SearchOption.TopDirectoryOnly
                        );
                        foreach (var f in rootFiles)
                        {
                            var fn = Path.GetFileName(f);
                            if (
                                string.Equals(fn, "modicon.png", StringComparison.OrdinalIgnoreCase)
                            )
                                continue;
                            if (
                                string.Equals(fn, "modinfo.txt", StringComparison.OrdinalIgnoreCase)
                            )
                                continue;
                            var rel = MakeRelativePath(m.Path, f).Replace('\\', '/').TrimStart('/');
                            fileOwner[rel] = i;
                        }
                    }
                    catch { }
                }

                var metadata = new DeploymentMetadata { BackupFolder = backupRoot };

                try
                {
                    var steamUserDataDir = Settings.Default.UserDataPath;
                    var steamUserId = Settings.Default.CurrentSteamUser;
                    var profileName = ProfileManager.GetCurrentProfileName(steamUserId);
                    if (
                        !string.IsNullOrWhiteSpace(steamUserDataDir)
                        && !string.IsNullOrWhiteSpace(steamUserId)
                        && !string.IsNullOrWhiteSpace(profileName)
                    )
                    {
                        metadata.SteamUserId = steamUserId;
                        metadata.ProfileName = profileName;

                        var profileSave = SaveManager.GetProfileSavePath(steamUserId, profileName);
                        if (File.Exists(profileSave))
                        {
                            var backupFile = Path.Combine(
                                backupRoot,
                                $"cc_save_{steamUserId}.global"
                            );
                            if (File.Exists(backupFile))
                            {
                                metadata.SavedGlobalSaveBackup = backupFile;
                                metadata.ProfileSaveDeployed = true;
                                DebugUtil.Log(
                                    DebugLevel.INFO,
                                    DebugFilter.DEPLOYMENT,
                                    $"DeployAsync: recorded global save backup {backupFile} for user {steamUserId} profile {profileName}"
                                );
                            }
                        }
                    }
                }
                catch { }

                var tasks = new List<Task>();
                int total = fileOwner.Count;
                int processed = 0;
                foreach (var kv in fileOwner)
                {
                    var rel = kv.Key.Replace('\\', '/').TrimStart('/');
                    int ownerIdx = kv.Value;
                    var m = enabled[ownerIdx];

                    try
                    {
                        var parts = rel.Split(
                            new[] { '/', '\\' },
                            StringSplitOptions.RemoveEmptyEntries
                        );
                        var src = Path.Combine(new[] { m.Path }.Concat(parts).ToArray());
                        var dest = Path.Combine(new[] { gameDir }.Concat(parts).ToArray());

                        var destDir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(destDir))
                            Directory.CreateDirectory(destDir);

                        if (File.Exists(dest))
                        {
                            var backupPath = Path.Combine(
                                new[] { backupRoot }.Concat(parts).ToArray()
                            );
                            var backupDir = Path.GetDirectoryName(backupPath);
                            if (!Directory.Exists(backupDir))
                                Directory.CreateDirectory(backupDir);
                            if (!File.Exists(backupPath))
                            {
                                File.Copy(dest, backupPath);
                            }
                        }

                        if (File.Exists(src))
                        {
                            var t = CopyFileAsync(src, dest);
                            tasks.Add(t);
                            metadata.DeployedFiles.Add(rel);
                        }
                    }
                    catch { }

                    processed++;
                    progress?.Report(
                        new DeploymentProgress
                        {
                            Percent = (int)(processed * 100.0 / Math.Max(1, total)),
                            Message = $"Copying {processed}/{total}: {rel}"
                        }
                    );
                }

                await Task.WhenAll(tasks);

                try
                {
                    var metaPath = Path.Combine(root, "meta.json");
                    File.WriteAllText(
                        metaPath,
                        JsonConvert.SerializeObject(metadata, Formatting.Indented)
                    );
                    File.WriteAllText(
                        Path.Combine(DeploymentsRoot, "current_deploy.json"),
                        JsonConvert.SerializeObject(new { Path = root })
                    );
                }
                catch { }

                try
                {
                    DebugUtil.Log(
                        DebugLevel.INFO,
                        DebugFilter.DEPLOYMENT,
                        $"DeployAsync: completed. deployed {metadata.DeployedFiles.Count} files"
                    );
                    lock (toastLock)
                    {
                        pendingToasts.Add(
                            Tuple.Create(
                                Color.Lime,
                                $"Deployed {metadata.DeployedFiles.Count} files"
                            )
                        );
                    }
                }
                catch { }

                return metadata;
            });

            RaiseOperationStatus("deploy", false);
            try
            {
                foreach (var t in pendingToasts)
                {
                    try
                    {
                        ToastUtil.CreateToast(t.Item1, t.Item2);
                    }
                    catch { }
                }
            }
            catch { }
            return result;
        }

        public static async Task<DeploymentMetadata> DeployWithSafetyAsync(
            IEnumerable<ModEntry> mods,
            string gameDir,
            IProgress<DeploymentProgress> progress = null
        )
        {
            try
            {
                return await DeployAsync(mods, gameDir, progress);
            }
            catch (Exception ex)
            {
                try
                {
                    DebugUtil.Log(
                        DebugLevel.ERROR,
                        DebugFilter.DEPLOYMENT,
                        $"DeployWithSafetyAsync: deploy failed: {ex.Message}"
                    );
                }
                catch { }
                try
                {
                    ToastUtil.CreateToast(Color.Red, "Deployment failed — attempting rollback...");
                }
                catch { }

                try
                {
                    await UndeployAsync(null);
                    try
                    {
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "Rollback completed after failed deployment."
                        );
                    }
                    catch { }
                }
                catch (Exception ex2)
                {
                    try
                    {
                        DebugUtil.Log(
                            DebugLevel.ERROR,
                            DebugFilter.DEPLOYMENT,
                            $"DeployWithSafetyAsync: rollback failed: {ex2.Message}"
                        );
                    }
                    catch { }
                    try
                    {
                        ToastUtil.CreateToast(
                            Color.Red,
                            "Rollback failed after deployment error. Manual cleanup may be required."
                        );
                    }
                    catch { }
                }

                throw;
            }
        }

        public static async Task UndeployAsync(IProgress<DeploymentProgress> progress = null)
        {
            try
            {
                var curPathFileCheck = Path.Combine(DeploymentsRoot, "current_deploy.json");
                if (!File.Exists(curPathFileCheck))
                {
                    try
                    {
                        DebugUtil.Log(
                            DebugLevel.INFO,
                            DebugFilter.DEPLOYMENT,
                            "UndeployAsync: no current deployment to undeploy"
                        );
                    }
                    catch { }
                    ToastUtil.CreateToast("No current deployment to undeploy");
                    return;
                }
            }
            catch { }

            RaiseOperationStatus("undeploy", true);
            try
            {
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEPLOYMENT,
                    $"UndeployAsync: starting undeploy"
                );
            }
            catch { }

            var pendingToasts = new List<Tuple<Color, string>>();
            var toastLock = new object();

            await Task.Run(async () =>
            {
                try
                {
                    var curPathFile = Path.Combine(DeploymentsRoot, "current_deploy.json");
                    if (!File.Exists(curPathFile))
                        return;
                    var cur = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(curPathFile));
                    string root = (string)cur.Path;
                    if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
                        return;
                    string metaPath = Path.Combine(root, "meta.json");
                    if (!File.Exists(metaPath))
                        return;
                    var metadata = JsonConvert.DeserializeObject<DeploymentMetadata>(
                        File.ReadAllText(metaPath)
                    );
                    if (metadata == null)
                        return;
                    var tasks = new List<Task>();

                    string backupRoot = metadata.BackupFolder;

                    var relList = new List<string>();
                    if (metadata.DeployedFiles != null && metadata.DeployedFiles.Count > 0)
                    {
                        relList.AddRange(metadata.DeployedFiles);
                    }
                    else if (!string.IsNullOrWhiteSpace(backupRoot) && Directory.Exists(backupRoot))
                    {
                        var files = Directory.GetFiles(
                            backupRoot,
                            "*",
                            SearchOption.AllDirectories
                        );
                        foreach (var f in files)
                        {
                            var rel = MakeRelativePath(backupRoot, f)
                                .Replace('\\', '/')
                                .TrimStart('/');
                            relList.Add(rel);
                        }
                    }

                    int total = relList.Count;
                    int processed = 0;

                    foreach (var rel in relList)
                    {
                        try
                        {
                            var src = Path.Combine(
                                backupRoot,
                                rel.Replace('/', Path.DirectorySeparatorChar)
                            );
                            var dest = Path.Combine(
                                Settings.Default.GamePath,
                                rel.Replace('/', Path.DirectorySeparatorChar)
                            );

                            if (File.Exists(src))
                            {
                                var destDir = Path.GetDirectoryName(dest);
                                if (!Directory.Exists(destDir))
                                    Directory.CreateDirectory(destDir);
                                tasks.Add(CopyFileAsync(src, dest));
                            }
                            else
                            {
                                if (File.Exists(dest))
                                    try
                                    {
                                        File.Delete(dest);
                                    }
                                    catch { }
                            }
                        }
                        catch { }
                        processed++;
                        progress?.Report(
                            new DeploymentProgress
                            {
                                Percent = (int)(processed * 100.0 / Math.Max(1, total)),
                                Message = $"Restoring {processed}/{total}: {rel}"
                            }
                        );
                    }

                    await Task.WhenAll(tasks);

                    try
                    {
                        if (
                            metadata != null
                            && metadata.ProfileSaveDeployed
                            && !string.IsNullOrWhiteSpace(metadata.SteamUserId)
                            && !string.IsNullOrWhiteSpace(metadata.ProfileName)
                        )
                        {
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.DEPLOYMENT,
                                "OperationStatus: undeploy running=True profile-specific save=True"
                            );
                            try
                            {
                                string steamUserDataDir = Settings.Default.UserDataPath;
                                string steamSavePath = SteamUserManager.GetGameSavePath(
                                    steamUserDataDir,
                                    metadata.SteamUserId
                                );
                                DebugUtil.Log(
                                    DebugLevel.INFO,
                                    DebugFilter.DEPLOYMENT,
                                    $"UndeployAsync: currentSteamSave={steamSavePath}, profile={metadata.ProfileName}"
                                );

                                if (File.Exists(steamSavePath))
                                {
                                    string profileSave = SaveManager.GetProfileSavePath(
                                        metadata.SteamUserId,
                                        metadata.ProfileName
                                    );
                                    DebugUtil.Log(
                                        DebugLevel.INFO,
                                        DebugFilter.DEPLOYMENT,
                                        $"UndeployAsync: target profileSave={profileSave}"
                                    );

                                    bool needCopy = true;
                                    if (File.Exists(profileSave))
                                    {
                                        try
                                        {
                                            var a = File.ReadAllBytes(steamSavePath);
                                            var b = File.ReadAllBytes(profileSave);
                                            if (a.Length == b.Length && a.SequenceEqual(b))
                                            {
                                                needCopy = false;
                                                DebugUtil.Log(
                                                    DebugLevel.INFO,
                                                    DebugFilter.DEPLOYMENT,
                                                    $"UndeployAsync: profile save identical, no replace: {profileSave}"
                                                );
                                            }
                                            else
                                            {
                                                string bakDir =
                                                    SaveManager.GetProfileBackupDirectory(
                                                        metadata.SteamUserId,
                                                        metadata.ProfileName
                                                    );
                                                string timestamp = DateTime.Now.ToString(
                                                    "yyyy-MM-dd_HH-mm-ss"
                                                );
                                                string dest = Path.Combine(
                                                    bakDir,
                                                    $"cc_save_({timestamp}).dat"
                                                );
                                                DebugUtil.Log(
                                                    DebugLevel.INFO,
                                                    DebugFilter.DEPLOYMENT,
                                                    $"UndeployAsync: backup of profile-specific save for {metadata.ProfileName} {profileSave} to {dest}"
                                                );
                                                try
                                                {
                                                    File.Move(profileSave, dest);
                                                    DebugUtil.Log(
                                                        DebugLevel.INFO,
                                                        DebugFilter.DEPLOYMENT,
                                                        $"UndeployAsync: moved profile save to backup: {dest}"
                                                    );
                                                }
                                                catch (Exception ex)
                                                {
                                                    DebugUtil.Log(
                                                        DebugLevel.ERROR,
                                                        DebugFilter.DEPLOYMENT,
                                                        $"UndeployAsync: failed to move profile save: {ex.Message}"
                                                    );
                                                }

                                                try
                                                {
                                                    var backups = Directory
                                                        .GetFiles(bakDir)
                                                        .OrderBy(f => File.GetCreationTime(f))
                                                        .ToList();
                                                    while (backups.Count > 10)
                                                    {
                                                        var toDel = backups.First();
                                                        try
                                                        {
                                                            File.Delete(toDel);
                                                            DebugUtil.Log(
                                                                DebugLevel.INFO,
                                                                DebugFilter.DEPLOYMENT,
                                                                $"UndeployAsync: deleted old backup {toDel}"
                                                            );
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DebugUtil.Log(
                                                                DebugLevel.ERROR,
                                                                DebugFilter.DEPLOYMENT,
                                                                $"UndeployAsync: failed to delete old backup {toDel}: {ex.Message}"
                                                            );
                                                        }
                                                        backups.RemoveAt(0);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    DebugUtil.Log(
                                                        DebugLevel.ERROR,
                                                        DebugFilter.DEPLOYMENT,
                                                        $"UndeployAsync: error enforcing backup limit: {ex.Message}"
                                                    );
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            DebugUtil.Log(
                                                DebugLevel.ERROR,
                                                DebugFilter.DEPLOYMENT,
                                                $"UndeployAsync: error comparing profile save files: {ex.Message}"
                                            );
                                        }
                                    }

                                    if (needCopy)
                                    {
                                        try
                                        {
                                            var profileDir = Path.GetDirectoryName(profileSave);
                                            if (!Directory.Exists(profileDir))
                                                Directory.CreateDirectory(profileDir);
                                            File.Copy(steamSavePath, profileSave, true);
                                            DebugUtil.Log(
                                                DebugLevel.INFO,
                                                DebugFilter.DEPLOYMENT,
                                                $"UndeployAsync: saving profile-specific save from {steamSavePath} to {profileSave}"
                                            );
                                        }
                                        catch (Exception ex)
                                        {
                                            DebugUtil.Log(
                                                DebugLevel.ERROR,
                                                DebugFilter.DEPLOYMENT,
                                                $"UndeployAsync: failed to copy to profile save: {ex.Message}"
                                            );
                                            lock (toastLock)
                                            {
                                                pendingToasts.Add(
                                                    Tuple.Create(
                                                        Color.Red,
                                                        $"Failed to copy to profile save: {ex.Message}"
                                                    )
                                                );
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DebugUtil.Log(
                                        DebugLevel.WARNING,
                                        DebugFilter.DEPLOYMENT,
                                        $"UndeployAsync: current steam save not found: {steamSavePath}"
                                    );
                                    lock (toastLock)
                                    {
                                        pendingToasts.Add(
                                            Tuple.Create(
                                                Color.Red,
                                                $"Current steam save not found: {steamSavePath}"
                                            )
                                        );
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                DebugUtil.Log(
                                    DebugLevel.ERROR,
                                    DebugFilter.DEPLOYMENT,
                                    $"UndeployAsync: error saving active save to profile: {ex.Message}"
                                );
                                lock (toastLock)
                                {
                                    pendingToasts.Add(
                                        Tuple.Create(
                                            Color.Red,
                                            $"Error saving active save to profile: {ex.Message}"
                                        )
                                    );
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        if (
                            !string.IsNullOrWhiteSpace(metadata?.SavedGlobalSaveBackup)
                            && !string.IsNullOrWhiteSpace(metadata?.SteamUserId)
                        )
                        {
                            try
                            {
                                string steamUserDataDir = Settings.Default.UserDataPath;
                                string dest = SteamUserManager.GetGameSavePath(
                                    steamUserDataDir,
                                    metadata.SteamUserId
                                );
                                DebugUtil.Log(
                                    DebugLevel.INFO,
                                    DebugFilter.DEPLOYMENT,
                                    $"UndeployAsync: restoring global save from {metadata.SavedGlobalSaveBackup} to {dest}"
                                );
                                if (File.Exists(metadata.SavedGlobalSaveBackup))
                                {
                                    var destDir = Path.GetDirectoryName(dest);
                                    if (!Directory.Exists(destDir))
                                        Directory.CreateDirectory(destDir);
                                    File.Copy(metadata.SavedGlobalSaveBackup, dest, true);
                                    DebugUtil.Log(
                                        DebugLevel.INFO,
                                        DebugFilter.DEPLOYMENT,
                                        $"UndeployAsync: restored {metadata.SavedGlobalSaveBackup} -> {dest}"
                                    );
                                }
                            }
                            catch (Exception ex)
                            {
                                DebugUtil.Log(
                                    DebugLevel.ERROR,
                                    DebugFilter.DEPLOYMENT,
                                    $"UndeployAsync: error restoring global save: {ex.Message}"
                                );
                                lock (toastLock)
                                {
                                    pendingToasts.Add(
                                        Tuple.Create(
                                            Color.Red,
                                            $"Error restoring global save: {ex.Message}"
                                        )
                                    );
                                }
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        Directory.Delete(root, true);
                    }
                    catch { }
                    try
                    {
                        File.Delete(curPathFile);
                    }
                    catch { }

                    try
                    {
                        if (Directory.Exists(DeploymentsRoot))
                        {
                            foreach (var d in Directory.GetDirectories(DeploymentsRoot))
                            {
                                try
                                {
                                    Directory.Delete(d, true);
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }
                catch { }
            });

            RaiseOperationStatus("undeploy", false);

            try
            {
                foreach (var t in pendingToasts)
                    try
                    {
                        ToastUtil.CreateToast(t.Item1, t.Item2);
                    }
                    catch { }
            }
            catch { }
        }

        private static string MakeRelativePath(string baseDir, string fullPath)
        {
            if (!baseDir.EndsWith("\\"))
                baseDir += "\\";
            if (fullPath.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
                return fullPath.Substring(baseDir.Length);
            return fullPath;
        }

        private static async Task CopyFileAsync(string src, string dest)
        {
            const int BUF = 81920;
            try
            {
                using (
                    var s = new FileStream(
                        src,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read,
                        BUF,
                        useAsync: true
                    )
                )
                using (
                    var d = new FileStream(
                        dest,
                        FileMode.Create,
                        FileAccess.Write,
                        FileShare.None,
                        BUF,
                        useAsync: true
                    )
                )
                {
                    await s.CopyToAsync(d);
                }
            }
            catch { }
        }
    }
}
