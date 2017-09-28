using Alachisoft.NCache.Web.Caching;
using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class NCacheStorage : IStorage
    {
        private readonly Cache _cache;

        public NCacheStorage(string cacheName)
        {
            _cache = NCache.InitializeCache(cacheName);
        }

        public void AddOrUpdate(string key, object value)
        {
            _cache.Insert(key, value);
        }

        public void AddOrUpdateAsync(string key, object value)
        {
            _cache.InsertAsync(key, value, null, null, null);
        }

        public void Dispose()
        {
            _cache.Dispose();
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }
    }
}
