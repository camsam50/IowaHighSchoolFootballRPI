using Models.Data.BcMoore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces;
public interface ISourceDataAccess
{

    Task<IEnumerable<Team>> GetTeams();
    //Task<IEnumerable<Ranking>> GetRankings();
    //Task<IEnumerable<Schedule>> GetSchedules();
    //Task<IEnumerable<Score>> GetScores();
}
