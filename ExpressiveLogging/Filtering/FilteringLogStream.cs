using System;
using System.Collections.Generic;
using ExpressiveLogging.V3.Filtering;

namespace ExpressiveLogging.V3
{
    public class FilteringLogStream : ILogStream
    {
        CompositeFilterToken _filters = new CompositeFilterToken();
        ILogStream _inner;

        internal FilteringLogStream(ILogStream inner)
        {
            _inner = inner;
            InitActions();
        }

        public void AddFilter(params ILogTokenFilter[] filters)
        {
            _filters.AddFilter(filters);
            InitActions();
        }
        public void AddFilter(IEnumerable<ILogTokenFilter> filters)
        {
            _filters.AddFilter(filters);
            InitActions();
        }
        public void RemoveFilter(params ILogTokenFilter[] filters)
        {
            _filters.RemoveFilter(filters);
            InitActions();
        }
        public void RemoveFilter(IEnumerable<ILogTokenFilter> filters)
        {
            _filters.RemoveFilter(filters);
            InitActions();
        }


        Action<FilteringLogStream, ILogToken, Action<CompleteLogMessage>> _write;
        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            _write(this, t, msgBuilder);
        }
        
        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnDetachScopeParameters(lt, parameters);
        }

        Action<FilteringLogStream, ICounterToken, long> _rawCounterToken_increment;
        Action<FilteringLogStream, ICounterToken, long> _rawCounterToken_set;
        Action<FilteringLogStream, INamedCounterToken, long> _namedCounterToken_increment;
        Action<FilteringLogStream, INamedCounterToken, long> _namedCounterToken_set;
        public void IncrementCounterBy(ICounterToken ct, long value)
        {
          var nct = ct as INamedCounterToken;
          if (nct != null) { _namedCounterToken_increment(this, nct, value); }
          else { _rawCounterToken_increment(this, ct, value); }
        }

        public void SetCounter(ICounterToken ct, long value)
        {
          var nct = ct as INamedCounterToken;
          if (nct != null) { _rawCounterToken_set(this, nct, value); }
          else { _rawCounterToken_set(this, ct, value); }
            
        }


        public void Dispose()
        {
            _inner.Dispose();
        }
        
        void InitActions()
        {
            if (_filters.IsWriteConfigured()) {
                _write = (i, t, m) => {
                    if (i._filters.IsWriteEnabled(t)) { i._inner.Write(t, m); }
                };
            }

            if (_filters.IsRawCounterConfigured())
            {
                _rawCounterToken_increment = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.IncrementCounterBy(ct, v);
                };
                _rawCounterToken_set = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.SetCounter(ct, v);
                };
            }
            else
            {
                _rawCounterToken_increment = (i, ct, v) => {
                    i._inner.IncrementCounterBy(ct, v);
                };
                _rawCounterToken_set = (i, ct, v) => {
                    i._inner.SetCounter(ct, v);
                };
            }

            if (_filters.IsNamedCounterConfigured())
            {
                _namedCounterToken_increment = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.IncrementCounterBy(ct, v);
                };
                _namedCounterToken_set = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.SetCounter(ct, v);
                };
            }
            else
            {
                _namedCounterToken_increment = (i, ct, v) => {
                    i._inner.IncrementCounterBy(ct, v);
                };
                _namedCounterToken_set = (i, ct, v) => {
                    i._inner.SetCounter(ct, v);
                };
            }
        }
    }
}
