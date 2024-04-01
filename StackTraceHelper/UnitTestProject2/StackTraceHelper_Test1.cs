using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoldSprite.GUtils {
    [TestClass]
    public class StackTraceHelper_Test1 {
        [TestMethod]
        public void Test()
        {
            new InvokeClassA();
        }

        public class InvokeClassA {
            public InvokeClassA()
            {
                InvokeMethodA();
            }

            public void InvokeMethodA()
            {
                string classname = StackTraceHelper.GetStackAboveClassName(this);
                Assert.AreEqual(classname, typeof(StackTraceHelper_Test1).Name);
                Console.WriteLine("实例参数通过");

                classname = StackTraceHelper.GetStackAboveClassName(typeof(InvokeClassA));
                Assert.AreEqual(classname, typeof(StackTraceHelper_Test1).Name);
                Console.WriteLine("类型参数通过");
            }
        }
    }
}
