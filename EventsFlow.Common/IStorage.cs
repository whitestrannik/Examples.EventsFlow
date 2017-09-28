using System;

namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for cache layer
    /// </summary>
    public interface IStorage : IDisposable
    {
        void AddOrUpdate(string key, object value);

        void AddOrUpdateAsync(string key, object value);

        object Get(string key);
    }
}
