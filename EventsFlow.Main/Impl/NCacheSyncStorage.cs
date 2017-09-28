namespace EventsFlow.Main.Impl
{
    public sealed class NCacheSyncStorage : NCacheStorageBase
    {
        public NCacheSyncStorage(string cacheName) : base(cacheName)
        { }

        public override sealed void AddOrUpdate(string key, object value)
        {
            Storage.Insert(key, value);
        }
    }
}
