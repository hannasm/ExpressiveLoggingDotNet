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
    /// Turns logging calls into a human-readable text-formatted message that is appropriate for output mechanisms such as flat files or the console.
    /// </summary>
    public class DefaultTextLogStreamFormatter : DelegatingLogStream
    {
        public DefaultTextLogStreamFormatter(ILogStream inner) : base (inner)
        {
        }

        string formatMessage(ILogToken lt, int? hash, string level, string msg, object[] fmt, Exception eError)
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

        object[] formatArgs(ILogToken lt, int? hash, string level, string msg, object[] fmt, Exception eError)
        {
            int cnt = fmt.Length;
            int exc = 0;
            if (eError != null) { exc = 1; }

            Array.Resize(ref fmt, fmt.Length + 4 + exc);
            fmt[cnt++] = DateTime.Now;
            fmt[cnt++] = level;
            fmt[cnt++] = lt.Name;
            fmt[cnt++] = hash ?? 0;
            if (eError != null) { fmt[cnt++] = eError; }

            return fmt;
        }


        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Write(t, log => log(e, u, formatMessage(t, u, "ALERT", m, f, e), formatArgs(t, u, "ALERT", m, f, e))));
        }
    }
}
