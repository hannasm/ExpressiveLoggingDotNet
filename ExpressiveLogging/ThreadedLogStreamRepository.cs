using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public class ThreadedLogStreamRepository : ILogStreamRepository
    {
        readonly object _lock = new object();
        readonly Func<ProxyLogStream, ILogStream> _factory;
        ThreadLocal<ILogStream> _log = new ThreadLocal<ILogStream>();
        ThreadLocal<ProxyLogStream> _proxy = new ThreadLocal<ProxyLogStream>();

        public ThreadedLogStreamRepository(Func<ProxyLogStream, ILogStream> factory)
        {
            _factory = factory;
        }

        public ProxyLogStream GetProxy()
        {
            if (_proxy == null)
            {
                throw new ObjectDisposedException(typeof(ThreadedLogStreamRepository).FullName);
            }

            if (_proxy.IsValueCreated) { return _proxy.Value; }

            lock (_lock)
            {
                if (!_proxy.IsValueCreated)
                {
                    _proxy.Value = new ProxyLogStream();
                }
                return _proxy.Value;
            }
        }
        public ILogStream GetLogger() {
          return TryGetLogger(true);
        }
        public ILogStream TryGetLogger(bool initialize)
        {
            if (_log == null)
            {
                throw new ObjectDisposedException(typeof(ThreadedLogStreamRepository).FullName);
            }

            if (_log.IsValueCreated) { return _log.Value; }
            else if (!initialize) { return null; }

            lock (_lock)
            {
                if (!_log.IsValueCreated)
                {
                    _log.Value = _factory(GetProxy());
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
                if (_proxy != null) {
                  var proxy = _proxy;
                  _proxy = null;
                  foreach (var val in proxy.Values) {
                    val.Dispose();
                  }
                  proxy.Dispose();
                }
            }
        }
    }
}
