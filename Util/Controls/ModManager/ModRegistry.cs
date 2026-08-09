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
        private class RegistryEntry
        {
            public string Folder { get; set; }
            public bool Enabled { get; set; }
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
                    QuillsModManagerV2.Util.Debug.DebugUtil.Log(
                        QuillsModManagerV2.Util.Debug.DebugLevel.INFO,
                        QuillsModManagerV2.Util.Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.Save: saving {mods.Count} mods to {RegistryPath}"
                    );
                }
                catch { }
                List<RegistryEntry> data = mods.Select(m => new RegistryEntry
                    {
                        Folder = Path.GetFileName(m.Path),
                        Enabled = m.Enabled
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
                    QuillsModManagerV2.Util.Debug.DebugUtil.Log(
                        QuillsModManagerV2.Util.Debug.DebugLevel.INFO,
                        QuillsModManagerV2.Util.Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.LoadOrder: loading from {RegistryPath}"
                    );
                }
                catch { }
                if (!File.Exists(RegistryPath))
                    return result;

                List<RegistryEntry> data = JsonConvert.DeserializeObject<List<RegistryEntry>>(
                    File.ReadAllText(RegistryPath)
                );

                if (data == null)
                    return result;

                foreach (RegistryEntry mod in data)
                {
                    string fullPath = Path.Combine(Settings.Default.ModPath, mod.Folder);

                    result.Add(fullPath);
                    enabled[fullPath] = mod.Enabled;
                }
                try
                {
                    QuillsModManagerV2.Util.Debug.DebugUtil.Log(
                        QuillsModManagerV2.Util.Debug.DebugLevel.INFO,
                        QuillsModManagerV2.Util.Debug.DebugFilter.DEPLOYMENT,
                        $"ModRegistry.LoadOrder: loaded {result.Count} entries"
                    );
                }
                catch { }
            }
            catch { }

            return result;
        }
    }
}
