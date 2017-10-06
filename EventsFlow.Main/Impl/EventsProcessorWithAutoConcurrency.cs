using EventsFlow.Common;
using System;
using System.Threading.Tasks;

namespace EventsFlow.Main.Impl
{
    public sealed class EventsProcessorWithAutoConcurrency : IEventsProcessor
    {
        private readonly IStorage _storage;

        public EventsProcessorWithAutoConcurrency(IStorage storage)
        {
            _storage = storage;
        }

        public void Process(IEventsSource eventsSource)
        {
            Parallel.ForEach(eventsSource.GetEvents(), ProcessEvents);
        }

        private void ProcessEvents(Guid @event)
        {
            _storage.AddOrUpdate(@event.ToString(), new EventData());
        }
    }
}
