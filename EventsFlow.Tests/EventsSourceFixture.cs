using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventsFlow.Main.Impl;
using EventsFlow.Common;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace EventsFlow.Tests
{
    [TestClass]
    public class EventsSourceFixture
    {
        [TestMethod]
        public void ShouldEnumerateEventsCountCorrespondInputParameter()
        {
            var eventsCount = 100;
            var sut = new EventsSource(eventsCount);

            var realEventsCount = 0;
            foreach (var @event in sut.GetEvents())
            {
                realEventsCount++;
            }

            Assert.AreEqual(eventsCount, realEventsCount);
        }
    }
}
