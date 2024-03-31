using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoldSprite.GUtils.LogTools.Tests {
    [TestClass]
    public class TestLogTool {
        [TestMethod]
        public void TestMethod1()
        {
            var log = "这是一条日志";
            LogTool.NLogMsg(log);
            Assert.AreEqual(1, 1);
        }
    }
}
