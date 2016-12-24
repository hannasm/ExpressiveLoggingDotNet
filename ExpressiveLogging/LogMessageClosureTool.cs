using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    public class LogMessageClosureTool
    {
        /// <summary>
        /// Evaluate the user delegate, and pull the
        /// resulting set of logging parameters into a new 
        /// delegate which can be executed on each of the composed loggers
        /// </summary>
        public static Action<LogExceptionMessage> CreateMessageClosure(Action<LogExceptionMessage> msgBuilder)
        {
            List<LogExceptionMessageClosure> closure = new List<LogExceptionMessageClosure>();
            LogExceptionMessage closureFunc = (pExc, pMsg, pFmt) => closure.Add(new LogExceptionMessageClosure { msg = pMsg, exc = pExc, fmt = pFmt, });
            msgBuilder(closureFunc);
            return (lem) =>
            {
                foreach (var cls in closure)
                {
                    lem(cls.exc, cls.msg, cls.fmt);
                }
            };
        }
        private class LogExceptionMessageClosure
        {
            public string msg;
            public Exception exc;
            public object[] fmt;
        }

        /// <summary>
        /// Evaluate the user delegate, and pull the
        /// resulting set of logging parameters into a new 
        /// delegate which can be executed on each of the composed loggers
        /// </summary>
        public static Action<LogFormatMessage> CreateMessageClosure(Action<LogFormatMessage> msgBuilder)
        {
            List<LogFormatMessageClosure> closure = new List<LogFormatMessageClosure>();
            LogFormatMessage closureFunc = (pMsg, pFmt) => closure.Add(new LogFormatMessageClosure { msg = pMsg, fmt = pFmt });
            msgBuilder(closureFunc);
            return (lem) =>
            {
                foreach (var cls in closure)
                {
                    lem(cls.msg, cls.fmt);
                }
            };
        }
        private class LogFormatMessageClosure
        {
            public string msg;
            public object[] fmt;
        }

        /// <summary>
        /// Evaluate the user delegate, and pull the
        /// resulting set of logging parameters into a new 
        /// delegate which can be executed on each of the composed loggers
        /// </summary>
        public static Action<LogExceptionMessageWithCustomUniqueness> CreateMessageClosure(Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            List<LogExceptionMessageWithCustomUniquenessClosure> closure = new List<LogExceptionMessageWithCustomUniquenessClosure>();
            LogExceptionMessageWithCustomUniqueness closureFunc = (pExc, pUnq, pMsg, pFmt) => closure.Add(new LogExceptionMessageWithCustomUniquenessClosure { uniqueness = pUnq,  msg = pMsg, exc = pExc, fmt = pFmt });
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
            public int uniqueness;
        }
        /// <summary>
        /// Evaluate the user delegate, and pull the
        /// resulting set of logging parameters into a new 
        /// delegate which can be executed on each of the composed loggers
        /// </summary>
        public static Action<LogFormatMessageWithCustomUniqueness> CreateMessageClosure(Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            List<LogFormatMessageWithCustomUniquenessClosure> closure = new List<LogFormatMessageWithCustomUniquenessClosure>();
            LogFormatMessageWithCustomUniqueness closureFunc = (pUnq, pMsg, pFmt) => closure.Add(new LogFormatMessageWithCustomUniquenessClosure { uniqueness = pUnq, msg = pMsg, fmt = pFmt });
            msgBuilder(closureFunc);
            return (lem) =>
            {
                foreach (var cls in closure)
                {
                    lem(cls.uniqueness, cls.msg, cls.fmt);
                }
            };
        }
        private class LogFormatMessageWithCustomUniquenessClosure
        {
            public string msg;
            public object[] fmt;
            public int uniqueness;
        }
    }
}
