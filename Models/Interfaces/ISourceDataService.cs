using Models.Data.BcMoore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces;
public interface ISourceDataService
{
    string GetTestMessage();
    Task<IEnumerable<Team>> GetTeams();
}
