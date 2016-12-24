using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Context
{
    /// <summary>
    /// Use LogManager.BuildScope if you need an instance of this class
    /// </summary>
    public class LogScopeBuilder
    {
        private readonly ILogToken _lt;
        private readonly Action<LogFormatMessage> _beginMessage = (m) => m("Begin scope");
        private readonly Action<LogFormatMessage> _endMessage = (m) => m("End scope");
        private readonly List<KeyValuePair<string, object>> _context = new List<KeyValuePair<string, object>>();

        internal LogScopeBuilder(
            ILogToken lt,
            Action<LogFormatMessage> beginMessage,
            Action<LogFormatMessage> endMessage)
        {
            _lt = lt;
            _beginMessage = beginMessage;
            _endMessage = endMessage;
        }
        internal LogScopeBuilder(ILogToken lt)
        {
            _lt = lt;
        }

        public LogScopeBuilder AddContext(string key, string value)
        {
            _context.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public ILogContextScope NewScope(ILogStream log)
        {
            return new LogScope(log, _lt, _beginMessage, _endMessage, _context);
        }

        private class LogScope : ILogContextScope
        {
            private readonly ILoggingContext _context;
            private readonly ILogStream _log;
            private readonly ILogToken _lt;
            private readonly Action<LogFormatMessage> _endMessage;
            public LogScope(
                ILogStream log,
                ILogToken lt,
                Action<LogFormatMessage> beginMsg,
                Action<LogFormatMessage> endMsg,
                List<KeyValuePair<string, object>> contextData)
            {
                _log = log;
                _lt = lt;
                _endMessage = endMsg;

                // provide loggers chance to attach data if needed
                _log.OnAttachScopeParameters(lt, contextData);

                // push context data
                _context = LoggingCallContextStore.Push(contextData);

                // write event signaling scope creation
                _log.BeginScope(_context, _lt, beginMsg);
            }

            public void Dispose()
            {
                // write event signaling scope destruction
                _log.EndScope(_context, _lt, _endMessage);

                // clear scope data
                LoggingCallContextStore.Pop(_context);
            }
        }
    }
}
