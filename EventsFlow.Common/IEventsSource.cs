using System;
using System.Collections.Generic;

namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for source of events
    /// </summary>
    public interface IEventsSource
    {
        IEnumerable<Guid> GetEvents();
    }
}
