using EventsFlow.Common;
using EventsFlow.Main.Internal;
using System;
using System.Diagnostics;

namespace EventsFlow.Main
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var eventsSourceFactory = new EventsSourceFactory(100000);
            var eventsProcessorWithSyncStorageFactory = new EventsProcessorWithSyncStorageFactory();
            var eventsProcessorWithAsyncStorageFactory = new EventsProcessorWithAsyncStorageFactory();

            var dummyStorageFactory = new DummyStorageFactory();
            var internalCacheFactory = new NCacheInternalStorageFactory();
            var externalCacheFactory = new NCacheExternalStorageFactory();
            var bulkStorageFactory = new NCacheBulkStorageFactory();

            try
            {
                //MakeExperiment("DummyStorage + blockingCollection", dummyStorageFactory, eventsSourceFactory, eventsProcessorWithAsyncStorageFactory);
                //MakeExperiment("NCacheInternal + sync + blockingCollection", internalCacheFactory, eventsSourceFactory, eventsProcessorWithSyncStorageFactory);
                MakeExperiment("NCacheInternal + sync + bulkStorage + blockingCollection", bulkStorageFactory, eventsSourceFactory, eventsProcessorWithSyncStorageFactory);
                //MakeExperiment("NCacheInternal + async + blockingCollection", internalCacheFactory, eventsSourceFactory, eventsProcessorWithAsyncStorageFactory);
                //MakeExperiment("NCacheExternal + blockingCollection", externalCacheFactory, eventsSourceFactory, eventsProcessorFactory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            
            Console.ReadLine();
        }

        private static void MakeExperiment(string experimentName, IStorageFactory storageFactory, IEventsSourceFactory eventsSourceFactory, IEventsProcessorFactory eventsProcessorFactory)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"=========== {experimentName} ===========");

                var stopWatch = new Stopwatch();

                using (var storage = storageFactory.Create())
                {
                    var eventsSource = eventsSourceFactory.Create();
                    var eventsProcessor = eventsProcessorFactory.Create(eventsSource, storage);

                    stopWatch.Start();
                    eventsProcessor.Process();
                    stopWatch.Stop();
                    Console.WriteLine($"Duration = {stopWatch.ElapsedMilliseconds} milliseconds.");
                }

                Console.WriteLine("========================================");
            }
        }
    }
}
