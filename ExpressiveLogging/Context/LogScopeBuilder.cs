using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Context
{
    /// <summary>
    /// Use LogManager.BuildScope if you need an instance of this class
    /// </summary>
    public class LogScopeBuilder
    {
        private readonly ILogToken _lt;
        private readonly List<KeyValuePair<string, object>> _context = new List<KeyValuePair<string, object>>();

        internal LogScopeBuilder(ILogToken lt)
        {
            _lt = lt;
        }

        public LogScopeBuilder AddContext(string key, string value)
        {
            _context.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public ILogContextScope NewScope()
        {
            var streams = StaticLogRepository.GetTokens().Select(key=>StaticLogRepository.GetRepository(key).GetLogger());
            return NewScope(streams);
        }
        public ILogContextScope NewScope(params ILogStream[] streams)
        {
          return NewScope((IEnumerable<ILogStream>)streams);
        }
        public ILogContextScope NewScope(IEnumerable<ILogStream> streams)
        {
            return new LogScope(streams, _lt, _context);
        }

        private class LogScope : ILogContextScope
        {
            readonly ILoggingContext _context;
            readonly IEnumerable<ILogStream> _streams;
            readonly ILogToken _lt;
            readonly List<KeyValuePair<string, object>> _contextData;

            public LogScope(
                IEnumerable<ILogStream> streams,
                ILogToken lt,
                List<KeyValuePair<string, object>> contextData)
            {

                foreach (var log in streams) {
                  // provide loggers chance to attach data if needed
                  log.OnAttachScopeParameters(lt, contextData);
                }

                // push context data
                _context = LoggingCallContextStore.Push(contextData);
                _streams = streams;
                _lt = lt;
                _contextData = contextData;
            }

            public void Dispose()
            {
                foreach (var log in _streams) {
                  // provide loggers chance to attach data if needed
                  log.OnDetachScopeParameters(_lt, _contextData);
                }
                // clear scope data
                LoggingCallContextStore.Pop(_context);
            }
        }
    }
}
