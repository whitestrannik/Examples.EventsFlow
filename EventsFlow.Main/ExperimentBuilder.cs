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
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage, eventsSource);

                MakeExperiment("Dummy storage + manual concurrency", eventsProcessor);
            }
        }

        internal void DoWithDummyStorageAndAutoConcurrency()
        {
            using (var storage = new DummyStorage())
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage, eventsSource);

                MakeExperiment("Dummy storage + auto concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalSyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage, eventsSource);

                MakeExperiment("Sync in-proc storage + manual concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalSyncStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage, eventsSource);

                MakeExperiment("Sync in-proc storage + auto concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalAsyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheAsyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage, eventsSource);

                MakeExperiment("Async in-proc storage + manual concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalAsyncStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheAsyncStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage, eventsSource);

                MakeExperiment("Async in-proc storage + auto concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalBulkStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheBulkStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage, eventsSource);

                MakeExperiment("Bulk in-proc storage + manual concurrency", eventsProcessor);
            }
        }

        internal void DoWithInternalBulkStorageStorageAndAutoConcurrency()
        {
            using (var storage = new NCacheBulkStorage("MyInternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithAutoConcurrency(storage, eventsSource);

                MakeExperiment("Bulk in-proc storage + auto concurrency", eventsProcessor);
            }
        }

        internal void DoWithExternalSyncStorageStorageAndManualConcurrency()
        {
            using (var storage = new NCacheSyncStorage("MyExternalCache"))
            {
                var eventsSource = new EventsSource(_eventsCount);
                var eventsProcessor = new EventsProcessorWithManualConcurrency(storage, eventsSource);

                MakeExperiment("Sync out-proc storage + manual concurrency", eventsProcessor);
            }
        }

        private static void MakeExperiment(string experimentName, IEventsProcessor eventsProcessor)
        {
            Console.WriteLine($"=========== {experimentName} ===========");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            eventsProcessor.Process();
            stopWatch.Stop();
            Console.WriteLine($"Duration of experiment = {stopWatch.ElapsedMilliseconds} milliseconds.");
        }
    }
}
