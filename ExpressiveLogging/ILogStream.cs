using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressiveLogging
{
    public delegate void LogExceptionMessage(Exception e, string message, params object[] format);
    public delegate void LogFormatMessage(string message, params object[] format);

    public delegate void LogExceptionMessageWithCustomUniqueness(Exception e, int uniqueness, string message, params object[] format);
    public delegate void LogFormatMessageWithCustomUniqueness(int uniqueness, string message, params object[] format);

    public interface ILogStream : IDisposable
    {
        void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters);
        void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder);
        void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder);
        

        void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        

        void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Info(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        
        void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        

        void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        

        void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Error(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        

        void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder);
        void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder);

        void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder);
        void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder);
        
        void IncrementCounterBy(IRawCounterToken ct, long value);
        void SetCounterValue(IRawCounterToken ct, long value);
        
        void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value);
        void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value);

    }    
}
