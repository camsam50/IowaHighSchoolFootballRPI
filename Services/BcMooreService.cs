using DataAccess.BcMoore;
using Models.Data.BcMoore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Interfaces;

namespace Services;


//public interface ISourceDataService
//{
//    Task<IEnumerable<Team>> GetTeams();
//    //Task<IEnumerable<Schedule>> GetSchedules();
//    //Task<IEnumerable<Score>> GetScores();
//    //Task<IEnumerable<Ranking>> GetRankings();


//    //TODO: UpdateAll, UpdateTeams (get teams/rankings from bcmoore and save combine to cosmos), UpdateGame (get score/schedule from bcmoore and save to cosomos as game) 

//}



//public class BcMooreService : ISourceDataService
//{

//    private readonly ISourceDataAccess _dataAccess;

//    public BcMooreService(ISourceDataAccess dataAccess)
//    {
//        _dataAccess = dataAccess;
//    }

        
//    public async Task<IEnumerable<Team>> GetTeams()
//    {
//        return await _dataAccess.GetTeams();
//    }

//    //public async Task<IEnumerable<Ranking>> GetRankings()
//    //{
//    //    return await _dataAccess.GetRankings();
//    //}

//    //public async Task<IEnumerable<Schedule>> GetSchedules()
//    //{
//    //    return await _dataAccess.GetSchedules();
//    //}

//    //public async Task<IEnumerable<Score>> GetScores()
//    //{
//    //    return await _dataAccess.GetScores();
//    //}
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
