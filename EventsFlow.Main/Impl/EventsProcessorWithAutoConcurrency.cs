using EventsFlow.Common;
using System;
using System.Threading.Tasks;

namespace EventsFlow.Main.Impl
{
    public sealed class EventsProcessorWithAutoConcurrency : IEventsProcessor
    {
        private readonly IEventsSource _eventsSource;
        private readonly IStorage _storage;

        public EventsProcessorWithAutoConcurrency(IStorage storage, IEventsSource eventsSource)
        {
            _storage = storage;
            _eventsSource = eventsSource;
        }

        public void Process()
        {
            Parallel.ForEach(_eventsSource.Events, ProcessEvents);
        }

        private void ProcessEvents(Guid @event)
        {
            _storage.AddOrUpdate(@event.ToString(), new EventData());
        }
    }
}
