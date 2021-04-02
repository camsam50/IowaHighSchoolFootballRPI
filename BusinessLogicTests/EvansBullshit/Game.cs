using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTests.EvansBullshit
{
    public class Game
    {
        public int Week { get; set; }
        public Team Winner { get; set; }
        public Team Loser { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
