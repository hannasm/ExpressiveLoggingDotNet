using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ExpressiveLogging.V3
{
    public static class StaticLogRepository
    {
        static Func<ILogStreamToken, ILogStreamToken> _guidTransform = null;
        static Func<ILogStreamToken, ThreadedLogStreamRepository> _builder = n=>new ThreadedLogStreamRepository(proxy=>{
            LogManager.AssignStreamToken(proxy, n);
            return proxy;
        });
        static ConcurrentDictionary<ILogStreamToken, ThreadedLogStreamRepository> _repo = new ConcurrentDictionary<ILogStreamToken, ThreadedLogStreamRepository>();

        public static IEnumerable<ILogStreamToken> GetTokens() {
          return _repo.Keys;
        }
        public static ThreadedLogStreamRepository GetRepository(ILogStreamToken key) {
            if (_guidTransform != null) { key = _guidTransform(key); }
            var repo = _repo.GetOrAdd(key, _builder);
            return repo;
        }
        public static ILogStream GetLogger(ILogStreamToken key)
        {
            var repo = GetRepository(key);
            return repo.GetLogger();
        }
        public static ProxyLogStream GetProxy(ILogStreamToken key)
        {
            var repo = GetRepository(key);
            return repo.GetProxy();
        }

        public static void Init(Func<ILogStreamToken, Func<ProxyLogStream, ILogStream>> factory)
        {
            _builder = key=>new ThreadedLogStreamRepository(factory(key));

            var old = _repo;

            _repo = new ConcurrentDictionary<ILogStreamToken, ThreadedLogStreamRepository>();

            foreach (var kvp in old) {
              kvp.Value.GetLogger().Dispose();
            }
        }
        public static void SetTokenTransform(Func<ILogStreamToken, ILogStreamToken> transform) {
           _guidTransform = transform;
        }
    }
}
