using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System.Management.Automation;

namespace ExpressiveLogging.PowershellV5Logging
{
    /// <summary>
    /// This log stream allows emitting log messages through the powershell logging channels.
    /// It is not thread safe. It may only be used from the thread that the powershell commandlet
    /// has been executed in. It also may only be used from inside
    /// the BeginProcessing() / ProcessRecords() / EndProcessing()
    /// methods.
    /// </summary>
    public class CmdletLogStream : ILogStream
    {
        Cmdlet _target;
        public CmdletLogStream(Cmdlet target)
        {
            _target = target;
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f), e), t.Symbol + "(" + u + ")", ErrorCategory.NotSpecified, null));
            });
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f)), t.Symbol, ErrorCategory.NotSpecified, null));
            });
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f)), t.Symbol + "(" + u + ")", ErrorCategory.NotSpecified, null));
            });
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f), e), t.Symbol, ErrorCategory.NotSpecified, null));
            });
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteInformation(string.Format(m, f), null);
            });
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteInformation(string.Format(m, f), new[] { "uniquenessCode(" + u + ")" });
            });
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteInformation(string.Format(m, f), new[] { "uniquenessCode(" + u + ")" });
                _target.WriteInformation(e, null);
            });
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {
                _target.WriteInformation(string.Format(m, f), null);
                _target.WriteInformation(e, null);
            });
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteDebug(string.Format(m, f));
            });
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteDebug(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteDebug(string.Format(m, f) + Environment.NewLine);
            });
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {
                _target.WriteDebug(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f)), t.Symbol, ErrorCategory.NotSpecified, null));
            });
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f), e), t.Symbol + "(" + u + ")", ErrorCategory.NotSpecified, null));
            });
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f)), t.Symbol + "(" + u + ")", ErrorCategory.NotSpecified, null));
            });
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {                
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f), e), t.Symbol, ErrorCategory.NotSpecified, null));
            });
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteVerbose(string.Format(m, f));
            });
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteVerbose(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteVerbose(string.Format(m, f));
            });
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {
                _target.WriteVerbose(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }
        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) =>
            {
                _target.WriteWarning(string.Format(m, f));
            });
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteWarning(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) =>
            {
                _target.WriteWarning(string.Format(m, f));
            });
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) =>
            {
                _target.WriteWarning(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }


        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }

        public void Dispose()
        {
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
    }
}
