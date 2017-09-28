using EventsFlow.Common;

namespace EventsFlow.Main.Internal
{
    internal sealed class EventsSourceFactory : IEventsSourceFactory
    {
        private readonly long _eventCount;

        public EventsSourceFactory(long eventCount)
        {
            _eventCount = eventCount;
        }

        public IEventsSource Create()
        {
            return new EventsSource(_eventCount);
        }
    }
}
