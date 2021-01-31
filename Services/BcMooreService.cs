using Models.Data.BcMoore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{

    public interface IBcMooreService
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<IEnumerable<Score>> GetScores();

    }



    public class BcMooreService : IBcMooreService
    {

        private const string CURRENT_YEAR = "2020";
        //private readonly HttpClient _httpClient;

        //public BcMooreService(HttpClient client)
        //{
        //    _httpClient = client;
        //    _httpClient.BaseAddress = new Uri("http://ia.bcmoorerankings.com/fb/2020/latest/");
        //}

        public async Task<IEnumerable<Team>> GetTeams()
        {
            HttpClient _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://ia.bcmoorerankings.com/fb/{CURRENT_YEAR}/latest/")
            };

            string line;
            string[] parts;
            List<Team> teams = new();


            using Stream response = await _httpClient.GetStreamAsync("team.csv");

            using var readFile = new StreamReader(response);


            while ((line = await readFile.ReadLineAsync()) != null)
            {
                parts = line.Split(',');

                var t = ProcessTeam(parts);
                if (t is not null)
                {
                    teams.Add(t);
                }

            }

            return teams;
        }

        public async Task<IEnumerable<Score>> GetScores()
        {
            HttpClient _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://ia.bcmoorerankings.com/fb/{CURRENT_YEAR}/latest/")
            };

            string line;
            string[] parts;
            List<Score> scores = new();


            using Stream response = await _httpClient.GetStreamAsync("score.csv");

            using var readFile = new StreamReader(response);


            while ((line = await readFile.ReadLineAsync()) != null)
            {
                parts = line.Split(',');

                var s = ProcessScore(parts);
                if (s is not null)
                {
                    scores.Add(s);
                }

            }

            return scores;
        }


        private static Team ProcessTeam(string[] parts)
        {
            if (parts == null) { return null; }
            if (parts.Length != 5) { return null; }
            if (parts[0] == "Long name") { return null; }

            return new Team()
            {
                LongName = parts[0],
                ShortName = parts[1],
                Class = parts[2],
                District = byte.Parse(parts[3])
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
                VisitorTeam = parts[1],
                VisitorScore = byte.Parse(parts[2]),
                HomeTeam = parts[3],
                HomeScore = byte.Parse(parts[4]),
                Overtimes = byte.Parse(parts[5])
            };
        }


    }







    //public interface IBcMooreService
    //{
    //    Task<IEnumerable<Team>> GetTeams();
    //    Task<IEnumerable<TeamRecord>> GetTeamRecords();
    //    Task<IEnumerable<Score>> GetScores();
    //}

    //public class BcMooreService : IBcMooreService
    //{

    //    private readonly HttpClient _httpClient;

    //    public BcMooreService(HttpClient client)
    //    {
    //        _httpClient = client;
    //        _httpClient.BaseAddress = new Uri("http://ia.bcmoorerankings.com/fb/2020/latest/");
    //    }


    //    public async Task<IEnumerable<Team>> GetTeams()
    //    {
    //        string line;
    //        string[] parts;
    //        List<Team> teams = new();


    //        using Stream response = await _httpClient.GetStreamAsync("team.csv");

    //        using var readFile = new StreamReader(response);


    //        while ((line = await readFile.ReadLineAsync()) != null)
    //        {
    //            parts = line.Split(',');

    //            var t = ProcessTeam(parts);
    //            if (t is not null)
    //            {
    //                teams.Add(t);
    //            }

    //        }

    //        return teams;
    //    }

    //    public async Task<IEnumerable<TeamRecord>> GetTeamRecords()
    //    {
    //        string line;
    //        string[] parts;
    //        List<TeamRecord> teams = new();


    //        using Stream response = await _httpClient.GetStreamAsync("team.csv");

    //        using var readFile = new StreamReader(response);


    //        while ((line = await readFile.ReadLineAsync()) != null)
    //        {
    //            parts = line.Split(',');

    //            var t = ProcessTeamRecord(parts);
    //            if (t is not null)
    //            {
    //                teams.Add(t);
    //            }

    //        }

    //        return teams;
    //    }

    //    public async Task<IEnumerable<Score>> GetScores()
    //    {
    //        string line;
    //        string[] parts;
    //        List<Score> scores = new();


    //        using Stream response = await _httpClient.GetStreamAsync("score.csv");

    //        using var readFile = new StreamReader(response);


    //        while ((line = await readFile.ReadLineAsync()) != null)
    //        {
    //            parts = line.Split(',');

    //            var s = ProcessScore(parts);
    //            if (s is not null)
    //            {
    //                scores.Add(s);
    //            }

    //        }

    //        return scores;
    //    }



    //    //public async void dothings()
    //    //{
    //    //    var _avatarCache = new WaitToFinishMemoryCache<byte[]>();
    //    //    object userId = null;
    //    //    object _database = null;
    //    //    // ...
    //    //    var myAvatar = await _avatarCache.GetOrCreate(userId, async () => await _database.GetAvatar(userId));
    //    //}






    //    private static Team ProcessTeam(string[] parts)
    //    {
    //        if (parts == null) { return null; }
    //        if (parts.Length != 5) { return null; }
    //        if (parts[0] == "Long name") { return null; }

    //        return new Team()
    //        {
    //            LongName = parts[0],
    //            ShortName = parts[1],
    //            Class = parts[2],
    //            District = byte.Parse(parts[3])
    //        };
    //    }

    //    private static TeamRecord ProcessTeamRecord(string[] parts)
    //    {
    //        if (parts == null) { return null; }
    //        if (parts.Length != 5) { return null; }
    //        if (parts[0] == "Long name") { return null; }

    //        return new TeamRecord()
    //        {
    //            LongName = parts[0],
    //            ShortName = parts[1],
    //            Class = parts[2],
    //            District = byte.Parse(parts[3])
    //        };
    //    }

    //    private static Score ProcessScore(string[] parts)
    //    {
    //        if (parts == null) { return null; }
    //        if (parts.Length != 6) { return null; }
    //        if (parts[0] == "Date") { return null; }


    //        return new Score()
    //        {
    //            Date = DateTime.Parse(parts[0]),
    //            VisitorTeam = parts[1],
    //            VisitorScore = byte.Parse(parts[2]),
    //            HomeTeam = parts[3],
    //            HomeScore = byte.Parse(parts[4]),
    //            Overtimes = byte.Parse(parts[5])
    //        };
    //    }


    //}




    //public class WaitToFinishMemoryCache<TItem>
    //{
    //    private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    //    private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

    //    public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
    //    {
    //        TItem cacheEntry;

    //        if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
    //        {
    //            SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

    //            await mylock.WaitAsync();
    //            try
    //            {
    //                if (!_cache.TryGetValue(key, out cacheEntry))
    //                {
    //                    // Key not in cache, so get data.
    //                    cacheEntry = await createItem();
    //                    _cache.Set(key, cacheEntry);
    //                }
    //            }
    //            finally
    //            {
    //                mylock.Release();
    //            }
    //        }
    //        return cacheEntry;
    //    }
    //}






}
