using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.V3.Context;

namespace ExpressiveLogging.V3
{
    public class ExceptionHandlingLogStream : DelegatingLogStream
    {
        static ILogToken _lt = LogManager.GetToken();
        Action<ILogStream, Exception> HandleError;
        Action<ILogStream, Exception, Exception> LastResort;

        public ExceptionHandlingLogStream(ILogStream inner)
            :this(
                 inner,
                 (l, e) => LogManager.Warning.Write(_lt, m => m(e, "Exception ocurred while writing log message")),
                 (l, e1, e2) => { throw new AggregateException(e1, e2); }
             )
        {
        }
        public ExceptionHandlingLogStream(
            ILogStream inner,
            Action<ILogStream, Exception> handler,
            Action<ILogStream, Exception, Exception> lastResort
        ) : base(inner)
        {
            HandleError = handler;
            LastResort = lastResort;
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            try
            {
                _inner.Write(t, msgBuilder);
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

        public override void Dispose()
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

        public override void IncrementCounterBy(ICounterToken ct, long value)
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

        public override void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
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
        public override void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            try
            {
              _inner.OnDetachScopeParameters(lt, parameters);
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

        public override void SetCounter(ICounterToken ct, long value)
        {
            try
            {
                _inner.SetCounter(ct, value);
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
