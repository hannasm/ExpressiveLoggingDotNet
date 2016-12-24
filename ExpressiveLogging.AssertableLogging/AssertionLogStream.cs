using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;

namespace ExpressiveLogging.AssertableLogging
{
    public class AssertionLogStream : ILogStream
    {
        public AssertionLogStream(LoggingAssertion message, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (message == null) { throw new ArgumentNullException("message"); }

            initializeDelgates(onError);

            _message = message;
        }

        public AssertionLogStream(BeginScopeAssertion scope, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (scope == null) { throw new ArgumentNullException("scope"); }

            initializeDelgates(onError);

            _beginScope = scope;
        }
        public AssertionLogStream(EndScopeAssertion scope, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (scope == null) { throw new ArgumentNullException("scope"); }

            initializeDelgates(onError);

            _endScope = scope;
        }
        public AssertionLogStream(RawCounterAssertion increment, RawCounterAssertion set, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (increment == null && set == null) { throw new ArgumentNullException("increment and set"); }

            initializeDelgates(onError);

            if (increment != null) { _incrementRawCounter = increment; }
            if (set != null) { _setRawCounter = set; }

        }
        public AssertionLogStream(NamedCounterAssertion increment, NamedCounterAssertion set, Action onError)
        {
            if (onError == null) { throw new ArgumentNullException("onError"); }
            if (increment == null && set == null) { throw new ArgumentNullException("increment and set"); }

            initializeDelgates(onError);

            if (increment != null) { _incrementNamedCounter = increment; }
            if (set != null) { _setNamedCounter = set; }
        }

        void initializeDelgates(Action onError) {
            _message = (l, t, e, u, m, f) => onError();
            _beginScope = (c, t, p) => onError();
            _endScope = (c, t, p) => onError();
            _incrementRawCounter = (t, v) => onError();
            _setRawCounter = (t, v) => onError();
            _incrementNamedCounter = (t, c, v) => onError();
            _setNamedCounter = (t, c, v) => onError();
        }

        Action _onError;
        LoggingAssertion _message;
        BeginScopeAssertion _beginScope;
        EndScopeAssertion _endScope;
        RawCounterAssertion _incrementRawCounter, _setRawCounter;
        NamedCounterAssertion _incrementNamedCounter, _setNamedCounter;
        
        public delegate void BeginScopeAssertion(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder);
        public delegate void EndScopeAssertion(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder);
        public delegate void LoggingAssertion(LoggingLevel level, ILogToken token, Exception error, int? uniqueness, string message, object[] fmt);
        public delegate void RawCounterAssertion(IRawCounterToken ct, long value);
        public delegate void NamedCounterAssertion(ILogToken lt, INamedCounterToken ct, long value);

        public void Test(LoggingLevel level, ILogToken token, Exception error, int? uniqueness, string message, object[] fmt)
        {
            var tst = _message;
            initializeDelgates(_onError);

            tst(level, token, error, uniqueness, message, fmt);
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.ALERT, t, e, u, m, f));
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.ALERT, t, null, null, m, f));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.ALERT, t, null, u, m, f));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.ALERT, t, e, null, m, f));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.AUDIT, t, null, null, m, f));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.AUDIT, t, null, u, m, f));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.AUDIT, t, e, u, m, f));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.AUDIT, t, e, null, m, f));
        }


        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.DEBUG, t, null, u, m, f));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.DEBUG, t, e, u, m, f));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.DEBUG, t, null, null, m, f));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.DEBUG, t, e, null, m, f));
        }


        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.ERROR, t, null, null, m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.ERROR, t, e, u, m, f));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.ERROR, t, null, u, m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.ERROR, t, e, null, m, f));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.INFO, t, null, u, m, f));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.INFO, t, e, u, m, f));
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.INFO, t, null, null, m, f));
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.INFO, t, e, null, m, f));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => Test(LoggingLevel.WARNING, t, null, u, m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => Test(LoggingLevel.WARNING, t, e, u, m, f));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => Test(LoggingLevel.WARNING, t, null, null, m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => Test(LoggingLevel.WARNING, t, e, null, m, f));
        }


        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            // the current implementation at least, calls this method constantly to facilitate proper
            // buffering of context data which would make assertions for these methods unrealistic
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            var tst = _beginScope;
            initializeDelgates(_onError);
            tst(ctx, t, msgBuilder);
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            var tst = _endScope;
            initializeDelgates(_onError);
            tst(ctx, t, msgBuilder);
        }
        
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            var tst = _incrementRawCounter;
            initializeDelgates(_onError);
            tst(ct, value);
        }

        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            var tst = _incrementNamedCounter;
            initializeDelgates(_onError);
            tst(lt, ct, value);
        }

        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            var tst = _setRawCounter;
            initializeDelgates(_onError);
            tst(ct, value);
        }

        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            var tst = _setNamedCounter;
            initializeDelgates(_onError);

            tst(lt, ct, value);
        }

        public void Dispose()
        {
        }
    }
}
