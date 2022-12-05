using Models.Data.BcMoore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces;
public interface ISourceDataService
{
    Task<IEnumerable<Team>> GetTeams();
}
