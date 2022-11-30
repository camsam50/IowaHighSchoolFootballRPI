//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net.Http;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Threading.Tasks;
////using Dapper;

//namespace RPI
//{


//    public class Team
//    {
//        public string LongName { get; set; }
//        public string ShortName { get; set; }
//        public string Class { get; set; }
//        public int District { get; set; }
//        public string Conference { get; set; }
//        public decimal CurrentRanking { get; set; }
//        public DateTime LastUpdatedDate { get; set; }
//        public int Wins { get; set; }
//        public int Losses { get; set; }
//        public bool IsOutOfState { get; set; }
//        public decimal WinningPercentage
//        {
//            get
//            {
//                return (Wins + Losses) == 0 ? (decimal)0 : (decimal)Wins / ((decimal)Wins + (decimal)Losses);
//            }
//        }
//        //public decimal WinningPercentage
//        //{
//        //    get
//        //    {
//        //        return (decimal)((decimal)3 / ((decimal)3 + (decimal)6));
//        //    }
//        //}


//        public List<Team> Opponents { get; set; } = new List<Team>();

//        public int OpponentWins()
//        {
//            return Opponents.Sum(o => o.Wins);
//        }
//        public int OpponentLosses()
//        {
//            return Opponents.Sum(o => o.Losses);

//        }
//        public decimal OpponentWinningPercentage()
//        {
//            if (this.IsOutOfState) { return .5m; }
//            if ((OpponentWins() + OpponentLosses()) == 0) { return 0m; }
//            else { return ((decimal)OpponentWins() / ((decimal)OpponentWins() + (decimal)OpponentLosses())); }
//        }


//        public int OpponentsOpponentWins()
//        {
//            return Opponents.Sum(o => o.Opponents.Sum(oo => oo.Wins));
//        }
//        public int OpponentsOpponentLosses()
//        {
//            return Opponents.Sum(o => o.Opponents.Sum(oo => oo.Losses));
//        }
//        public decimal OpponentsOpponentWinningPercentage()
//        {
//            if (this.IsOutOfState) { return .5m; }
//            if ((OpponentsOpponentWins() + OpponentsOpponentLosses()) == 0) { return 0m; }
//            else { return ((decimal)OpponentsOpponentWins() / ((decimal)OpponentsOpponentWins() + (decimal)OpponentsOpponentLosses())); }
//        }

//        public decimal RPI()
//        {
//            //(.375 x WP) + (.375 x OWP) + (.250 x OOWP)
//            return decimal.Round((.375m * WinningPercentage) + (.375m * OpponentWinningPercentage()) + (.250m * OpponentsOpponentWinningPercentage()), 4);
//        }


//    }



//    public class Game
//    {
//        public string Visitor { get; set; }
//        public string Home { get; set; }

//    }


//    class Program
//    {
//        static void Main()
//        {

//            //_ = GetBcMooreData();
//            PredictRPI();




//            Thread.Sleep(10000);
//        }


//        static void PredictRPI()
//        {
//            var games = new List<Game>(); // GetGames().Result;

//            var teams = new List<Team>(); // GetTeams().Result;
//            foreach (var team in teams)
//            {
//                //Console.WriteLine(team.LongName + " - " + team.OpponentWins().ToString() + " - " + team.OpponentsOpponentWins().ToString());

//                List<string> opponentNames = new();

//                opponentNames.AddRange(games.Where(x => x.Home == team.LongName).Select(x => x.Visitor).ToList());
//                opponentNames.AddRange(games.Where(x => x.Visitor == team.LongName).Select(x => x.Home).ToList());

//                var g = games.Where(x => x.Home == team.LongName).Select(x => x.Visitor).ToList();


//                //var list = new List<string> { ... };
//                //var query = query.Where(x => x.tags.Any(tag => list.Contains(tag));

//                var q = teams.Where(x => opponentNames.Contains(x.LongName));
//                foreach (var u in q)
//                {
//                    //Console.WriteLine("         " + u.LongName);
//                }

//                team.Opponents = q.ToList();

//                //Console.WriteLine(team.OpponentWins().ToString() + " - " + team.OpponentsOpponentWins().ToString());
//                //Console.WriteLine(team.RPI());
//            }



//            var care = teams.Where(t => t.Class == "4A").OrderByDescending(t => t.RPI());
//            Console.WriteLine("TEAM".PadRight(25) + " W/L".PadRight(8) + " O W/L".PadRight(11) + " OO W/L".PadRight(13) + "RPI");
//            foreach (var c in care)
//            {
//                Console.WriteLine(c.ShortName.PadRight(25) + "(" + c.Wins + "-" + c.Losses + ") | (" + c.OpponentWins() + "-" + c.OpponentLosses() + ") | (" + c.OpponentsOpponentWins() + " - " + c.OpponentsOpponentLosses() + ") = " + c.RPI());
//            }
//        }


//        //static async Task GetBcMooreData()
//        //{
//        //    var classes = new List<string>() { "4A", "3A", "2A", "1A", "A", "8" }; //"3A-A"
//        //    HttpClient client = new HttpClient();
//        //    using (client)
//        //    {
//        //        foreach (var division in classes)
//        //        {
//        //            Uri uri = new Uri("http://ia.bcmoorerankings.com/fb/2020/latest/" + division + "Rank.html");
//        //            string responseBody = await client.GetStringAsync(uri);
//        //            string data = GetBetween(responseBody, "<pre>", "</pre>");
//        //            ProcessHTML(data);
//        //        }
//        //    }


//        //}

//        //static void ProcessHTML(string data)
//        //{
//        //    string[] stringSeparators = new string[] { "\n" };
//        //    string[] lines = data.Split(stringSeparators, StringSplitOptions.None);
//        //    foreach (string s in lines)
//        //    {
//        //        if (s.Length > 90)
//        //        {
//        //            //Console.WriteLine(s);


//        //            var a = GetBetween(s, "<a href=", "</a>");
//        //            //Console.WriteLine(a);

//        //            var longName = GetBetween(a, "/", ".html");
//        //            Console.WriteLine("Long Name: " + longName);

//        //            var i = a.IndexOf(">");
//        //            var shortName = a.Substring(i + 1);
//        //            Console.WriteLine("ShortName: " + shortName);


//        //            var b = GetBetween(s, ") ", "(");
//        //            b = b.Substring(0, 6);
//        //            //Console.WriteLine("b = " + b);
//        //            decimal.TryParse(b, out decimal currentRanking);
//        //            Console.WriteLine("CurrentRanking: " + currentRanking);

//        //            //string[] split = s.Split('\t');
//        //            //foreach(var x in split)
//        //            //{
//        //            //    //Console.WriteLine("x = " + x);
//        //            //}

//        //            //await UpdateCurrentRanking(longName, shortName, currentRanking);

//        //            Console.WriteLine();

//        //        }


//        //        //if (Regex.IsMatch(s, @"^\d"))
//        //        //{
//        //        //    Console.WriteLine("YES -> " + s);
//        //        //}
//        //        //else
//        //        //{
//        //        //    Console.WriteLine("BAD -> " + s);
//        //        //}

//        //    }
//        //}

//        //static async Task UpdateCurrentRanking(string longName, string shortName, decimal currentRanking)
//        //{
//        //    string sqlText = "UPDATE dbo.Teams SET [CurrentRanking] = " + currentRanking + ", [LastUpdatedDate] = GETDATE() WHERE [LongName] = '" + longName.Replace("'", "''") + "' AND [ShortName] = '" + shortName.Replace("'", "''") + "'";
//        //    string connectionString = "Server=JDCAMP-BOOK\\EXPRESS2014;Database=TestDatabase;Trusted_Connection=True;";
//        //    var conn = new SqlConnection(connectionString);
//        //    using (conn)
//        //    {
//        //        await conn.OpenAsync();
//        //        var command = new SqlCommand(sqlText, conn);
//        //        var x = await command.ExecuteNonQueryAsync();

//        //    }
//        //}


//        //static async Task<IEnumerable<Team>> GetTeams()
//        //{
//        //    string sql = "SELECT * FROM dbo.Teams";
//        //    string connectionString = "Server=JDCAMP-BOOK\\EXPRESS2014;Database=TestDatabase;Trusted_Connection=True;";

//        //    using (var connection = new SqlConnection(connectionString))
//        //    {
//        //        return await connection.QueryAsync<Team>(sql);
//        //    }
//        //}

//        //static async Task<IEnumerable<Game>> GetGames()
//        //{
//        //    string sql = "SELECT * FROM dbo.Schedules";
//        //    string connectionString = "Server=JDCAMP-BOOK\\EXPRESS2014;Database=TestDatabase;Trusted_Connection=True;";

//        //    using (var connection = new SqlConnection(connectionString))
//        //    {
//        //        return await connection.QueryAsync<Game>(sql);
//        //    }
//        //}



//        public static string GetBetween(string strSource, string strStart, string strEnd)
//        {
//            int Start, End;
//            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
//            {
//                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
//                End = strSource.IndexOf(strEnd, Start);
//                return strSource.Substring(Start, End - Start);
//            }
//            else
//            {
//                return "";
//            }
//        }



//    }
//}


//namespace Models.Business
//{
//    class Class1
//    {


//        //Team & properties

//        //list of games to get opponents and 

//        //calculate wins/losses

//        public Lazy<Team> Team { get; set; }


//    }
//}
