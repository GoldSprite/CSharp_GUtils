using GoldSprite.GUtils.LogTools;
using System.Collections.Generic;

namespace GoldSprite.GUtils.LogTools {
    public class LogToolData {
        public Dictionary<int, bool> logLevels = new Dictionary<int, bool>()
        {
            {ILogLevel.ERROR, true},
            {ILogLevel.WARNING, true},
            { ILogLevel.DEBUG, true},
            { ILogLevel.INFO, true},
            { ILogLevel.MSG, true},
        };
        public int interceptLevel = ILogLevel.WARNING;
        public bool defaultDispaly = false;
        public Dictionary<string, LogToolFilterInfo> filterList = new Dictionary<string, LogToolFilterInfo>() { { "default", new LogToolFilterInfo() { display = true } } };
        public Dictionary<string, LogToolFilterInfo> realtimeFilterList = new Dictionary<string, LogToolFilterInfo>();
        //public Dictionary<int, bool> logLevels;
        //public int interceptLevel;
        //public bool defaultDispaly;
        //public Dictionary<string, LogToolFilterInfo> filterList;
        //public Dictionary<string, LogToolFilterInfo> realtimeFilterList;
    }
}
