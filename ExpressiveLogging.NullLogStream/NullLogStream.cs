using ExpressiveLogging;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressiveLogging.NullLogging
{
    /// <summary>
    /// Implements the ILogger interface but provides no functionality
    /// </summary>
    public class NullLogStream : ILogStream
    {
        public void Dispose()
        {
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            // don't have anything we need to attach
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }
        
        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }
        

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }
        
        
        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }
        
        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }
        

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }
        

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
        }

        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
        }
        public void SetCounterValue(IRawCounterToken ct, long value)
        {
        }      

        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
        }
        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
        }
    }
}
