using ExpressiveLogging.Counters;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ExpressiveLogging.Filtering
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

        public bool IsDebugEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsDebugEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsInfoEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsInfoEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsAuditEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsAuditEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsWarningEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsWarningEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsErrorEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsErrorEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsAlertEnabled(ILogToken lt)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsAlertEnabled(lt)) { return false; }
            }
            return true;
        }

        public bool IsCounterEnabled(IRawCounterToken ct)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsCounterEnabled(ct)) { return false; }
            }
            return true;
        }

        public bool IsCounterEnabled(ILogToken lt, INamedCounterToken ct)
        {
            foreach (var filter in _filters)
            {
                if (!filter.IsCounterEnabled(lt, ct)) { return false; }
            }
            return true;
        }

        public bool IsDebugConfigured()
        {
            return _filters.Any(f => f.IsDebugConfigured());
        }

        public bool IsInfoConfigured()
        {
            return _filters.Any(f => f.IsInfoConfigured());
        }

        public bool IsAuditConfigured()
        {
            return _filters.Any(f => f.IsAuditConfigured());
        }

        public bool IsWarningConfigured()
        {
            return _filters.Any(f => f.IsWarningConfigured());
        }

        public bool IsErrorConfigured()
        {
            return _filters.Any(f => f.IsErrorConfigured());
        }

        public bool IsAlertConfigured()
        {
            return _filters.Any(f => f.IsAlertConfigured());
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
