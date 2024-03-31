using System;
using System.Collections.Generic;

namespace GoldSprite.GUtils.LogTools {
    public abstract class ILogLevel {
        public const int FORCE = -2;
        public const int ERROR = 1;
        public const int WARNING = 2;
        public const int DEBUG = 3;
        public const int INFO = 4;
        public const int MSG = 5;

        private ILogLevel() { }

        private static Dictionary<int, string> msgMap = new Dictionary<int, string>()
        {
            {MSG,     " [#MSG#] "},
            {INFO,    "   [#INFO] "},
            {DEBUG,   " [DEBUG] "},
            {WARNING, "[#WARN] "},
            {ERROR,   "  [#ERR#] "},
            {FORCE,   "[FORCE] "},
        };

        public static string GetLogMsg(int logLevel)
        {
            if (!msgMap.ContainsKey(logLevel)) return "[UNKNOWN] ";
            return msgMap[logLevel];
        }
    }
}
