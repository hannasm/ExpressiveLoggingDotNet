using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;

namespace ExpressiveLogging.StreamFormatters
{
    public class EmptyFormatMessageFixer : ILogStream
    {
        ILogStream _inner;

        public EmptyFormatMessageFixer(ILogStream inner)
        {
            _inner = inner;
        }

        public string fixMessage(string msg, object[] fmt)
        {
            if (fmt == null || fmt.Length == 0)
            {
                return "{0}";
            }
            else
            {
                return msg;
            }
        }
        public object[] fixFormat(string msg, object[] fmt)
        {
            if (fmt == null || fmt.Length == 0)
            {
                return new object[] { msg };
            }
            else
            {
                return fmt;
            }

        }
        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Alert(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Alert(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Alert(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Alert(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Audit(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Audit(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Audit(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Audit(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Debug(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Debug(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Debug(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Debug(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Error(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Error(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Error(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Error(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Info(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Info(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Info(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Info(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Warning(t, z => z(u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Warning(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Warning(t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }
        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Warning(t, z => z(e, fixMessage(m, f), fixFormat(m, f))));
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.EndScope(ctx, t, z => z(fixMessage(m, f), fixFormat(m, f))));
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.BeginScope(ctx, t, z=>z(fixMessage(m,f), fixFormat(m,f))));
        }

        public void Dispose()
        {
            _inner.Dispose();
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
    }
}
