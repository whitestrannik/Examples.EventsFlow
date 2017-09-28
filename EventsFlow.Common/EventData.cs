using System;

namespace EventsFlow.Common
{
    /// <summary>
    /// Event's data
    /// </summary>
    [Serializable]
    public sealed class EventData
    {
        private const int Size = 1024;

        public EventData()
        {
            Data = new byte[Size];
            TimeStamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffffff");
        }

        public byte[] Data { get; }

        public string TimeStamp { get; }
    }
}
