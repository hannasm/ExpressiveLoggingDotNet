using System.Collections.Generic;
using System.Linq;

namespace ExpressiveLogging.V3.Filtering
{
    public class CompositeFilterToken : ILogTokenFilter
    {
        private HashSet<ILogTokenFilter> _filters = new HashSet<ILogTokenFilter>();
        public void AddFilter(params ILogTokenFilter[] filters)
        {
            for (int i = 0, n = filters.Length; i < n; ++i)
            {
                _filters.Add(filters[i]);
            }
        }

        public void AddFilter(IEnumerable<ILogTokenFilter> filters)
        {
            foreach (var filter in filters)
            {
                _filters.Add(filter);
            }
        }

        public void RemoveFilter(params ILogTokenFilter[] filters)
        {
            _filters.RemoveWhere(filters.Contains);
        }

        public void RemoveFilter(IEnumerable<ILogTokenFilter> filters)
        {
            _filters.RemoveWhere(filters.Contains);
        }

        public bool IsWriteEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsWriteEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsCounterEnabled(ICounterToken ct)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsCounterEnabled(ct)) { return false; }
            }
            return true;
        }

        public bool IsCounterEnabled(INamedCounterToken ct, ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsCounterEnabled(ct, lt)) { return false; }
            }
            return true;
        }

        public bool IsWriteConfigured()
        {
            return _filters.Any(f => f.IsWriteConfigured());
        }

        public bool IsRawCounterConfigured()
        {
            return _filters.Any(f => f.IsRawCounterConfigured());
        }

        public bool IsNamedCounterConfigured()
        {
            return _filters.Any(f => f.IsNamedCounterConfigured());
        }
    }
}
