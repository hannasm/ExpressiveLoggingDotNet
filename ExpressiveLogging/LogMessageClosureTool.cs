using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public class LogMessageClosureTool
    {
        /// <summary>
        /// Evaluate the user delegate, and pull the
        /// resulting set of logging parameters into a new 
        /// delegate which can be executed on each of the composed loggers
        /// </summary>
        public static Action<CompleteLogMessage> CreateMessageClosure(Action<CompleteLogMessage> msgBuilder)
        {
            List<LogExceptionMessageWithCustomUniquenessClosure> closure = new List<LogExceptionMessageWithCustomUniquenessClosure>();
            CompleteLogMessage closureFunc = (pExc, pUnq, pMsg, pFmt) => closure.Add(new LogExceptionMessageWithCustomUniquenessClosure { uniqueness = pUnq,  msg = pMsg, exc = pExc, fmt = pFmt });
            msgBuilder(closureFunc);
            return (lem) =>
            {
                foreach (var cls in closure)
                {
                    lem(cls.exc, cls.uniqueness, cls.msg, cls.fmt);
                }
            };
        }
        private class LogExceptionMessageWithCustomUniquenessClosure
        {
            public string msg;
            public Exception exc;
            public object[] fmt;
            public int? uniqueness;
        }
    }
}
