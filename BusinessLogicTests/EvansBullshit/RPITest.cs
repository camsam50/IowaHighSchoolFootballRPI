using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTests.EvansBullshit
{
    [TestClass()]
    public class RPITest
    {
        [TestMethod()]
        public void TestWholeDeal()
        {
            //Example Data From Here: https://kenpom.com/blog/rpi-help/#:~:text=The%20RPI%20is%20calculated%20by,win%20is%20multiplied%20by%201.4.
            //Team A: Beat team B and team C
            //Team B: 1-1 record. Beat a 2-1 record and lost to a 2-0 Team A
            //Team C: 1-2 record. Team C beat a team with a 0-2 record, lost to a 2-0 record, lost to Team A

            Team a = new Team() { Name = "A"};//, Wins = 2, Losses = 0 };
            Team b = new Team() { Name = "B"};//, Wins = 1, Losses = 1 };
            Team c = new Team() { Name = "C"};//, Wins = 1, Losses = 2 };
            Team d = new Team() { Name = "D"};//, Wins = 2, Losses = 1 }; //Team that lost to B
            Team e = new Team() { Name = "E"};//, Wins = 0, Losses = 2 }; //Team that lost to C
            Team f = new Team() { Name = "F"};//, Wins = 2, Losses = 0 }; //Team that beat C
            //NOTE: These are just fillers to get B's opponents wins/losses right
            Team g = new Team() { Name = "G" }; //Team that lost to D
            Team h = new Team() { Name = "H" }; //Team that lost to D
            Team i = new Team() { Name = "I" }; //Team that beat E
            Team j = new Team() { Name = "J" }; //Team that lost to F

            //A's Games
            Game g1 = new Game() { Winner = a, Loser = b, Week = 1 };
            Game g2 = new Game() { Winner = a, Loser = c, Week = 2 };
            //B's Games
            //Week 1 loss to A is g1
            Game g3 = new Game() { Winner = b, Loser = d, Week = 2 };
            //C's Games
            Game g4 = new Game() { Winner = c, Loser = e, Week = 1 };
            Game g5 = new Game() { Winner = f, Loser = c, Week = 3 };
            //D's Games
            Game g6 = new Game() { Winner = d, Loser = g, Week = 3 };
            Game g7 = new Game() { Winner = d, Loser = h, Week = 3 };
            //E's Games
            Game g8 = new Game() { Winner = i, Loser = e, Week = 3 };
            //F's Games
            Game g9 = new Game() { Winner = f, Loser = j, Week = 3 };

            Season s = new Season()
            {
                Games = new List<Game>() { g1, g4, g2, g3, g5, g6, g7, g8, g9 }
            };

            foreach(Game gm in s.Games)
            {
                //g.Winner.Wins++;
                gm.Winner.OpponentsBeat.Add(gm.Loser);
                //g.Loser.Losses++;
                gm.Loser.OpponentsLost.Add(gm.Winner);
            }
            //A: 2-0
            Assert.AreEqual(2, a.Wins);
            Assert.AreEqual(0, a.Losses);
            Assert.IsTrue(a.OpponentsBeat.Contains(b));
            Assert.IsTrue(a.OpponentsBeat.Contains(c));
            //B: 1-1
            Assert.AreEqual(1, b.Wins);
            Assert.AreEqual(1, b.Losses);
            Assert.IsTrue(b.OpponentsLost.Contains(a));
            Assert.IsTrue(b.OpponentsBeat.Contains(d));
            //C: 1-2
            Assert.AreEqual(1, c.Wins);
            Assert.AreEqual(2, c.Losses);
            Assert.IsTrue(c.OpponentsLost.Contains(a));
            Assert.IsTrue(c.OpponentsBeat.Contains(e));
            Assert.IsTrue(c.OpponentsLost.Contains(f));
            //Opponents Opponents
            Assert.AreEqual(2, d.Wins);
            Assert.AreEqual(1, d.Losses);
            Assert.AreEqual(0, e.Wins);
            Assert.AreEqual(2, e.Losses);
            Assert.AreEqual(2, f.Wins);
            Assert.AreEqual(0, f.Losses);

            //A: RPI = 1/4 X (1) + 1/2 X (.75) + 1/4 X (.833) = .8333
            Assert.AreEqual(0.833, a.GetRPI(),0.001);
        }
    }

}
