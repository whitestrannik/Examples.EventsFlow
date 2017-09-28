# Examples.EventsFlow

- simple implementation of multithreaded processing  of input sequence of objects using NCache;
- requirements: VS2015 + .Net 4.6.1. + NCache installed (precreated in-proc cache with "MyInternalCache" and out-proc with "MyExternalCache");
- without real profiling\optimizing\error handling\storage tuning etc. - only quick concept prototyping;
- there are several implementations of each componenets (events source, events processor, storage) and experiments uses some combinations;
  Best result: In-proc NCache for storage (with Bulk inserts) and standard Parallel.Foreach for parallelize processing:
  1M objects - 9700ms (Surface Pro 3 Core I5, 8Gb);
- Unit tests are added only as example - they are not covered all stuff;