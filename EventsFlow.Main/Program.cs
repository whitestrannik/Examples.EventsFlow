using System;

namespace EventsFlow.Main
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var eventsCount = GetEventCount(args);
            var experimentBuilder = new ExperimentBuilder(eventsCount);

            try
            {
                //experimentBuilder.DoWithDummyStorageAndAutoConcurrency();                     //6100ms
                //experimentBuilder.DoWithDummyStorageAndManualConcurrency();                   //9140ms

                //experimentBuilder.DoWithInternalSyncStorageStorageAndAutoConcurrency();       //97740ms
                //experimentBuilder.DoWithInternalSyncStorageStorageAndManualConcurrency();     //97400ms
                
                //experimentBuilder.DoWithInternalAsyncStorageStorageAndAutoConcurrency();
                //experimentBuilder.DoWithInternalAsyncStorageStorageAndManualConcurrency();    //95000ms

                //experimentBuilder.DoWithInternalBulkStorageStorageAndAutoConcurrency();       //9700ms
                experimentBuilder.DoWithInternalBulkStorageStorageAndManualConcurrency();       //11620ms

                //experimentBuilder.DoWithExternalSyncStorageStorageAndManualConcurrency();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            Console.WriteLine("Press any key ...");
            Console.ReadLine();
        }

        private static long GetEventCount(string[] args)
        {
            long result = 0;

            if (args == null || args.Length == 0 || !long.TryParse(args[0], out result))
            {
                result = 1000000;
                Console.WriteLine($"EventS count parameter isn't set. Set default = {result}");
            }

            return result;
        }
    }
}
