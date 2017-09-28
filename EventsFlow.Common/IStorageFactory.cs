namespace EventsFlow.Common
{
    /// <summary>
    /// Contract for creating IStorage instance
    /// </summary>
    public interface IStorageFactory
    {
        IStorage Create();
    }
}
