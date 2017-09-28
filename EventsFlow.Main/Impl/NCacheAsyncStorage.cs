namespace EventsFlow.Main.Impl
{
    public sealed class NCacheAsyncStorage : NCacheStorageBase
    {
        public NCacheAsyncStorage(string cacheName) : base(cacheName)
        { }

        public override sealed void AddOrUpdate(string key, object value)
        {
            Storage.InsertAsync(key, value, null, null, null);
        }
    }
}
