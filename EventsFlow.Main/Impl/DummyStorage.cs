using EventsFlow.Common;

namespace EventsFlow.Main.Impl
{
    public sealed class DummyStorage : IStorage
    {
        public void AddOrUpdate(string key, object value)
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
