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
    /// Intercepts any logging calls that are missing a uniqueness code, and generates a uniqueness code using appropriate default heuristics
    /// </summary>
    public class UniquenessCodeGeneratorLogStream : ILogStream
    {
        ILogStream _inner;
        public UniquenessCodeGeneratorLogStream(ILogStream inner)
        {
            _inner = inner;
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Alert(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Alert(t, log => log(u,m,f)));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Alert(t, log => log(e,u,m,f)));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Alert(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f)));
        }
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Audit(t, log => log(e,u,m,f)));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Audit(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Audit(t, log => log(u, m, f)));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Audit(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f, e)));
        }


        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Debug(t, log => log(e, u, m, f)));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Debug(t, log => log(u, m, f)));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Debug(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Debug(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f)));
        }



        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Error(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Error(t, log => log(e, u, m, f)));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Error(t, log => log(u, m, f)));
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Error(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f)));
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Info(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Info(t, log => log(u, m, f)));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Info(t, log => log(e, u, m, f)));
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Info(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f)));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _inner.Warning(t, log => log(u, m, f)));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Warning(t, log => log(e, u, m, f)));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _inner.Warning(t, log => log(LogManager.GenerateUniquenessCode(m), m, f)));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Warning(t, log => log(e, LogManager.GenerateUniquenessCode(e, m), m, f)));
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
