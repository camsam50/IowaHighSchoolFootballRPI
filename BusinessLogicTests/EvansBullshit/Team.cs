using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BusinessLogicTests.EvansBullshit
{
    public class Team
    {
        public string Name { get; set; }
        public List<Team> OpponentsBeat = new List<Team>();
        public List<Team> OpponentsLost = new List<Team>();
        public int Wins { get { return OpponentsBeat.Count(); } }
        public int Losses { get { return OpponentsLost.Count(); } }
        public override string ToString()
        {
            return $"{Name}: {Wins}/{Losses}";
        }

        public double GetRPI()
        {
            //Part 1 (25%): Team Winning Percentage 
            double own_record = 0.25 * (Wins / (Wins + Losses));

            List<Team> allopponents = OpponentsBeat.Concat(OpponentsLost).ToList();

            //Part 2 (50%): Average opponents winning percentage
            //Kenpom says do the W/L and average
            List<double> opp_kp = new List<double>();
            foreach(Team t in allopponents)
            {
                //Don't use current team
                var wins = t.OpponentsBeat.Where(o => o.Name != Name).Count();
                var loss = t.OpponentsLost.Where(o => o.Name != Name).Count();
                opp_kp.Add((double)wins / (wins + loss));
            }
            var avg_kp = opp_kp.Average();
            double opp_record_kp = 0.5 * avg_kp;

            //Part 3 (25%): Average opponents opponents winning percentage
            List<double> opp_opp_kp = new List<double>();
            foreach (Team t in allopponents)
            {
                List<Team> all_opp_opp = t.OpponentsBeat.Concat(t.OpponentsLost).ToList();
                List<double> tmp = new List<double>();
                foreach (Team s in all_opp_opp)
                {
                    //Don't use current team
                    var wins = s.OpponentsBeat.Where(o => o.Name != t.Name).Count();
                    var loss = s.OpponentsLost.Where(o => o.Name != t.Name).Count();
                    tmp.Add((double)wins / (wins + loss));
                }
                opp_opp_kp.Add(tmp.Average());
            }
            var avg__opp_kp = opp_opp_kp.Average();
            double opp_opp_record_kp = 0.25 * avg__opp_kp;

            return own_record + opp_record_kp + opp_opp_record_kp;
        }
    }
}
