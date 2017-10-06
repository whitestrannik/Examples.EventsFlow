using EventsFlow.Common;
using EventsFlow.Main.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventsFlow.Tests
{
    [TestClass]
    public class EventsProcessorFixture
    {
        [TestMethod]
        public void ShouldUpdateStorageForEveryEvents()
        {
            var inputData = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var eventsSource = new EventsSourceMock(inputData);
            var storage = new StorageMock();

            var sut = new EventsProcessorWithAutoConcurrency(storage);

            sut.Process(eventsSource);

            Assert.AreEqual(storage.Result.Count, inputData.Count);
        }

        private sealed class StorageMock : IStorage
        {
            public StorageMock()
            {
                Result = new ConcurrentQueue<Tuple<string, object>>();
            }

            public ConcurrentQueue<Tuple<string, object>> Result { get; set; }

            public void AddOrUpdate(string key, object value)
            {
                Result.Enqueue(Tuple.Create(key, value));
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public object Get(string key)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class EventsSourceMock : IEventsSource
        {
            private readonly List<Guid> _data;

            public EventsSourceMock(List<Guid> data)
            {
                _data = data;
            }

            public IEnumerable<Guid> GetEvents()
            {
                return _data;
            }
        }
    }
}
