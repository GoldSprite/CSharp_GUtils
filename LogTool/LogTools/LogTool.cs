using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace GoldSprite.GUtils.LogTools {
    public class LogTool {

        public static Dictionary<int, bool> logLevels = new Dictionary<int, bool>()
        {
            {ILogLevel.ERROR, true},
            {ILogLevel.WARNING, true},
            {ILogLevel.DEBUG, true},
            {ILogLevel.INFO, true},
            {ILogLevel.MSG, true},
        };

        public static LogToolData data = new LogToolData();


        public static void NLogMsg(object msg) => NLogMsg("", msg);
        public static void NLogMsg(string tag, object msg)
        {
            UseConsoleColor(ConsoleColor.Gray);
            NLog(ILogLevel.MSG, tag, msg);
        }


        public static void NLogInfo(object msg) => NLogInfo("", msg);
        public static void NLogInfo(string tag, object msg)
        {
            UseConsoleColor(ConsoleColor.White);
            NLog(ILogLevel.INFO, tag, msg);
        }


        public static void NLogDebug(object msg) => NLogDebug("", msg);
        public static void NLogDebug(string tag, object msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            NLog(ILogLevel.DEBUG, tag, msg);
        }


        public static void NLogWarn(object msg) => NLogWarn("", msg);
        public static void NLogWarn(string tag, object msg)
        {
            UseConsoleColor(ConsoleColor.Yellow);
            NLog(ILogLevel.WARNING, tag, msg);
        }


        public static void NLogErr(object msg) => NLogErr("", msg);
        public static void NLogErr(string tag, object msg)
        {
            UseConsoleColor(ConsoleColor.Red);
            NLog(ILogLevel.ERROR, tag, msg);
        }


        public static void NLog(int logLevel, string tag, object msg)
        {
            if (logLevel != ILogLevel.FORCE)
                if (!logLevels.ContainsKey(logLevel) || !logLevels[logLevel]) return;

            //过滤Log
            if (!DisplayLog(logLevel, tag)) return;

            var log = ""
                    + DateTime.Now.ToString("[yy/MM/dd-HH:mm:ss:fff]")
                    + ILogLevel.GetLogMsg(logLevel)
                    + "[" + tag + "]"
                    + "    "  //空位
                    + msg
                    ;
            Console.WriteLine(log);
            ResetConsoleColor();
        }

        public static void UseConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public static void ResetConsoleColor()
        {
            Console.ResetColor();
        }


        private static bool DisplayLog(int logLevel, string tag)
        {
            var exist = data.filterList.TryGetValue(tag, out LogToolFilterInfo tagInfo);
            var realExist = data.realtimeFilterList.TryGetValue(tag, out LogToolFilterInfo realTimeTagInfo);
            //低于拦截等级编号(0为Err5为Msg), 或在白名单列表, 或默认显示时 --- 显示该log
            var result = (logLevel < data.interceptLevel) || (exist && tagInfo.display) || (!exist && data.defaultDispaly);

            if (!realExist) {
                data.realtimeFilterList[tag] = exist ? tagInfo : new LogToolFilterInfo();
                data.realtimeFilterList[tag].useInfo = result ? LogToolFilterInfo.UseInfo.Used : LogToolFilterInfo.UseInfo.Intercepted;
            }
            return result;
        }
    }
}