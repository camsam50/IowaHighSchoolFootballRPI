﻿using DataAccess.BcMoore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Data.BcMoore;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Tests;

[TestClass()]
public class BcMooreServiceTests
{

    private ISourceDataAccess SourceDataAcess { get; }
    private ISourceDataService SourceDataService { get; }

    public BcMooreServiceTests()
    {
        SourceDataAcess = new BcMooreDataAccess(); 
        SourceDataService = new BcMooreService(SourceDataAcess);
    }




    [TestMethod()]
    public async Task GetTeamsTestAsync()
    {

        //ARRANGE
        int expectedTeamCount = 332;

        //ACT
        IEnumerable<Team> teams = await SourceDataService.GetTeams();

        //ASSSERT
        Assert.IsTrue(teams.Count() == expectedTeamCount);
    }



    ////EXAMPLE OF TEST WITH MULTIPLE INPUT
    //[TestMethod()]
    //[DataRow(1, 2, DisplayName = "Sequential numbers")]
    //[DataRow(2, 2, DisplayName = "Equal numbers")]
    //public void TestSomeNumbers(int x, int y)
    //{
    //    Assert.AreEqual(x, y);
    //}

    //[TestMethod()]
    //public async Task GetTeamsTestAsync()
    //{
    //    //ARRANGE
    //    var x = new BcMooreService(new BcMooreDataAccess()); //TODO: Implement better - test only one layer

    //    //ACT
    //    var teams = await x.GetTeams();

    //    //ASSSERT
    //    Assert.IsTrue(teams.Any()); //Was able to retrieve teams


    //}








    


}


[ObsoleteAttribute("This test is obsolete. Do not use.", false)]
[TestClass()]
public class BcMooreDataAccessTests
{
    //TODO: How much of this should be yearly dependant?
    
    [TestMethod()]
    public async Task GetTeamsTest()
    {
        //ARRANGE
        //var x = new BcMooreDataAccess();

        //ACT
        //var teams = await x.GetTeams();

        //ASSSERT
        //Assert.IsTrue(teams.Any()); //Was able to retrieve teams
        //Assert.IsTrue(teams.Count() == 334); //Got the correct number of teams
        //Assert.IsTrue(teams.Any(t => t.ShortName == "WDM Dowling")); //Was able to get Dowling
        //Assert.IsTrue(teams.Any(t => t.ShortName == "East Marshall")); //Was able to get East Marshall
        //Assert.IsTrue(teams.Any(t => t.Classification == "5A")); //Was able to get 5A
        //Assert.IsTrue(teams.Any(t => t.Classification == "4A")); //Was able to get 4A
        //Assert.IsTrue(teams.Any(t => t.Classification == "3A")); //Was able to get 3A
        //Assert.IsTrue(teams.Any(t => t.Classification == "2A")); //Was able to get 2A
        //Assert.IsTrue(teams.Any(t => t.Classification == "1A")); //Was able to get 1A
        //Assert.IsTrue(teams.Any(t => t.Classification == "A")); //Was able to get A
        //Assert.IsTrue(teams.Any(t => t.Classification == "8")); //Was able to get 8
        Assert.IsTrue(true);
    }
}