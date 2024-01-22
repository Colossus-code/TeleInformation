using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepositoryCache
    {
        List<T> GetCache<T>(int cacheId);
        void SetCache<T>(List<T> generic, int cacheId);
    }
}
