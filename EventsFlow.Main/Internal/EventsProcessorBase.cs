using EventsFlow.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace EventsFlow.Main.Internal
{
    internal abstract class EventsProcessorBase : IEventsProcessor
    {
        private readonly IEventsSource _eventsSource;
        private BlockingCollection<Guid> _eventsCollectionForProcess;
        private Task[] _processors;

        protected readonly IStorage _storage;

        protected EventsProcessorBase(IStorage storage, IEventsSource eventsSource)
        {
            _storage = storage;
            _eventsSource = eventsSource;
        }

        public void Process()
        {
            var degreeOfConcurrency = CalculateDegreeOfConcurrency();
            _eventsCollectionForProcess = new BlockingCollection<Guid>(degreeOfConcurrency+10);
            _processors = Enumerable
                .Range(0, degreeOfConcurrency + 4)
                .Select(i => Task.Run(() => ProcessEvents()))
                .ToArray();

            foreach (var @event in _eventsSource.Events)
            {
                _eventsCollectionForProcess.Add(@event);
            }

            _eventsCollectionForProcess.CompleteAdding();
        }

        private void ProcessEvents()
        {
            foreach (var @event in _eventsCollectionForProcess.GetConsumingEnumerable())
            {
                ProcessInternal(@event.ToString(), new RandomData());
            }
        }

        protected abstract void ProcessInternal(string key, object value);

        private int CalculateDegreeOfConcurrency()
        {
            return Environment.ProcessorCount;
        }
    }
}
