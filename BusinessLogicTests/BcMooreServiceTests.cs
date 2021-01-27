using Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Services.Tests
{
    [TestClass()]
    public class BcMooreServiceTests
    {

        [TestMethod()]
        [DataRow(1, 2, DisplayName = "Sequential numbers")]
        [DataRow(2, 2, DisplayName = "Equal numbers")]
        public void TestSomeNumbers(int x, int y)
        {
            Assert.AreEqual(x, y);
        }

        [TestMethod()]
        public void DoWorkTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DoSomeOtherWorkTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetTruthsTest()
        {
            throw new NotImplementedException();
        }
    }
}