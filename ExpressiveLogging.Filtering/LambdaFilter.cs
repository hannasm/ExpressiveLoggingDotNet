using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Filtering
{
    public class LambdaFilter : ILogTokenFilter
    {
        private Func<ILogToken, bool> _debugFilter;
        private Func<ILogToken, bool> _infoFilter;
        private Func<ILogToken, bool> _auditFilter;
        private Func<ILogToken, bool> _warningFilter;
        private Func<ILogToken, bool> _errorFilter;
        private Func<ILogToken, bool> _alertFilter;
        private Func<IRawCounterToken, bool> _rawCounterFilter;
        private Func<ILogToken, INamedCounterToken, bool> _namedCounterFilter;

        private bool _debugConfigured = true;
        private bool _infoConfigured = true;
        private bool _auditConfigured = true;
        private bool _warningConfigured = true;
        private bool _errorConfigured = true;
        private bool _alertConfigured = true;
        private bool _rawCounterConfigured = true;
        private bool _namedCounterConfigured = true;


        public LambdaFilter(
            Func<ILogToken, bool> debugFilter,
            Func<ILogToken, bool> infoFilter,
            Func<ILogToken, bool> auditFilter,
            Func<ILogToken, bool> warningFilter,
            Func<ILogToken, bool> errorFilter,
            Func<ILogToken, bool> alertFilter,
            Func<IRawCounterToken, bool> rawCounterFilter,
            Func<ILogToken, INamedCounterToken, bool> namedCounterFilter)
        {
            if (debugFilter == null) { debugFilter = _ => true; _debugConfigured = false; }
            if (infoFilter == null) { infoFilter = _ => true; _infoConfigured = false; }
            if (auditFilter == null) { auditFilter = _ => true; _auditConfigured = false; }
            if (warningFilter == null) { warningFilter = _ => true; _warningConfigured = false; }
            if (errorFilter == null) { errorFilter = _ => true; _errorConfigured = false; }
            if (alertFilter == null) { alertFilter = _ => true; _alertConfigured = false; }
            if (rawCounterFilter == null) { rawCounterFilter = _ => true; _rawCounterConfigured = false; }
            if (namedCounterFilter == null) { namedCounterFilter = (_1, _2) => true; _namedCounterConfigured = false; }

            _debugFilter = debugFilter;
            _infoFilter = infoFilter;
            _auditFilter = auditFilter;
            _warningFilter = warningFilter;
            _errorFilter = errorFilter;
            _alertFilter = alertFilter;
            _rawCounterFilter = rawCounterFilter;
            _namedCounterFilter = namedCounterFilter;
        }

        public bool IsDebugEnabled(ILogToken lt)
        {
            return _debugFilter(lt);
        }

        public bool IsInfoEnabled(ILogToken lt)
        {
            return _infoFilter(lt);
        }

        public bool IsAuditEnabled(ILogToken lt)
        {
            return _auditFilter(lt);
        }

        public bool IsWarningEnabled(ILogToken lt)
        {
            return _warningFilter(lt);
        }

        public bool IsErrorEnabled(ILogToken lt)
        {
            return _errorFilter(lt);
        }

        public bool IsAlertEnabled(ILogToken lt)
        {
            return _alertFilter(lt);
        }

        public bool IsCounterEnabled(IRawCounterToken ct)
        {
            return _rawCounterFilter(ct);
        }

        public bool IsCounterEnabled(ILogToken lt, INamedCounterToken ct)
        {
            return _namedCounterFilter(lt, ct);
        }

        public bool IsDebugConfigured()
        {
            return _debugConfigured;
        }

        public bool IsInfoConfigured()
        {
            return _infoConfigured;
        }

        public bool IsAuditConfigured()
        {
            return _auditConfigured;
        }

        public bool IsWarningConfigured()
        {
            return _warningConfigured;
        }

        public bool IsErrorConfigured()
        {
            return _errorConfigured;
        }

        public bool IsAlertConfigured()
        {
            return _alertConfigured;
        }

        public bool IsRawCounterConfigured()
        {
            return _rawCounterConfigured;
        }

        public bool IsNamedCounterConfigured()
        {
            return _namedCounterConfigured;
        }
    }
}
