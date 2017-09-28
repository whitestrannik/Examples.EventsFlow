using Alachisoft.NCache.Web.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsFlow.Main.Impl
{
    public sealed class NCacheBulkStorage : NCacheStorageBase
    {
        private const int BulkItemCount = 200;
        private Task _task;
        private BlockingCollection<Tuple<string, object>> _items;

        public NCacheBulkStorage(string cacheName) : base(cacheName)
        {
            _items = new BlockingCollection<Tuple<string, object>>();
            _task = Task.Run(() => ProcessItems());
        }

        public override sealed void AddOrUpdate(string key, object value)
        {
            _items.Add(Tuple.Create(key, value));
        }

        protected override void Dispose(bool disposing)
        {
            _items.CompleteAdding();
            _task.Wait();

            base.Dispose(disposing);
        }

        private void ProcessItems()
        {
            var localDict = new Dictionary<string, object>();

            foreach (var item in _items.GetConsumingEnumerable())
            {
                localDict[item.Item1] = item.Item2;

                if (localDict.Count == BulkItemCount)
                {
                    Storage.InsertBulk(localDict.Keys.ToArray(), localDict.Values.Select(i => new CacheItem(i)).ToArray());
                    localDict.Clear();
                }
            }
        }
    }
}
