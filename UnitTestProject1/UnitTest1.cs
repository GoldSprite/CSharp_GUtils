using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldSprite.GUtils.LogTools;

namespace UnitTestProject2 {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1()
        {
            var log = "这是一条日志";
            LogTool.NLogMsg(log);
            Assert.AreEqual(1, 1);
        }
    }
}
