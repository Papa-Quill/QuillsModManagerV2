using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace QuillsModManagerV2.Util.Controls
{
    public class ModEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _enabled = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value)
                    return;

                _enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public Image Icon { get; set; }
        public Dictionary<string, List<string>> Conflicts { get; set; } =
            new Dictionary<string, List<string>>();
        public bool HasConflict => Conflicts != null && Conflicts.Count > 0;
        public string ConflictTooltip
        {
            get
            {
                if (!HasConflict)
                    return string.Empty;
                var lines = new System.Text.StringBuilder();
                foreach (var kv in Conflicts)
                {
                    lines.AppendLine(kv.Key + ":");
                    foreach (var f in kv.Value)
                        lines.AppendLine("  " + f);
                }
                return lines.ToString();
            }
        }
        public string ConflictModNames
        {
            get
            {
                if (!HasConflict)
                    return string.Empty;
                return string.Join("\n", Conflicts.Keys);
            }
        }
    }
}
