using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class EventsProcessorWithSyncStorage : EventsProcessorBase
    {
        public EventsProcessorWithSyncStorage(IStorage storage, IEventsSource eventsSource) : base(storage, eventsSource)
        {
        }

        protected override void ProcessInternal(string key, object value)
        {
            _storage.AddOrUpdate(key, value);
        }
    }
}
