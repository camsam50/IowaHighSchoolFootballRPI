using Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Services.Tests
{
    [TestClass()]
    public class BcMooreServiceTests
    {

        ////EXAMPLE OF TEST WITH MULTIPLE INPUT
        //[TestMethod()]
        //[DataRow(1, 2, DisplayName = "Sequential numbers")]
        //[DataRow(2, 2, DisplayName = "Equal numbers")]
        //public void TestSomeNumbers(int x, int y)
        //{
        //    Assert.AreEqual(x, y);
        //}

        [TestMethod()]
        public async System.Threading.Tasks.Task GetTeamsTestAsync()
        {
            //ARRANGE
            var x = new BcMooreService();
            
            //ACT
            var teams = await x.GetTeams();

            //ASSSERT
            Assert.IsTrue(teams.Any()); //Was able to retrieve teams
            

        }

        [TestMethod()]
        public async System.Threading.Tasks.Task GetScoresTestAsync()
        {

            //ARRANGE
            var x = new BcMooreService();

            //ACT
            var scores = await x.GetScores();

            //ASSSERT
            Assert.IsTrue(scores.Any()); //Was able to retrieve scores
        }
    }
}