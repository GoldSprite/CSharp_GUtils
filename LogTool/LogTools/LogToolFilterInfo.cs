using System;
using System.Collections.Generic;

namespace GoldSprite.GUtils.LogTools {
    [Serializable]
    public class LogToolFilterInfo {
        //public string tag = "";
        public bool display = false;
        public UseInfo useInfo = UseInfo.UnUsed;

        [Serializable]
        public enum UseInfo
        {
            UnUsed, Used, Intercepted
        }
    }
}
