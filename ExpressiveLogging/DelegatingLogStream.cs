using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    /// <summary>
    /// Barebones class which simply forwards calls to inner stream. Only really useful as a base class for implementing stream formatters
    /// </summary>
    public abstract class DelegatingLogStream : ILogStream
    {
        protected ILogStream _inner;

        public DelegatingLogStream(ILogStream inner)
        {
            _inner = inner;
        }

        public virtual void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            _inner.Write(t, msgBuilder);
        }

        public virtual void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }
        public virtual void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnDetachScopeParameters(lt, parameters);
        }

        public virtual void IncrementCounterBy(ICounterToken ct, long value)
        {
            _inner.IncrementCounterBy(ct, value);
        }
        
        public virtual void SetCounter(ICounterToken ct, long value)
        {
            _inner.SetCounter(ct, value);
        }
        
        public virtual void Dispose()
        {
            _inner.Dispose();
        }
    }
}
