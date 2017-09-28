using Alachisoft.NCache.Web.Caching;
using EventsFlow.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsFlow.Main.Internal
{
    internal sealed class NCacheBulkStorage : IStorage
    {
        private readonly Cache _cache;
        private int _count;
        private Task _task;
        private BlockingCollection<Tuple<string, object>> _items;

        public NCacheBulkStorage(string cacheName)
        {
            _cache = NCache.InitializeCache(cacheName);
            _items = new BlockingCollection<Tuple<string, object>>();
            _task = Task.Run(() => ProcessItems());
        }

        private void ProcessItems()
        {
            var localDict = new Dictionary<string, object>();

            foreach (var item in _items.GetConsumingEnumerable())
            {
                localDict[item.Item1] = item.Item2;

                if (localDict.Count == 200)
                {
                    _cache.InsertBulk(localDict.Keys.ToArray(), localDict.Values.Select(i => new CacheItem(i)).ToArray());
                    localDict.Clear();
                }
            }
        }

        public void AddOrUpdate(string key, object value)
        {
            _items.Add(Tuple.Create(key, value));
        }

        public void AddOrUpdateAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _items.CompleteAdding();
            _cache.Dispose();
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }
    }
}
