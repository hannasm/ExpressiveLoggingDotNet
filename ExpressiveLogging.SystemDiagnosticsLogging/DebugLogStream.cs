#define DEBUG // we need debug here so that the calls are not removed by conditional compilation attribution on the System.Diagnostics.Debug class

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;

namespace ExpressiveLogging.SystemDiagnosticsLogging
{

    public class DebugLogStream : ILogStream
    {
        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }


        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }



        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => System.Diagnostics.Debug.WriteLine(m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { System.Diagnostics.Debug.WriteLine(m, f); System.Diagnostics.Debug.WriteLine(e.ToString()); });
        }

        
        
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
        }

        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
        }

        public void SetCounterValue(IRawCounterToken ct, long value)
        {
        }

        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
        }

        public void Dispose()
        {
        }
    }
}
