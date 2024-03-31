using System.Collections.Generic;

namespace GoldSprite.GUtils.LogTools {
    public class LogToolData {
        public int interceptLevel = ILogLevel.WARNING;
        public bool defaultDispaly = true;
        public Dictionary<string, LogToolFilterInfo> filterList = new Dictionary<string, LogToolFilterInfo>();
        public Dictionary<string, LogToolFilterInfo> realtimeFilterList = new Dictionary<string, LogToolFilterInfo>();
    }
}
