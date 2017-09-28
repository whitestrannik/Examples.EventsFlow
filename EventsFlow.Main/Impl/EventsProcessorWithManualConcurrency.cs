using EventsFlow.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace EventsFlow.Main.Impl
{
    public sealed class EventsProcessorWithManualConcurrency : IEventsProcessor
    {
        private readonly IEventsSource _eventsSource;
        private readonly IStorage _storage;
        private BlockingCollection<Guid> _eventsCollectionForProcess;
        private Task[] _tasks;

        public EventsProcessorWithManualConcurrency(IStorage storage, IEventsSource eventsSource)
        {
            _storage = storage;
            _eventsSource = eventsSource;
        }

        public void Process()
        {
            var degreeOfConcurrency = CalculateDegreeOfConcurrency();

            _eventsCollectionForProcess = new BlockingCollection<Guid>(2 * degreeOfConcurrency);
            _tasks = Enumerable
                .Range(0, degreeOfConcurrency)
                .Select(i => Task.Run(() => ProcessEvents()))
                .ToArray();

            foreach (var @event in _eventsSource.Events)
            {
                _eventsCollectionForProcess.Add(@event);
            }

            _eventsCollectionForProcess.CompleteAdding();
            Task.WaitAll(_tasks);
        }

        private void ProcessEvents()
        {
            foreach (var @event in _eventsCollectionForProcess.GetConsumingEnumerable())
            {
                _storage.AddOrUpdate(@event.ToString(), new EventData());
            }
        }

        private int CalculateDegreeOfConcurrency()
        {
            return Environment.ProcessorCount;
        }
    }
}
