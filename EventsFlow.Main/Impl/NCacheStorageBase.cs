using Alachisoft.NCache.Web.Caching;
using EventsFlow.Common;
using System;

namespace EventsFlow.Main.Impl
{
    public abstract class NCacheStorageBase : IStorage
    {
        protected NCacheStorageBase(string cacheName)
        {
            Storage = NCache.InitializeCache(cacheName);
        }

        protected Cache Storage { get; }

        public abstract void AddOrUpdate(string key, object value);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual object Get(string key)
        {
            return Storage.Get(key);
        }

        protected virtual void Dispose(bool disposing)
        {
            Storage.Dispose();
        }
    }
}
