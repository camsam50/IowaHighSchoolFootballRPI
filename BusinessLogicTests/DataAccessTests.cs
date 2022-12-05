using DataAccess.BcMoore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicTests;

[TestClass()]
public class DataAccessTests
{

    private ISourceDataAccess SourceDataAcess { get; }

    public DataAccessTests() 
    {
        //do something
        SourceDataAcess = new BcMooreDataAccess();
    }
    
    
    [TestMethod()]
    public async Task GetTeamsTestAsync()
    {

        //ARRANGE
        int expectedTeamCount = 332;

        //ACT
        var teams = await SourceDataAcess.GetTeams();

        //ASSSERT
        Assert.IsTrue(teams.Count() == expectedTeamCount); //Was able to retrieve scores
    }

}
