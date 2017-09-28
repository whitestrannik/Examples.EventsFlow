using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class EventsProcessorWithAsyncStorage : EventsProcessorBase
    {
        public EventsProcessorWithAsyncStorage(IStorage storage, IEventsSource eventsSource) : base(storage, eventsSource)
        {
        }

        protected override void ProcessInternal(string key, object value)
        {
            _storage.AddOrUpdateAsync(key, value);
        }
    }
}
