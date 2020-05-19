using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressiveLogging.V3
{
    public class ProxyLogStream : ILogStream
    {
        ILogStream _inner;
        HashSet<ILogStream> _channels;

        public ProxyLogStream() {
          _channels = new HashSet<ILogStream>();
          buildProxy();
        }

        public int Count {
          get {
            return _channels.Count;
          }
        }

        public void Add(ILogStream stream) {
          _channels.Add(stream);
          buildProxy();
        }
        public bool Remove(ILogStream stream) {
          var result = _channels.Remove(stream);
          buildProxy();
          return result;
        } 

        void buildProxy() {
          if (_channels.Count == 0) {
            _inner = new NullLogStream();
          } else if (_channels.Count == 1) {
            _inner = _channels.Single();
          } else {
            _inner = CompositeLogStream.Create(_channels);
          }
        }

        public void Dispose()
        {
          _inner.Dispose();
        }

        public void IncrementCounterBy(ICounterToken ct, long value)
        {
          _inner.IncrementCounterBy(ct, value);
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
          _inner.OnAttachScopeParameters(lt, parameters);
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnDetachScopeParameters(lt, parameters);
        }

        public void SetCounter(ICounterToken ct, long value)
        {
          _inner.SetCounter(ct, value);
        }

        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
          _inner.Write(t, msgBuilder);
        }
    }
}
