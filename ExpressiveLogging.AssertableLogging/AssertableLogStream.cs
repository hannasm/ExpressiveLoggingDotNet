using ExpressiveLogging.BufferLogging;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpressiveLogging.AssertableLogging.AssertionLogStream;

namespace ExpressiveLogging.AssertableLogging
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

        public void AssertBeginScope(BeginScopeAssertion scope)
        {
            AssertBeginScope(scope, null);
        }
        public void AssertBeginScope(BeginScopeAssertion scope, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(scope, onError ?? _onAssertFailure));
        }
        public void AssertEndScope(EndScopeAssertion scope)
        {
            AssertEndScope(scope, null);
        }
        public void AssertEndScope(EndScopeAssertion scope, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(scope, onError ?? _onAssertFailure));
        }
        public void AssertIncrementRawCounter(RawCounterAssertion increment)
        {
            AssertIncrementRawCounter(increment, null);
        }
        public void AssertIncrementRawCounter(RawCounterAssertion increment, Action onError)
        {
            ExecuteAssert(new AssertionLogStream(increment, null, onError ?? _onAssertFailure));
        }
        public void AssertSetRawCounter(RawCounterAssertion set)
        {
            AssertSetRawCounter(set, null);
        }
        public void AssertSetRawCounter(RawCounterAssertion set, Action onError)
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
            AssertIncrementNamedCounter(set, null);
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
