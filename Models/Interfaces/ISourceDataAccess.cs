using Models.Data.BcMoore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Interfaces;
public interface ISourceDataAccess
{

    string GetTestMessage();
    Task<IEnumerable<Team>> GetTeams();
    //Task<IEnumerable<Ranking>> GetRankings();
    //Task<IEnumerable<Schedule>> GetSchedules();
    //Task<IEnumerable<Score>> GetScores();
}
