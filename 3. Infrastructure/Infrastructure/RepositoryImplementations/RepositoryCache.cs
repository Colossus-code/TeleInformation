using Contracts.RepositoryContracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Generic cache for get list by a id. 
    /// </summary>
    public class RepositoryCache : IRepositoryCache
    {
        private readonly IMemoryCache _memoryCache;
        public RepositoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

        }
        public List<T> GetCache<T>(int cacheId)
        {
            var response = _memoryCache.Get(cacheId);

            if(response == null) response = new List<T>();
            return (List<T>)response;
        }

        public void SetCache<T>(List<T> generic, int cacheId)
        {
            _memoryCache.Set(cacheId, generic);
        }
    }
}
