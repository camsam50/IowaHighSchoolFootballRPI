using DataAccess.BcMoore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;
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
    public async Task DummyTestAsync()
    {

        //ARRANGE
        var x = 1;

        //ACT
        var variable = x;
        SourceDataAcess.tester();

        //ASSSERT
        Assert.IsTrue(variable == x); //Was able to retrieve scores
    }

}
