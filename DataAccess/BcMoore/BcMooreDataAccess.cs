using Models.Data.BcMoore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccess.BcMoore
{

    public interface ISourceDataAccess
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<IEnumerable<Ranking>> GetRankings();
        Task<IEnumerable<Schedule>> GetSchedules();
        Task<IEnumerable<Score>> GetScores();
    }

    public class BcMooreDataAccess : ISourceDataAccess
    {
        private const string CURRENT_YEAR = "2021";


        public async Task<IEnumerable<Ranking>> GetRankings()
        {
            List<Ranking> rankings = new();
            var classes = new List<string>() { "5A", "4A", "3A", "2A", "1A", "A", "8" };

            using HttpClient client = GetHttpClient();

            foreach (var c in classes)
            {

                string responseBody = await client.GetStringAsync($"{c}Rank.html");

                string data = GetBetween(responseBody, "<pre>", "</pre>");
                var classRankings = ProcessHTML(data);

                rankings.AddRange(classRankings);


            }

            return rankings;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await GetData<Team>("team", ProcessTeam);
        }

        public async Task<IEnumerable<Schedule>> GetSchedules()
        {
            return await GetData<Schedule>("schedule", ProcessSchedule);
        }

        public async Task<IEnumerable<Score>> GetScores()
        {
            return await GetData<Score>("score", ProcessScore);

        }





        private static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        private static IEnumerable<Ranking> ProcessHTML(string data)
        {
            List<Ranking> rankings = new();
            string[] stringSeparators = new string[] { "\n" };
            string[] lines = data.Split(stringSeparators, StringSplitOptions.None);
            foreach (string s in lines)
            {
                if (s.Length > 90)
                {
                    Ranking r = GetRankingsData(s);
                    rankings.Add(r);
                }
            }
            return rankings;
        }



        
        private static async Task<IEnumerable<T>> GetData<T>(string fileName, Func<string[], T> processor)
        {


            //TODO: get from factory
            using HttpClient httpClient = GetHttpClient();

            string line;
            string[] parts;
            List<T> returnValues = new();


            using Stream response = await httpClient.GetStreamAsync($"{fileName}.csv");

            using var readFile = new StreamReader(response);


            while ((line = await readFile.ReadLineAsync()) != null)
            {
                parts = line.Split(',');

                var s = processor(parts);
                if (s is not null)
                {
                    returnValues.Add(s);
                }

            }

            return returnValues;



        }

        private static HttpClient GetHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri($"http://ia.bcmoorerankings.com/fb/{CURRENT_YEAR}/latest/")
            };
        }

        private static Ranking GetRankingsData(string data)
        {

            var namePart = GetBetween(data, "<a href=", "</a>");

            var longName = GetBetween(namePart, "/", ".html");

            var i1 = namePart.IndexOf(">");
            var shortName = namePart.Substring(i1 + 1);


            var i2 = data.IndexOf("-");
            var rankingPart = data.Substring(i2 + 11);


            _ = decimal.TryParse(rankingPart.AsSpan(0, 6), out decimal currentRanking);//81
            _ = decimal.TryParse(rankingPart.AsSpan(11, 6), out decimal scheduleRanking);//92
            _ = decimal.TryParse(rankingPart.AsSpan(24, 6), out decimal offensiveRanking);//105
            _ = decimal.TryParse(rankingPart.AsSpan(37, 6), out decimal defensiveRanking);//118

            
            return new Ranking
            {
                LongName = longName,
                ShortName = shortName,
                Rank = currentRanking,
                ScheduleAverage = scheduleRanking,
                OffensiveAverage = offensiveRanking,
                DefensiveAverage = defensiveRanking
            };
        }
        
        private static Team ProcessTeam(string[] parts)
        {
            if (parts == null || parts.Length != 5 || parts[0] == "Long name") 
            { return null; }
            else
            {
                return new Team()
                {
                    LongName = parts[0],
                    ShortName = parts[1],
                    Class = parts[2],
                    District = byte.Parse(parts[3])
                };
            }
            
        }

        private static Schedule ProcessSchedule(string[] parts)
        {
            if (parts == null) { return null; }
            if (parts.Length != 4) { return null; }
            if (parts[0] == "Date") { return null; }

            return new Schedule()
            {
                Date = DateTime.Parse(parts[0]),
                Visitor = parts[1],
                Home = parts[2],
                Location = byte.Parse(parts[3])
            };

        }

        private static Score ProcessScore(string[] parts)
        {
            if (parts == null) { return null; }
            if (parts.Length != 6) { return null; }
            if (parts[0] == "Date") { return null; }


            return new Score()
            {
                Date = DateTime.Parse(parts[0]),
                Visitor = parts[1],
                VisitorScore = byte.Parse(parts[2]),
                Home = parts[3],
                HomeScore = byte.Parse(parts[4]),
                Overtimes = byte.Parse(parts[5])
            };
        }


    }
}
