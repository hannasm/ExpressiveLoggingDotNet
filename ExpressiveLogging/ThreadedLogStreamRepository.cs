using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    public class ThreadedLogStreamRepository : IDisposable
    {
        readonly object _lock = new object();
        readonly Func<ILogStream> _factory;
        ThreadLocal<ILogStream> _log = new ThreadLocal<ILogStream>();

        public ThreadedLogStreamRepository(Func<ILogStream> factory)
        {
            _factory = factory;
        }

        public ILogStream GetLogger()
        {
            if (_log == null)
            {
                throw new ObjectDisposedException(typeof(ThreadedLogStreamRepository).FullName);
            }

            if (_log.IsValueCreated) { return _log.Value; }

            lock (_lock)
            {
                if (!_log.IsValueCreated)
                {
                    _log.Value = _factory();
                }
                return _log.Value;
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_log != null)
                {
                    var log = _log;
                    _log = null;
                    foreach (var val in log.Values)
                    {
                        val.Dispose();
                    }
                    log.Dispose();
                }
            }
        }
    }
}
