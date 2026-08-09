using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2.Util.Controls.ModManager
{
    public class SaveManager
    {
        public static bool HasProfileSave(string steamUserId, string profileName)
        {
            try
            {
                var path = GetProfileSavePath(steamUserId, profileName);
                return File.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        public static bool BackupCurrentSave(
            string steamUserDataDir,
            string steamUserId,
            string profileName
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserDataDir)
                    || string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(profileName)
                )
                    return false;

                string savePath = SteamUserManager.GetGameSavePath(steamUserDataDir, steamUserId);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"BackupCurrentSave: steamSavePath={savePath}"
                );
                if (!File.Exists(savePath))
                    return false;

                string profileBackupDir = GetProfileBackupDirectory(steamUserId, profileName);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"BackupCurrentSave: profileBackupDir={profileBackupDir}"
                );
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string backupPath = Path.Combine(profileBackupDir, $"{timestamp} cc_save.dat");

                File.Copy(savePath, backupPath, true);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"BackupCurrentSave: copied {savePath} -> {backupPath}"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteProfileSave(string steamUserId, string profileName)
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserId) || string.IsNullOrWhiteSpace(profileName)
                )
                    return false;

                string profileSavePath = GetProfileSavePath(steamUserId, profileName);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"DeleteProfileSave: profileSavePath={profileSavePath}"
                );
                if (File.Exists(profileSavePath))
                {
                    File.Delete(profileSavePath);
                    DebugUtil.Log(
                        DebugLevel.INFO,
                        DebugFilter.PROFILE,
                        $"DeleteProfileSave: deleted {profileSavePath}"
                    );
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string BackupGlobalSaveForDeployment(
            string steamUserDataDir,
            string steamUserId,
            string backupRoot
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserDataDir)
                    || string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(backupRoot)
                )
                    return null;

                string steamSavePath = SteamUserManager.GetGameSavePath(
                    steamUserDataDir,
                    steamUserId
                );
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEPLOYMENT,
                    $"BackupGlobalSaveForDeployment: steamSavePath={steamSavePath}, backupRoot={backupRoot}"
                );
                if (!File.Exists(steamSavePath))
                {
                    DebugUtil.Log(
                        DebugLevel.WARNING,
                        DebugFilter.DEPLOYMENT,
                        $"BackupGlobalSaveForDeployment: global save not found: {steamSavePath}"
                    );
                    return null;
                }

                if (!Directory.Exists(backupRoot))
                    Directory.CreateDirectory(backupRoot);
                string dest = Path.Combine(backupRoot, $"cc_save_{steamUserId}.global");

                if (File.Exists(dest))
                {
                    try
                    {
                        var a = File.ReadAllBytes(steamSavePath);
                        var b = File.ReadAllBytes(dest);
                        if (a.Length == b.Length && a.SequenceEqual(b))
                        {
                            DebugUtil.Log(
                                DebugLevel.INFO,
                                DebugFilter.DEPLOYMENT,
                                $"BackupGlobalSaveForDeployment: existing identical backup kept: {dest}"
                            );
                            return dest;
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugUtil.Log(
                            DebugLevel.ERROR,
                            DebugFilter.DEPLOYMENT,
                            $"BackupGlobalSaveForDeployment: error comparing files: {ex.Message}"
                        );
                    }
                }

                File.Copy(steamSavePath, dest, true);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEPLOYMENT,
                    $"BackupGlobalSaveForDeployment: copied {steamSavePath} -> {dest}"
                );
                return dest;
            }
            catch
            {
                return null;
            }
        }

        public static string GetProfileSavePath(string steamUserId, string profileName)
        {
            var userDir = ProfileManager.UsersDirectory;
            var profileDir = Path.Combine(userDir, steamUserId, profileName);
            return Path.Combine(profileDir, "cc_save.dat");
        }

        public static string GetProfileBackupDirectory(string steamUserId, string profileName)
        {
            var userDir = ProfileManager.UsersDirectory;
            var backupDir = Path.Combine(userDir, steamUserId, profileName, "backups");
            if (!Directory.Exists(backupDir))
                Directory.CreateDirectory(backupDir);
            return backupDir;
        }

        public static bool RestoreProfileSave(
            string steamUserDataDir,
            string steamUserId,
            string profileName
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserDataDir)
                    || string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(profileName)
                )
                    return false;

                string profileSavePath = GetProfileSavePath(steamUserId, profileName);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"RestoreProfileSave: profileSavePath={profileSavePath}"
                );
                if (!File.Exists(profileSavePath))
                    return false;

                string steamSavePath = SteamUserManager.GetGameSavePath(
                    steamUserDataDir,
                    steamUserId
                );
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"RestoreProfileSave: steamSavePath={steamSavePath}"
                );
                File.Copy(profileSavePath, steamSavePath, true);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"RestoreProfileSave: copied {profileSavePath} -> {steamSavePath}"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CaptureCurrentSaveToProfile(
            string steamUserDataDir,
            string steamUserId,
            string profileName
        )
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(steamUserDataDir)
                    || string.IsNullOrWhiteSpace(steamUserId)
                    || string.IsNullOrWhiteSpace(profileName)
                )
                    return false;

                string savePath = SteamUserManager.GetGameSavePath(steamUserDataDir, steamUserId);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"CaptureCurrentSaveToProfile: savePath={savePath}"
                );
                if (!File.Exists(savePath))
                    return false;

                string profileSavePath = GetProfileSavePath(steamUserId, profileName);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"CaptureCurrentSaveToProfile: profileSavePath={profileSavePath}"
                );
                var profileDir = Path.GetDirectoryName(profileSavePath);
                if (!Directory.Exists(profileDir))
                    Directory.CreateDirectory(profileDir);

                File.Copy(savePath, profileSavePath, true);
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.PROFILE,
                    $"CaptureCurrentSaveToProfile: copied {savePath} -> {profileSavePath}"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> GetProfileBackups(string steamUserId, string profileName)
        {
            try
            {
                string backupDir = GetProfileBackupDirectory(steamUserId, profileName);
                if (!Directory.Exists(backupDir))
                    return new List<string>();

                var files = Directory
                    .GetFiles(backupDir, "*.dat")
                    .OrderByDescending(f => File.GetLastWriteTime(f))
                    .ToList();
                return files;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
