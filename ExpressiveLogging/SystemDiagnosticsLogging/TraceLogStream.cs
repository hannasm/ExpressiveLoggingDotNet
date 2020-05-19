using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public class TraceLogStream : ILogStream
    {
        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
        }

        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { 
                if (m != null) {System.Diagnostics.Trace.WriteLine(string.Format(m,f));}
               if (e != null) { System.Diagnostics.Trace.WriteLine(e.ToString()); }});
        }

        public void IncrementCounterBy(ICounterToken ct, long value)
        {
        }

        public void SetCounter(ICounterToken ct, long value)
        {
        }

        public void Dispose()
        {
        }
    }
}
