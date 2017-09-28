using System;

namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for storage layer
    /// </summary>
    public interface IStorage : IDisposable
    {
        void AddOrUpdate(string key, object value);

        object Get(string key);
    }
}
