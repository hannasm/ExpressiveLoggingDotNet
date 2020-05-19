using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpressiveLogging.V3.AssertionLogStream;

namespace ExpressiveLogging.V3
{
    public class AssertableLogStream : BufferedLogStream
    {
        Action _onAssertFailure;
        public AssertableLogStream(Action onAssertFailure)
        {
            _onAssertFailure = onAssertFailure;
        }

        public void AssertMessage(LoggingAssertion message)
        {
            AssertMessage(message, null);
        }
        public void AssertMessage(LoggingAssertion message, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(message, onError ?? _onAssertFailure));
        }

        public void AssertIncrementRawCounter(CounterAssertion increment)
        {
            AssertIncrementRawCounter(increment, null);
        }
        public void AssertIncrementRawCounter(CounterAssertion increment, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(increment, null, onError ?? _onAssertFailure));
        }
        public void AssertSetRawCounter(CounterAssertion set)
        {
            AssertSetRawCounter(set, null);
        }
        public void AssertSetRawCounter(CounterAssertion set, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(null, set, onError ?? _onAssertFailure));

        }
        public void AssertIncrementNamedCounter(NamedCounterAssertion increment)
        {
            AssertIncrementNamedCounter(increment, null);
        }
        public void AssertIncrementNamedCounter(NamedCounterAssertion increment, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(increment, null, onError ?? _onAssertFailure));
        }
        public void AssertSetNamedCounter(NamedCounterAssertion set)
        {
            AssertSetNamedCounter(set, null);
        }
        public void AssertSetNamedCounter(NamedCounterAssertion set, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(null, set, onError ?? _onAssertFailure));
        }

        protected void ExecuteAssert(ILogStream asserter)
        {
            if (ExecuteBuffer(asserter, 1) != 1)
            {
                _onAssertFailure();
            }
        }
    }
}
