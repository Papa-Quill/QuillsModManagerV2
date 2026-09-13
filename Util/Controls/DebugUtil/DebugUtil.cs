using System.Collections.Generic;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util.Debug
{
    internal class DebugUtil
    {
        private static FormDebugConsole _console;

        private static readonly HashSet<DebugLevel> _enabledLevels = new HashSet<DebugLevel>();
        private static readonly HashSet<DebugFilter> _enabledFilters = new HashSet<DebugFilter>();

        public static void SetConsole(FormDebugConsole console)
        {
            _console = console;
        }

        public static void EnableLevel(DebugLevel level)
        {
            _enabledLevels.Add(level);
        }

        public static void DisableLevel(DebugLevel level)
        {
            _enabledLevels.Remove(level);
        }

        public static void EnableFilter(DebugFilter filter)
        {
            _enabledFilters.Add(filter);
        }

        public static void DisableFilter(DebugFilter filter)
        {
            _enabledFilters.Remove(filter);
        }

        public static void Log(DebugLevel level, DebugFilter filter, string message)
        {
            try
            {
                if (!Settings.Default.DebugMode)
                    return;
            }
            catch { }

            _console?.AppendLog(level, filter, message);
        }

        public static void SaveLogs()
        {
            _console?.SaveLogsToFile();
        }
    }

    internal enum DebugLevel
    {
        INFO,
        WARNING,
        ERROR
    }

    internal enum DebugFilter
    {
        NONE,
        UI,
        SETTINGS,
        DEPLOYMENT,
        PROCESSHOOK,
        PROFILE,
        MODMANAGER,
        DEV
    }
}
