using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;

namespace ExpressiveLogging.StreamFormatters
{
    public class ExceptionHandlingLogStream : ILogStream
    {
        static ILogToken _lt = LogManager.GetToken();
        ILogStream _inner;
        Action<ILogStream, Exception> HandleError;
        Action<ILogStream, Exception, Exception> LastResort;

        public ExceptionHandlingLogStream(ILogStream inner)
            :this(
                 inner,
                 (l, e) => l.Alert(_lt, m => m(e, "Exception ocurred while writing log message")),
                 (l, e1, e2) => { throw new AggregateException(e1, e2); }
             )
        {
        }
        public ExceptionHandlingLogStream(
            ILogStream inner,
            Action<ILogStream, Exception> handler,
            Action<ILogStream, Exception, Exception> lastResort
        )
        {
            _inner = inner;
            HandleError = handler;
            LastResort = lastResort;
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Alert(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Alert(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Alert(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Alert(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Audit(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Audit(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Audit(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Audit(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.BeginScope(ctx, t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Debug(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Debug(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Debug(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Debug(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                _inner.Dispose();
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.EndScope(ctx, t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Error(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Error(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Error(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Error(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            try
            {
                _inner.IncrementCounterBy(ct, value);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            try
            {
                _inner.IncrementCounterBy(lt, ct, value);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Info(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Info(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Info(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Info(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            try
            {
                _inner.OnAttachScopeParameters(lt, parameters);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            try
            {
                _inner.SetCounterValue(ct, value);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            try
            {
                _inner.SetCounterValue(lt, ct, value);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Warning(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            try
            {
                _inner.Warning(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            try
            {
                _inner.Warning(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            try
            {
                _inner.Warning(t, msgBuilder);
            }
            catch (Exception eError)
            {
                try
                {
                    HandleError(this, eError);
                }
                catch (Exception eError2)
                {
                    LastResort(this, eError, eError2);
                }
            }
        }
    }
}
