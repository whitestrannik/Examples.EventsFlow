using EventsFlow.Common;
using EventsFlow.Main.Impl;
using System;
using System.Diagnostics;

namespace EventsFlow.Main
{
    internal sealed class ExperimentBuilder
    {
        private const int RepeatExperimentCount = 3;
        private readonly long _eventsCount;

        internal ExperimentBuilder(long eventsCount)
        {
            _eventsCount = eventsCount;
        }

        internal void DoWithDummyStorageAndManualConcurrency()
        {
            using (var storage = new DummyStorage())
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage);

                MakeExperiment("Dummy storage + manual concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithDummyStorageAndAutoConcurrency()
        {
            using (var storage = new DummyStorage())
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage);

                MakeExperiment("Dummy storage + auto concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalSyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage);

                MakeExperiment("Sync in-proc storage + manual concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalSyncStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage);

                MakeExperiment("Sync in-proc storage + auto concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalAsyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheAsyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage);

                MakeExperiment("Async in-proc storage + manual concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalAsyncStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheAsyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage);

                MakeExperiment("Async in-proc storage + auto concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalBulkStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheBulkStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage);

                MakeExperiment("Bulk in-proc storage + manual concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithInternalBulkStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheBulkStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage);

                MakeExperiment("Bulk in-proc storage + auto concurrency", eventsProcessor, eventsSource);
            }
        }

        internal void DoWithExternalSyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyExternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage);

                MakeExperiment("Sync out-proc storage + manual concurrency", eventsProcessor, eventsSource);
            }
        }

        private static void MakeExperiment(string experimentName, IEventsProcessor eventsProcessor, IEventsSource eventsSource)
        {
            Console.WriteLine($"=========== {experimentName} ===========");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            eventsProcessor.Process(eventsSource);
            stopWatch.Stop();
            Console.WriteLine($"Duration of experiment = {stopWatch.ElapsedMilliseconds} milliseconds.");
        }
    }
}
