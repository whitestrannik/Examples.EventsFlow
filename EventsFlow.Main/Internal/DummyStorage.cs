using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class DummyStorage : IStorage
    {
        public void AddOrUpdate(string key, object value)
        {            
        }

        public void AddOrUpdateAsync(string key, object value)
        {            
        }

        public void Dispose()
        {           
        }

        public object Get(string key)
        {
            return null;   
        }
    }
}
