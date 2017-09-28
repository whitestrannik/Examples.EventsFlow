using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class NCacheExternalStorageFactory : IStorageFactory
    {
        public IStorage Create()
        {
            return new NCacheStorage("MyExternalCache");
        }
    }

    internal sealed class NCacheInternalStorageFactory : IStorageFactory
    {
        public IStorage Create()
        {
            return new NCacheStorage("MyInternalCache");
        }
    }

    internal sealed class DummyStorageFactory : IStorageFactory
    {
        public IStorage Create()
        {
            return new DummyStorage();
        }
    }

    internal sealed class NCacheBulkStorageFactory : IStorageFactory
    {
        public IStorage Create()
        {
            return new NCacheBulkStorage("MyInternalCache");
        }
    }
}
