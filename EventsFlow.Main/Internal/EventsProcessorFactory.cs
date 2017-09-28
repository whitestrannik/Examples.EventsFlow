using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class EventsProcessorWithSyncStorageFactory : IEventsProcessorFactory
    {
        public IEventsProcessor Create(IEventsSource eventsSource, IStorage storage)
        {
            return new EventsProcessorWithSyncStorage(storage, eventsSource);
        }
    }

    internal sealed class EventsProcessorWithAsyncStorageFactory : IEventsProcessorFactory
    {
        public IEventsProcessor Create(IEventsSource eventsSource, IStorage storage)
        {
            return new EventsProcessorWithAsyncStorage(storage, eventsSource);
        }
    }
}
