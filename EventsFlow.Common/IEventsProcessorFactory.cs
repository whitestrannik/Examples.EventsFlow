namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for creating IEventsProcessor instance
    /// </summary>
    public interface IEventsProcessorFactory
    {
        IEventsProcessor Create(IEventsSource eventsSource, IStorage storage);
    }
}
