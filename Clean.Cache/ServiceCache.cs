using Clean.Service;

namespace Clean.Cache
{
    public class ServiceCache : ICacheService
    {
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}