using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.V3.Context;

namespace ExpressiveLogging.V3
{
    public class EmptyFormatMessageFixer : DelegatingLogStream
    {
        public EmptyFormatMessageFixer(ILogStream inner) : base(inner)
        {
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
        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Write(t, z => z(e, u, fixMessage(m, f), fixFormat(m, f))));
        }
    }
}
