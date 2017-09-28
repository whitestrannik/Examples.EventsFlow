using EventsFlow.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsFlow.Main.Impl
{
    public sealed class EventsSource : IEventsSource
    {
        private const int TotalValuesPercent = 100;
        private const int UniqueValuesPercent = 80;
        private const int PregeneratedValuesCount = 200;

        private readonly PregeneratedValues _pregeneratedValues;
        private readonly long _maxEventCount;
        private readonly Random _rnd;

        public EventsSource(long maxEventCount)
        {
            _maxEventCount = maxEventCount;
            _pregeneratedValues = new PregeneratedValues(PregeneratedValuesCount);
            _rnd = new Random();
        }

        public IEnumerable<Guid> Events
        {
            get
            {
                var i = 0L;
                while (i < _maxEventCount)
                {
                    yield return GenerateEvent();
                    i++;
                }
            }
        }

        private Guid GenerateEvent()
        {
            var random = _rnd.Next(100);

            return random < UniqueValuesPercent? Guid.NewGuid() : _pregeneratedValues.GetRandomValue();
        }

        private sealed class PregeneratedValues
        {
            private readonly Guid[] _values;
            private readonly Random _rnd;

            internal PregeneratedValues(int size)
            {
                _rnd = new Random();
                _values = Enumerable.Range(0, size).Select(i => Guid.NewGuid()).ToArray();
            }

            internal Guid GetRandomValue()
            {
                var random = _rnd.Next(_values.Length);
                return _values[random];
            }
        }
    }
}
