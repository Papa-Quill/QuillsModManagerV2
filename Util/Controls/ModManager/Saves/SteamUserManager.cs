using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace QuillsModManagerV2.Util.Controls.ModManager
{
    public class SteamUserManager
    {
        private const string GAME_ID = "204360";

        public class SteamUser
        {
            public string UserDataId { get; set; }
            public string SteamID64 { get; set; }
            public string PersonaName { get; set; }
            public string AvatarPath { get; set; }

            public string UserDirectory
            {
                get
                {
                    var appDataDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "QuillsModManagerV2",
                        "users",
                        UserDataId
                    );
                    if (!Directory.Exists(appDataDir))
                        Directory.CreateDirectory(appDataDir);
                    return appDataDir;
                }
            }
        }

        public static List<SteamUser> GetSteamUsers(string steamUserDataDir)
        {
            var result = new List<SteamUser>();

            if (string.IsNullOrWhiteSpace(steamUserDataDir) || !Directory.Exists(steamUserDataDir))
                return result;

            try
            {
                foreach (var dir in Directory.GetDirectories(steamUserDataDir))
                {
                    var userDataId = Path.GetFileName(dir);

                    if (!HasGameSave(steamUserDataDir, userDataId))
                        continue;

                    var user = ParseSteamUser(steamUserDataDir, userDataId);
                    if (user != null)
                        result.Add(user);
                }
            }
            catch { }

            return result;
        }

        public static bool HasGameSave(string steamUserDataDir, string steamUserId)
        {
            if (
                string.IsNullOrWhiteSpace(steamUserDataDir)
                || string.IsNullOrWhiteSpace(steamUserId)
            )
                return false;

            string savePath = Path.Combine(
                steamUserDataDir,
                steamUserId,
                GAME_ID,
                "remote",
                "cc_save.dat"
            );

            return File.Exists(savePath);
        }

        public static string GetGameSavePath(string steamUserDataDir, string steamUserId)
        {
            return Path.Combine(steamUserDataDir, steamUserId, GAME_ID, "remote", "cc_save.dat");
        }

        private static SteamUser ParseSteamUser(string steamUserDataDir, string userDataId)
        {
            try
            {
                string userDirectory = Path.Combine(steamUserDataDir, userDataId);
                string localConfigPath = Path.Combine(userDirectory, "config", "localconfig.vdf");

                if (!File.Exists(localConfigPath))
                    return null;

                string fileText = File.ReadAllText(localConfigPath);

                string personaName = "Unknown";
                var nameMatch = Regex.Match(
                    fileText,
                    "\"PersonaName\"\\s*\"([^\"]+)\"",
                    RegexOptions.IgnoreCase
                );
                if (nameMatch.Success)
                    personaName = nameMatch.Groups[1].Value;

                string steamID = userDataId;
                var idMatch = Regex.Match(
                    fileText,
                    "GetEquippedProfileItemsForUser(\\d+)",
                    RegexOptions.IgnoreCase
                );
                if (idMatch.Success)
                {
                    steamID = idMatch.Groups[1].Value;
                }
                else
                {
                    var friendMatch = Regex.Match(
                        fileText,
                        "\"steamid\"\\s*\"(\\d+)\"",
                        RegexOptions.IgnoreCase
                    );
                    if (friendMatch.Success)
                        steamID = friendMatch.Groups[1].Value;
                }

                string steamRoot = Directory.GetParent(steamUserDataDir)?.FullName;
                string avatarPath = Path.Combine(
                    steamRoot ?? string.Empty,
                    "config",
                    "avatarcache",
                    steamID + ".png"
                );

                return new SteamUser
                {
                    UserDataId = userDataId,
                    SteamID64 = steamID,
                    PersonaName = personaName,
                    AvatarPath = File.Exists(avatarPath) ? avatarPath : null
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
