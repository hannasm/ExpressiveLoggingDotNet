using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    public static class LogManager
    {
        public static LogScopeBuilder BuildScope(
            ILogToken lt,
            Action<LogFormatMessage> beginMessage,
            Action<LogFormatMessage> endMessage
        )
        {
            return new LogScopeBuilder(lt, beginMessage, endMessage);
        }
        public static LogScopeBuilder BuildScope(
            ILogToken lt
        )
        {
            return new LogScopeBuilder(lt);
        }
        public static ILogContextScope NewScope(
            ILogStream log,
            ILogToken lt,
            Action<LogFormatMessage> beginMessage,
            Action<LogFormatMessage> endMessage
        )
        {
            return new LogScopeBuilder(lt, beginMessage, endMessage).NewScope(log);
        }
        public static ILogContextScope NewScope(
            ILogStream log,
            ILogToken lt
        )
        {
            return new LogScopeBuilder(lt).NewScope(log);
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(Exception error, string message)
        {
            return GenerateUniquenessCode(message) * GenerateUniquenessCode(error);
        }

        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(string message)
        {
            if (message == null) { return 27; }
            return message.GetHashCode();
        }

        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(string message, params object[] args)
        {
            return string.Format(message, args).GetHashCode();
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(Exception exc)
        {
            if (exc == null) { return 37; }
            return exc.ToString().GetHashCode();
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(ILogToken lt)
        {
            return 97; // messages are already 'unique' per log token because the log token name is captured
        }

        public static ILogToken GetToken()
        {
            StackFrame frame = new StackFrame(1, false);
            return new LogToken(frame.GetMethod().DeclaringType);
        }
        public static ILogToken GetToken(ILogToken token, string name)
        {
            return new LogToken(string.Concat(token.Name, ".", name));
        }
        public static ILogToken GetToken(string name)
        {
            return new LogToken(name);
        }

        public static IRawCounterToken CreateRawCounterToken(string name)
        {
            return new RawCounterToken(name);
        }
        public static INamedCounterToken CreateNamedCounterToken(string name)
        {
            return new NamedCounterToken(name);
        }
    }
}
