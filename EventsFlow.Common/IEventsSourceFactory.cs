namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for creating IEventsSource instance
    /// </summary>
    public interface IEventsSourceFactory
    {
        IEventsSource Create();
    }
}
