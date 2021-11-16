using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Converters
    {



        public static Data.Cosmos.Team CreateTeam(Data.BcMoore.Team team, Data.BcMoore.Ranking ranking)
        {

            //TODO: logic that the two objects are legit and match each other

            return new Data.Cosmos.Team
            {
                LongName = team.LongName,
                ShortName = team.ShortName,
                Class = team.Classification,
                District = team.District,
                Rank = ranking.Rank,
                ScheduleAverage = ranking.ScheduleAverage,
                OffensiveAverage = ranking.OffensiveAverage,
                DefensiveAverage = ranking.DefensiveAverage
            };
        }





        public static Data.Cosmos.Team ConvertToCosmos(this Data.BcMoore.Team team)
        {
            return new Data.Cosmos.Team
            {
                LongName = team.LongName,
                ShortName = team.ShortName,
                Class = team.Classification,
                District = team.District
            };
        }





    }
}
