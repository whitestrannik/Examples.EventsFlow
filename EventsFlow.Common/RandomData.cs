using System;

namespace EventsFlow.Common
{
    [Serializable]
    public sealed class RandomData
    {
        private const int Size = 1024;

        public RandomData()
        {
            Data = new byte[Size];
            TimeStamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffffff");
        }

        public byte[] Data { get; }

        public string TimeStamp { get; }
    }
}
