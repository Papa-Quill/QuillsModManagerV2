using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util.Controls
{
    public static class ModRegistry
    {
        public class LayoutEntry
        {
            public string Folder { get; set; }
            public bool Enabled { get; set; }
            public bool IsSeparator { get; set; }
            public string SeparatorName { get; set; }
            public int SeparatorColorArgb { get; set; }
        }

        private static string RegistryPath
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "QuillsModManagerV2"
                );

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                return Path.Combine(dir, "mods.json");
            }
        }

        public static void Save(BindingList<ModEntry> mods)
        {
            try
            {
                try
                {
                    Debug.DebugUtil.Log(
                        Debug.DebugLevel.INFO,
                        Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.Save: saving {mods.Count} mods to {RegistryPath}"
                    );
                }
                catch { }
                List<LayoutEntry> data = mods.Select(m => new LayoutEntry
                    {
                        Folder = m.IsSeparator ? null : Path.GetFileName(m.Path),
                        Enabled = m.Enabled,
                        IsSeparator = m.IsSeparator,
                        SeparatorName = m.SeparatorName,
                        SeparatorColorArgb = m.SeparatorColorArgb
                    })
                    .ToList();

                File.WriteAllText(
                    RegistryPath,
                    JsonConvert.SerializeObject(data, Formatting.Indented)
                );
            }
            catch { }
        }

        public static List<string> LoadOrder(out Dictionary<string, bool> enabled)
        {
            enabled = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            List<string> result = new List<string>();

            try
            {
                try
                {
                    Debug.DebugUtil.Log(
                        Debug.DebugLevel.INFO,
                        Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.LoadOrder: loading from {RegistryPath}"
                    );
                }
                catch { }
                if (!File.Exists(RegistryPath))
                    return result;

                List<LayoutEntry> data = JsonConvert.DeserializeObject<List<LayoutEntry>>(
                    File.ReadAllText(RegistryPath)
                );

                if (data == null)
                    return result;

                foreach (LayoutEntry mod in data)
                {
                    if (mod.IsSeparator || string.IsNullOrWhiteSpace(mod.Folder))
                        continue;
                    string fullPath = Path.Combine(Settings.Default.ModPath, mod.Folder);

                    result.Add(fullPath);
                    enabled[fullPath] = mod.Enabled;
                }
                try
                {
                    Debug.DebugUtil.Log(
                        Debug.DebugLevel.INFO,
                        Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.LoadOrder: loaded {result.Count} entries"
                    );
                }
                catch { }
            }
            catch { }

            return result;
        }

        public static List<LayoutEntry> LoadLayout(out Dictionary<string, bool> enabled)
        {
            enabled = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            try
            {
                if (!File.Exists(RegistryPath))
                    return new List<LayoutEntry>();

                var data = JsonConvert.DeserializeObject<List<LayoutEntry>>(
                    File.ReadAllText(RegistryPath)
                ) ?? new List<LayoutEntry>();
                foreach (var item in data)
                {
                    if (!item.IsSeparator && !string.IsNullOrWhiteSpace(item.Folder))
                    {
                        string fullPath = Path.Combine(Settings.Default.ModPath, item.Folder);
                        enabled[fullPath] = item.Enabled;
                    }
                }
                return data;
            }
            catch
            {
                return new List<LayoutEntry>();
            }
        }
    }
}
