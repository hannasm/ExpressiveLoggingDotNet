using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.V3.Context;

namespace ExpressiveLogging.V3
{
    public delegate void LoggingAssertion(ILogToken token, Exception error, int? uniqueness, string message, object[] fmt);
    public delegate void CounterAssertion(ICounterToken ct, long value);
    public delegate void NamedCounterAssertion(INamedCounterToken ct, long value);

    internal class AssertionLogStream : ILogStream
    {
        public AssertionLogStream(LoggingAssertion message, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (message == null) { throw new ArgumentNullException("message"); }

            initializeDelegates(_onError = onError);

            _message = message;
        }

        public AssertionLogStream(CounterAssertion increment, CounterAssertion set, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (increment == null && set == null) { throw new ArgumentNullException("increment and set"); }

            initializeDelegates(_onError = onError);

            if (increment != null) { _incrementRawCounter = increment; }
            if (set != null) { _setRawCounter = set; }

        }
        public AssertionLogStream(NamedCounterAssertion increment, NamedCounterAssertion set, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (increment == null && set == null) { throw new ArgumentNullException("increment and set"); }

            initializeDelegates(_onError = onError);

            if (increment != null) { _incrementNamedCounter = increment; }
            if (set != null) { _setNamedCounter = set; }
        }

        void initializeDelegates(Action onError) {
            _message = (t, e, u, m, f) => onError();
            _incrementRawCounter = (t, v) => onError();
            _setRawCounter = (t, v) => onError();
            _incrementNamedCounter = (c, v) => onError();
            _setNamedCounter = (c, v) => onError();
        }

        Action _onError;
        LoggingAssertion _message;
        CounterAssertion _incrementRawCounter, _setRawCounter;
        NamedCounterAssertion _incrementNamedCounter, _setNamedCounter;
        

        public void Test(ILogToken token, Exception error, int? uniqueness, string message, object[] fmt)
        {
            _message(token, error, uniqueness, message, fmt);
        }

        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(t, e, u, m, f));
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            // the current implementation at least, calls this method constantly to facilitate proper
            // buffering of context data which would make assertions for these methods unrealistic
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            // the current implementation at least, calls this method constantly to facilitate proper
            // buffering of context data which would make assertions for these methods unrealistic
        }
        
        public void IncrementCounterBy(ICounterToken ct, long value)
        {
            var nct = ct as INamedCounterToken;
            if (nct != null) {
              _incrementNamedCounter(nct, value);
            } else {
              _incrementRawCounter(ct, value);
            }
        }

        public void SetCounter(ICounterToken ct, long value)
        {
            var nct = ct as INamedCounterToken;
            if (nct != null ) {
              _setNamedCounter(nct, value);
            } else {
              _setRawCounter(ct, value);
            }
        }

        public void Dispose()
        {
        }
    }
}
