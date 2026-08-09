using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls
{
    public static class ProfileManager
    {
        public static string UsersDirectory
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "QuillsModManagerV2",
                    "users"
                );
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        private static string GetUserProfilesDir(string steamUserId)
        {
            string dir = Path.Combine(UsersDirectory, steamUserId);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        public static void SaveProfile(
            string steamUserId,
            string profileName,
            BindingList<ModEntry> mods
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                var userDir = GetUserProfilesDir(steamUserId);
                var profileDir = Path.Combine(userDir, profileName);
                if (!Directory.Exists(profileDir))
                    Directory.CreateDirectory(profileDir);

                var data = mods.Where(m => m.Enabled)
                    .Select(m => Path.GetFileName(m.Path))
                    .ToList();
                string path = Path.Combine(profileDir, "profile.json");
                File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"SaveProfile: {steamUserId}/{profileName}"
                );
            }
            catch { }
        }

        public static void LoadProfile(
            string steamUserId,
            string profileName,
            BindingList<ModEntry> mods
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return;

                var userDir = GetUserProfilesDir(steamUserId);
                var profileDir = Path.Combine(userDir, profileName);
                string path = Path.Combine(profileDir, "profile.json");

                if (!File.Exists(path))
                    return;

                var data = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
                if (data == null)
                    return;

                var ordered = new List<ModEntry>();
                foreach (var folder in data)
                {
                    var found = mods.FirstOrDefault(x =>
                        string.Equals(
                            Path.GetFileName(x.Path),
                            folder,
                            StringComparison.OrdinalIgnoreCase
                        )
                    );
                    if (found != null)
                        ordered.Add(found);
                }
                foreach (var m in mods.ToList())
                    if (!ordered.Contains(m))
                        ordered.Add(m);

                mods.RaiseListChangedEvents = false;
                mods.Clear();
                foreach (var m in ordered)
                    mods.Add(m);
                mods.RaiseListChangedEvents = true;
                mods.ResetBindings();

                var enabledSet = new HashSet<string>(data, StringComparer.OrdinalIgnoreCase);
                foreach (var m in mods)
                    m.Enabled = false;
                foreach (var m in mods)
                {
                    var folder = Path.GetFileName(m.Path);
                    if (enabledSet.Contains(folder))
                        m.Enabled = true;
                }
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"LoadProfile: {steamUserId}/{profileName}"
                );
            }
            catch { }
        }

        public static bool DeleteProfile(string steamUserId, string profileName)
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return false;

                var profiles = ListProfiles(steamUserId);
                if (profiles.Count <= 1)
                {
                    DebugUtil.Log(
                        DebugLevel.WARNING,
                        DebugFilter.PROFILE,
                        $"DeleteProfile: Cannot delete last profile {profileName}"
                    );
                    return false;
                }

                var userDir = GetUserProfilesDir(steamUserId);
                var profileDir = Path.Combine(userDir, profileName);
                if (Directory.Exists(profileDir))
                    Directory.Delete(profileDir, true);

                var currentProfile = GetCurrentProfileName(steamUserId);
                if (string.Equals(currentProfile, profileName, StringComparison.OrdinalIgnoreCase))
                {
                    var remaining = ListProfiles(steamUserId);
                    var newCurrent = remaining.FirstOrDefault();
                    SetCurrentProfileName(steamUserId, newCurrent);
                }

                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"DeleteProfile: {steamUserId}/{profileName}"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> GetEnabledFolders(string steamUserId, string profileName)
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return new List<string>();

                var userDir = GetUserProfilesDir(steamUserId);
                var profileDir = Path.Combine(userDir, profileName);
                string path = Path.Combine(profileDir, "profile.json");

                if (!File.Exists(path))
                    return new List<string>();

                var data = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
                return data ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static bool DuplicateProfile(
            string steamUserId,
            string profileName,
            string newProfileName
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(profileName)
                    || string.IsNullOrWhiteSpace(newProfileName)
                )
                    return false;

                var userDir = GetUserProfilesDir(steamUserId);
                var srcDir = Path.Combine(userDir, profileName);
                var dstDir = Path.Combine(userDir, newProfileName);

                if (!Directory.Exists(srcDir) || Directory.Exists(dstDir))
                    return false;

                CopyDirectory(srcDir, dstDir);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"DuplicateProfile: {steamUserId}/{profileName} -> {newProfileName}"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RenameProfile(
            string steamUserId,
            string profileName,
            string newProfileName
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(profileName)
                    || string.IsNullOrWhiteSpace(newProfileName)
                )
                    return false;

                if (!IsValidProfileName(profileName) || !IsValidProfileName(newProfileName))
                    return false;

                var userDir = GetUserProfilesDir(steamUserId);
                var srcDir = Path.Combine(userDir, profileName);
                var dstDir = Path.Combine(userDir, newProfileName);

                if (!Directory.Exists(srcDir) || Directory.Exists(dstDir))
                    return false;

                Directory.Move(srcDir, dstDir);

                var currentProfile = GetCurrentProfileName(steamUserId);
                if (string.Equals(currentProfile, profileName, StringComparison.OrdinalIgnoreCase))
                    SetCurrentProfileName(steamUserId, newProfileName);

                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    "RenameProfile: " + steamUserId + "/" + profileName + " -> " + newProfileName
                );

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidProfileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return false;

            if (name.IndexOf('\\') >= 0 || name.IndexOf('/') >= 0)
                return false;

            if (name.EndsWith(" ") || name.EndsWith("."))
                return false;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(name);

            switch (nameWithoutExtension.ToUpperInvariant())
            {
                case "CON":
                case "PRN":
                case "AUX":
                case "NUL":
                case "COM1":
                case "COM2":
                case "COM3":
                case "COM4":
                case "COM5":
                case "COM6":
                case "COM7":
                case "COM8":
                case "COM9":
                case "LPT1":
                case "LPT2":
                case "LPT3":
                case "LPT4":
                case "LPT5":
                case "LPT6":
                case "LPT7":
                case "LPT8":
                case "LPT9":
                    return false;
            }

            return true;
        }

        public static string GetCurrentProfileName(string steamUserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(steamUserId))
                    return null;

                var userDir = GetUserProfilesDir(steamUserId);
                string file = Path.Combine(userDir, "current_profile.txt");

                if (!File.Exists(file))
                    return null;

                var txt = File.ReadAllText(file).Trim();
                return string.IsNullOrWhiteSpace(txt) ? null : txt;
            }
            catch
            {
                return null;
            }
        }

        public static void SetCurrentProfileName(string steamUserId, string profileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(steamUserId))
                    return;

                var userDir = GetUserProfilesDir(steamUserId);
                string file = Path.Combine(userDir, "current_profile.txt");

                if (string.IsNullOrWhiteSpace(profileName))
                {
                    if (File.Exists(file))
                        File.Delete(file);
                    return;
                }

                File.WriteAllText(file, profileName);
            }
            catch { }
        }

        public static List<string> ListProfiles(string steamUserId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(steamUserId))
                    return new List<string>();

                var userDir = GetUserProfilesDir(steamUserId);

                if (!Directory.Exists(userDir))
                    return new List<string>();

                var profileDirs = Directory.GetDirectories(userDir);
                var list = profileDirs
                    .Select(d => Path.GetFileName(d))
                    .Where(name => File.Exists(Path.Combine(userDir, name, "profile.json")))
                    .OrderBy(x => x)
                    .ToList();

                if (list.Count == 0)
                {
                    string defaultDir = Path.Combine(userDir, "Default");
                    if (!Directory.Exists(defaultDir))
                        Directory.CreateDirectory(defaultDir);
                    string profileJson = Path.Combine(defaultDir, "profile.json");
                    if (!File.Exists(profileJson))
                        File.WriteAllText(
                            profileJson,
                            JsonConvert.SerializeObject(new List<string>())
                        );
                    list.Add("Default");
                }

                return list;
            }
            catch
            {
                return new List<string>();
            }
        }

        private static void CopyDirectory(string source, string destination)
        {
            var dir = new DirectoryInfo(source);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {source}");

            Directory.CreateDirectory(destination);

            foreach (var file in dir.GetFiles())
                file.CopyTo(Path.Combine(destination, file.Name), true);

            foreach (var subdir in dir.GetDirectories())
                CopyDirectory(subdir.FullName, Path.Combine(destination, subdir.Name));
        }
    }
}
