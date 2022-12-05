using DataAccess.BcMoore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Data.BcMoore;
using Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicTests;

[TestClass()]
public class BcMooreDataAccessTests
{

    private ISourceDataAccess SourceDataAcess { get; }

    public BcMooreDataAccessTests() 
    {
        SourceDataAcess = new BcMooreDataAccess();
    }
    
    
    [TestMethod()]
    public async Task GetTeamsTestAsync()
    {

        //ARRANGE
        int expectedTeamCount = 332;

        //ACT
        IEnumerable<Team> teams = await SourceDataAcess.GetTeams();

        //ASSSERT
        Assert.IsTrue(teams.Count() == expectedTeamCount);
    }

}
