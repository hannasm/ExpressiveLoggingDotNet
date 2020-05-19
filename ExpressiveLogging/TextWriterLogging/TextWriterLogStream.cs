using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public class TextWriterLogStream : ILogStream
    {
        TextWriter _stream;

        public TextWriterLogStream(TextWriter stream)
        {
            _stream = stream;
        }

        string formatMessage(ILogToken lt, int hash, string level, string msg, object[] fmt)
        {
            return formatMessage(lt, hash, level, msg, fmt, null);
        }
        string formatMessage(ILogToken lt, int hash, string level, string msg, object[] fmt, Exception eError)
        {
            return string.Format("{3}-{4}-{0}-({1}): {2}", lt.Name, hash, string.Format(msg, fmt), DateTime.Now, level);
        }

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
              if (m != null) {
                _stream.WriteLine(m, f);
              }
              if (e != null) { _stream.WriteLine(e.ToString()); }  
            });
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
