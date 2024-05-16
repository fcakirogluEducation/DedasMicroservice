using Clean.Domain;
using Clean.Service;
using Microsoft.Extensions.Caching.Memory;

namespace Clean.Cache
{
    public class ServiceCache(IMemoryCache cache) : ICacheService
    {
        public T? Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            cache.Set(key, value);
        }
    }
}