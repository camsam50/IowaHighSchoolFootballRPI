using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Website.Data
{

    public interface IBcMooreService
    {
        Task<List<Team>> LoadTeams(int year);
    }

    public class BcMooreService
    {
        private readonly static HttpClient _httpClient;

        static BcMooreService()
        {
            var baseUri = new Uri("http://ia.bcmoorerankings.com");
            _httpClient = new HttpClient() { BaseAddress = baseUri };
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("JaysService");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region public

        public async Task<List<Team>> LoadTeams(int year)
        {
            //load our teams and scores at the same time
            var (teamTask, scoreTask) = (GetTeams(year), GetScores(year));
            await Task.WhenAll(teamTask, scoreTask).ConfigureAwait(false);
            var (teams, scores) = (teamTask.Result, scoreTask.Result);

            foreach (var score in scores)
            {
                var homeTeam = teams.SingleOrDefault(t => t.LongName.Equals(score.HomeTeam, StringComparison.OrdinalIgnoreCase));
                var visitingTeam = teams.SingleOrDefault(t => t.LongName.Equals(score.VisitorTeam, StringComparison.OrdinalIgnoreCase));

                if (homeTeam == null)
                    throw new Exception($"Cannot find home team with name '{score.HomeScore}'");

                if (visitingTeam == null)
                    throw new Exception($"Cannot find visiting team with name '{score.VisitorScore}'");

                homeTeam.AddGame(score.Date, true, score.HomeScore, visitingTeam, score.VisitorScore);
                visitingTeam.AddGame(score.Date, false, score.VisitorScore, homeTeam, score.HomeScore);
            }
            return teams;
        }


        #endregion

        #region Teams

        private async Task<List<Team>> GetTeams(int year)
        {
            string line;
            var teams = new List<Team>();

            using var response = await _httpClient.GetStreamAsync($"fb/{year}/latest/team.csv").ConfigureAwait(false);
            using var readFile = new StreamReader(response);
            while ((line = await readFile.ReadLineAsync().ConfigureAwait(false)) != null)
                if (TryParseTeam(line, out var team))
                    teams.Add(team);

            return teams;
        }

        private static bool TryParseTeam(string csvLine, out Team team)
        {
            team = null;
            var parts = csvLine?.Split(',');
            if (parts?.Length != 5 || parts[0].Equals("Long name", StringComparison.OrdinalIgnoreCase))
                return false;

            team = new Team()
            {
                LongName = parts[0],
                ShortName = parts[1],
                Class = parts[2],
                District = byte.Parse(parts[3])
            };
            return true;
        }

        #endregion

        #region Scores

        private async Task<List<Score>> GetScores(int year)
        {
            string line;
            var scores = new List<Score>();

            using Stream response = await _httpClient.GetStreamAsync($"fb/{year}/latest/score.csv").ConfigureAwait(false);
            using var readFile = new StreamReader(response);
            while ((line = await readFile.ReadLineAsync().ConfigureAwait(false)) != null)
                if (TryParseScore(line, out var score))
                    scores.Add(score);
            return scores;
        }


        private static bool TryParseScore(string csvLine, out Score score)
        {
            score = null;

            var parts = csvLine?.Split(',');
            if (parts?.Length != 6 || parts[0].Equals("Date", StringComparison.OrdinalIgnoreCase))
                return false;

            score = new Score()
            {
                Date = DateTime.Parse(parts[0]),
                VisitorTeam = parts[1],
                VisitorScore = byte.Parse(parts[2]),
                HomeTeam = parts[3],
                HomeScore = byte.Parse(parts[4]),
                Overtimes = byte.Parse(parts[5])
            };
            return true;
        }

        #endregion

    }

    public class Team
    {
        private List<TeamGame> _games = new List<TeamGame>();
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Class { get; set; }
        public byte District { get; set; }
        public string Conference => $"{Class}-{District}";
        public bool IsOutOfState => Class == "ZZ";
        public IReadOnlyCollection<TeamGame> Games => _games.AsReadOnly();
        public int Wins => _games.Count(g => g.IsWin);
        public int Losses => _games.Count(g => !g.IsWin);
        public decimal WinningPercentage => _games.Any() ? Wins / Convert.ToDecimal(Wins + Losses) : 0;

        public void AddGame(DateTime gameDate, bool isHomeGame, byte teamScore, Team opposingTeam, byte opposingTeamScore)
            => _games.Add(new TeamGame
            {
                Date = gameDate,
                IsHomeGame = isHomeGame,
                TeamPoints = teamScore,
                OpposingPoints = opposingTeamScore,
                OpposingTeam = opposingTeam
            });

        public decimal GetRatingsPercentageIndex()
        {
            //(.375 x WP) + (.375 x OWP) + (.250 x OOWP)
            var (owp, oowp) = (0m, 0m);
            if (_games.Any())
            {
                //TODO: somehow factor in out of state, but i didnt really understand the logic 

                var opponentsGames = _games.SelectMany(f => f.OpposingTeam.Games).ToList();
                owp = opponentsGames.Count(f => f.IsWin) / Convert.ToDecimal(opponentsGames.Count);

                var opponentsOpponentsGames = opponentsGames.SelectMany(f => f.OpposingTeam.Games).ToList();
                oowp = opponentsOpponentsGames.Count(f => f.IsWin) / Convert.ToDecimal(opponentsOpponentsGames.Count);
            }
            decimal rpi = (.375m * WinningPercentage) + (.375m * owp) + (.250m * oowp);
            return decimal.Round(rpi, 4);
        }
    }

    public class TeamGame
    {
        public DateTime Date { get; set; }
        public bool IsHomeGame { get; set; }
        public Team OpposingTeam { get; set; }
        public byte TeamPoints { get; set; }
        public byte OpposingPoints { get; set; }
        public bool IsWin => TeamPoints > OpposingPoints;
    }

    public class Score
    {
        public DateTime Date { get; set; }
        public string VisitorTeam { get; set; } //uses team's long name
        public byte VisitorScore { get; set; }
        public string HomeTeam { get; set; } //uses team's long name
        public byte HomeScore { get; set; }
        public byte Overtimes { get; set; }
    }


}
