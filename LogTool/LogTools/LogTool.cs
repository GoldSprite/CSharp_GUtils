using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoldSprite.GUtils.LogTools {
    public class LogTool {
        public static bool IsInit = false;
        public static string filePath = string.Empty;
        public static LogToolData data;


        private static void Init()
        {
            var dataPath = System.IO.Directory.GetCurrentDirectory();
            filePath = Path.Combine(dataPath, "LogToolData.json");
            try {
                if (!File.Exists(filePath)) throw new Exception();
                ReadData(filePath);
                data.realtimeFilterList.Clear();
                IsInit = true;
                LogTool.NLogDebug("default", "已读取配置数据文件.");
            }
            catch (Exception) {
                SaveData(data = new LogToolData());
                IsInit = true;
                LogTool.NLogDebug("default", "初始数据文件以创建.");
            }
        }

        public static void ReadData(string filePath)
        {
            var readContent = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<LogToolData>(readContent);
        }
        public static void SaveData(LogToolData data)
        {
            var writeContent = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, writeContent);
        }


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
            if (!IsInit) Init();
            if (logLevel != ILogLevel.FORCE)
                if (!data.logLevels.ContainsKey(logLevel) || !data.logLevels[logLevel]) return;

            if (string.IsNullOrEmpty(tag))
                tag = StackTraceHelper.GetStackAboveClassName(typeof(LogTool));

            //过滤Log
            if (!DisplayLog(logLevel, tag)) return;

            var log = ""
                    + DateTime.Now.ToString("[yy/MM/dd-HH:mm:ss:fff]")
                    + ILogLevel.GetLogMsg(logLevel)
                    + "[" + tag + "]"
                    + " "  //空位
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
                data.realtimeFilterList[tag].display = result;
                data.realtimeFilterList[tag].useInfo = result ? LogToolFilterInfo.UseInfo.Used : LogToolFilterInfo.UseInfo.Intercepted;
                SaveData(data);
            }
            return result;
        }
    }
}