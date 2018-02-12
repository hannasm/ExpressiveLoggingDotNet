using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.TextWriterLogging
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
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
        }
        
        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }


        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }



        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((u, m, f) => _stream.WriteLine(m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder((m, f) => _stream.WriteLine(m, f));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => { _stream.WriteLine(m, f); _stream.WriteLine(e.ToString()); });
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

        public void Dispose()
        {
        }
    }
}
