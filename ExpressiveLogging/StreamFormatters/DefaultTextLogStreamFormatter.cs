using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.StreamFormatters
{
    /// <summary>
    /// Turns logging calls into a human-readable text-formatted message that is appropriate for output mechanisms such as flat files or the console.
    /// </summary>
    public class DefaultTextLogStreamFormatter : ILogStream
    {
        ILogStream _inner;

        public DefaultTextLogStreamFormatter(ILogStream inner)
        {
            _inner = inner;
        }

        string formatMessage(ILogToken lt, int hash, string level, string msg, object[] fmt)
        {
            return formatMessage(lt, hash, level, msg, fmt, null);
        }
        string formatMessage(ILogToken lt, int hash, string level, string msg, object[] fmt, Exception eError)
        {
            int cnt = fmt.Length;
            int size = 4;
            if (eError != null) { size += 1; }

            StringBuilder newMsg = new StringBuilder(msg.Length + size * 4);
            newMsg.Append("{");
            newMsg.Append(cnt++);
            newMsg.Append("}-{");
            newMsg.Append(cnt++);
            newMsg.Append("}-{");
            newMsg.Append(cnt++);
            newMsg.Append("}-({");
            newMsg.Append(cnt++);
            newMsg.Append("}): ");
            newMsg.Append(msg);
            if (eError != null)
            {
                newMsg.AppendLine();
                newMsg.Append("  ");
                newMsg.Append("{");
                newMsg.Append(cnt++);
                newMsg.Append("}");
            }

            return newMsg.ToString();
        }

        object[] formatArgs(ILogToken lt, int hash, string level, string msg, object[] fmt)
        {
            return formatArgs(lt, hash, level, msg, fmt, null);
        }
        object[] formatArgs(ILogToken lt, int hash, string level, string msg, object[] fmt, Exception eError)
        {
            int cnt = fmt.Length;
            int exc = 0;
            if (eError != null) { exc = 1; }

            Array.Resize(ref fmt, fmt.Length + 4 + exc);
            fmt[cnt++] = DateTime.Now;
            fmt[cnt++] = level;
            fmt[cnt++] = lt.Name;
            fmt[cnt++] = hash;
            if (eError != null) { fmt[cnt++] = eError; }

            return fmt;
        }


        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Alert(t, log => log(formatMessage(t, -1, "ALERT", m, f), formatArgs(t, -1, "ALERT", m, f))));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Alert(t, log => log(formatMessage(t, u, "ALERT", m, f), formatArgs(t, u, "ALERT", m, f))));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Alert(t, log => log(formatMessage(t, u, "ALERT", m, f, e), formatArgs(t, u, "ALERT", m, f, e))));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Alert(t, log => log(formatMessage(t, -1, "ALERT", m, f, e), formatArgs(t, -1, "ALERT", m, f, e))));
        }
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Audit(t, log => log(formatMessage(t, u, "AUDIT", m, f, e), formatArgs(t, u, "AUDIT", m, f, e))));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Audit(t, log => log(formatMessage(t, -1, "AUDIT", m, f), formatArgs(t, -1, "AUDIT", m, f))));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Audit(t, log => log(formatMessage(t, u, "AUDIT", m, f), formatArgs(t, u, "AUDIT", m, f))));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Audit(t, log => log(formatMessage(t, -1, "AUDIT", m, f, e), formatArgs(t, -1, "AUDIT", m, f, e))));
        }


        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Debug(t, log => log(formatMessage(t, u, "DEBUG", m, f, e), formatArgs(t, u, "DEBUG", m, f, e))));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Debug(t, log => log(formatMessage(t, u, "DEBUG", m, f), formatArgs(t, u, "DEBUG", m, f))));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Debug(t, log => log(formatMessage(t, -1, "DEBUG", m, f), formatArgs(t, -1, "DEBUG", m, f))));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Debug(t, log => log(formatMessage(t, -1, "DEBUG", m, f, e), formatArgs(t, -1, "DEBUG", m, f, e))));
        }



        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Error(t, log => log(formatMessage(t, -1, "ERROR", m, f), formatArgs(t, -1, "ERROR", m, f))));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Error(t, log => log(formatMessage(t, u, "ERROR", m, f, e), formatArgs(t, u, "ERROR", m, f, e))));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Error(t, log => log(formatMessage(t, u, "ERROR", m, f), formatArgs(t, u, "ERROR", m, f))));
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Error(t, log => log(formatMessage(t, -1, "ERROR", m, f, e), formatArgs(t, -1, "ERROR", m, f, e))));
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Info(t, log => log(formatMessage(t, -1, "INFO", m, f), formatArgs(t, -1, "INFO", m, f))));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Info(t, log => log(formatMessage(t, u, "INFO", m, f), formatArgs(t, u, "INFO", m, f))));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Info(t, log => log(formatMessage(t, u, "INFO", m, f, e), formatArgs(t, u, "INFO", m, f, e))));
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Info(t, log => log(formatMessage(t, -1, "INFO", m, f, e), formatArgs(t, -1, "INFO", m, f, e))));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Warning(t, log => log(formatMessage(t, u, "WARNING", m, f), formatArgs(t, u, "WARNING", m, f))));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Warning(t, log => log(formatMessage(t, u, "WARNING", m, f, e), formatArgs(t, u, "WARNING", m, f, e))));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Warning(t, log => log(formatMessage(t, -1, "WARNING", m, f), formatArgs(t, -1, "WARNING", m, f))));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Warning(t, log => log(formatMessage(t, -1, "WARNING", m, f, e), formatArgs(t, -1, "WARNING", m, f, e))));
        }


        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.BeginScope(ctx, t, msgBuilder);
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.EndScope(ctx, t, msgBuilder);
        }
        
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            _inner.IncrementCounterBy(ct, value);
        }
        
        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            _inner.SetCounterValue(ct, value);
        }
        
        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            _inner.IncrementCounterBy(lt, ct, value);
        }
        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            _inner.SetCounterValue(lt, ct, value);
        }

        public void Dispose()
        {
            _inner.Dispose();
        }
    }
}
