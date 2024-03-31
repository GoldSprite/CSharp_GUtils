using GoldSprite.GUtils.LogTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1 {
    public class Program
    {
        public static void Main(string[] args)
        {
            new UnitTest1().TestMethod1();
            Console.ReadKey();
        }
    }
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1()
        {
            var log = "这是一条日志";
            LogTool.NLogMsg("default", log);
            LogTool.NLogMsg(log);
            LogTool.NLogInfo(log);
            LogTool.NLogDebug(log);
            LogTool.NLogWarn(log);
            LogTool.NLogErr(log);
            Assert.AreEqual(1, 1);
        }
    }
}
