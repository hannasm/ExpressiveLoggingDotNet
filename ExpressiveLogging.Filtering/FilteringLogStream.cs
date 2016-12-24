using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System.Diagnostics;

namespace ExpressiveLogging.Filtering
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


        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _alert_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _alert_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _alert_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _alert_formatWithUniqueness;
        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _alert_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _alert_format(this, t, msgBuilder);
        }
        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _alert_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _alert_exception(this, t, msgBuilder);
        }

        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _audit_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _audit_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _audit_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _audit_formatWithUniqueness;
        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _audit_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _audit_format(this, t, msgBuilder);
        }
        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _audit_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _audit_exception(this, t, msgBuilder);
        }

        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _debug_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _debug_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _debug_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _debug_formatWithUniqueness;
        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _debug_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _debug_format(this, t, msgBuilder);
        }
        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _debug_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _debug_exception(this, t, msgBuilder);
        }

        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _error_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _error_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _error_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _error_formatWithUniqueness;
        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _error_format(this, t, msgBuilder);
        }
        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _error_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _error_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _error_exception(this, t, msgBuilder);
        }

        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _info_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _info_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _info_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _info_formatWithUniqueness;
        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _info_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _info_format(this, t, msgBuilder);
        }
        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _info_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _info_exception(this, t, msgBuilder);
        }

        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessage>> _warning_exception;
        Action<FilteringLogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> _warning_excetionWithUniqueness;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessage>> _warning_format;
        Action<FilteringLogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> _warning_formatWithUniqueness;
        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _warning_formatWithUniqueness(this, t, msgBuilder);
        }
        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            _warning_excetionWithUniqueness(this, t, msgBuilder);
        }
        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _warning_format(this, t, msgBuilder);
        }
        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            _warning_exception(this, t, msgBuilder);
        }
        
        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.BeginScope(ctx, t, msgBuilder);
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.EndScope(ctx, t, msgBuilder);
        }

        Action<FilteringLogStream, IRawCounterToken, long> _rawCounterToken_increment;
        Action<FilteringLogStream, IRawCounterToken, long> _rawCounterToken_set;
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            _rawCounterToken_increment(this, ct, value);
        }

        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            _rawCounterToken_set(this, ct, value);
        }

        Action<FilteringLogStream, INamedCounterToken, ILogToken, long> _namedCounterToken_increment;
        Action<FilteringLogStream, INamedCounterToken, ILogToken, long> _namedCounterToken_set;
        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            _namedCounterToken_increment(this, ct,  lt, value);
        }
        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            _namedCounterToken_set(this, ct, lt, value);
        }

        public void Dispose()
        {
            _inner.Dispose();
        }
        
        void InitActions()
        {
            if (_filters.IsAlertConfigured())
            {
                _alert_exception = (i,t,m)=>{
                    if (i._filters.IsAlertEnabled(t)) { i._inner.Alert(t, m); }
                };
                _alert_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsAlertEnabled(t)) { i._inner.Alert(t, m); }
                };
                _alert_format = (i, t, m) => {
                    if (i._filters.IsAlertEnabled(t)) { i._inner.Alert(t, m); }
                };
                _alert_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsAlertEnabled(t)) { i._inner.Alert(t, m); }
                };
            }
            else
            {
                _alert_exception = (i, t, m) => { i._inner.Alert(t, m); };
                _alert_excetionWithUniqueness = (i, t, m) => { i._inner.Alert(t, m); };
                _alert_format = (i, t, m) => { i._inner.Alert(t, m); };
                _alert_formatWithUniqueness = (i, t, m) => { i._inner.Alert(t, m); };
            }

            if (_filters.IsAuditConfigured())
            {
                _audit_exception = (i, t, m) => {
                    if (i._filters.IsAuditEnabled(t)) { i._inner.Audit(t, m); }
                };
                _audit_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsAuditEnabled(t)) { i._inner.Audit(t, m); }
                };
                _audit_format = (i, t, m) => {
                    if (i._filters.IsAuditEnabled(t)) { i._inner.Audit(t, m); }
                };
                _audit_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsAuditEnabled(t)) { i._inner.Audit(t, m); }
                };
            }
            else
            {
                _audit_exception = (i, t, m) => { i._inner.Audit(t, m); };
                _audit_excetionWithUniqueness = (i, t, m) => { i._inner.Audit(t, m); };
                _audit_format = (i, t, m) => { i._inner.Audit(t, m); };
                _audit_formatWithUniqueness = (i, t, m) => { i._inner.Audit(t, m); };
            }

            if (_filters.IsDebugConfigured())
            {
                _debug_exception = (i, t, m) => {
                    if (i._filters.IsDebugEnabled(t)) { i._inner.Debug(t, m); }
                };
                _debug_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsDebugEnabled(t)) { i._inner.Debug(t, m); }
                };
                _debug_format = (i, t, m) => {
                    if (i._filters.IsDebugEnabled(t)) { i._inner.Debug(t, m); }
                };
                _debug_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsDebugEnabled(t)) { i._inner.Debug(t, m); }
                };
            }
            else
            {
                _debug_exception = (i, t, m) => { i._inner.Debug(t, m); };
                _debug_excetionWithUniqueness = (i, t, m) => { i._inner.Debug(t, m); };
                _debug_format = (i, t, m) => { i._inner.Debug(t, m); };
                _debug_formatWithUniqueness = (i, t, m) => { i._inner.Debug(t, m); };
            }

            if (_filters.IsErrorConfigured())
            {
                _error_exception = (i, t, m) => {
                    if (i._filters.IsErrorEnabled(t)) { i._inner.Error(t, m); }
                };
                _error_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsErrorEnabled(t)) { i._inner.Error(t, m); }
                };
                _error_format = (i, t, m) => {
                    if (i._filters.IsErrorEnabled(t)) { i._inner.Error(t, m); }
                };
                _error_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsErrorEnabled(t)) { i._inner.Error(t, m); }
                };
            }
            else
            {
                _error_exception = (i, t, m) => { i._inner.Error(t, m); };
                _error_excetionWithUniqueness = (i, t, m) => { i._inner.Error(t, m); };
                _error_format = (i, t, m) => { i._inner.Error(t, m); };
                _error_formatWithUniqueness = (i, t, m) => { i._inner.Error(t, m); };
            }

            if (_filters.IsInfoConfigured())
            {
                _info_exception = (i, t, m) => {
                    if (i._filters.IsInfoEnabled(t)) { i._inner.Info(t, m); }
                };
                _info_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsInfoEnabled(t)) { i._inner.Info(t, m); }
                };
                _info_format = (i, t, m) => {
                    if (i._filters.IsInfoEnabled(t)) { i._inner.Info(t, m); }
                };
                _info_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsInfoEnabled(t)) { i._inner.Info(t, m); }
                };
            }
            else
            {
                _info_exception = (i, t, m) => { i._inner.Info(t, m); };
                _info_excetionWithUniqueness = (i, t, m) => { i._inner.Info(t, m); };
                _info_format = (i, t, m) => { i._inner.Info(t, m); };
                _info_formatWithUniqueness = (i, t, m) => { i._inner.Info(t, m); };
            }

            if (_filters.IsWarningConfigured())
            {
                _warning_exception = (i, t, m) => {
                    if (i._filters.IsWarningEnabled(t)) { i._inner.Warning(t, m); }
                };
                _warning_excetionWithUniqueness = (i, t, m) => {
                    if (i._filters.IsWarningEnabled(t)) { i._inner.Warning(t, m); }
                };
                _warning_format = (i, t, m) => {
                    if (i._filters.IsWarningEnabled(t)) { i._inner.Warning(t, m); }
                };
                _warning_formatWithUniqueness = (i, t, m) => {
                    if (i._filters.IsWarningEnabled(t)) { i._inner.Warning(t, m); }
                };
            }
            else
            {
                _warning_exception = (i, t, m) => { i._inner.Warning(t, m); };
                _warning_excetionWithUniqueness = (i, t, m) => { i._inner.Warning(t, m); };
                _warning_format = (i, t, m) => { i._inner.Warning(t, m); };
                _warning_formatWithUniqueness = (i, t, m) => { i._inner.Warning(t, m); };
            }

            if (_filters.IsRawCounterConfigured())
            {
                _rawCounterToken_increment = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.IncrementCounterBy(ct, v);
                };
                _rawCounterToken_set = (i, ct, v) => {
                    if (!i._filters.IsCounterEnabled(ct)) { return; }
                    i._inner.SetCounterValue(ct, v);
                };
            }
            else
            {
                _rawCounterToken_increment = (i, ct, v) => {
                    i._inner.IncrementCounterBy(ct, v);
                };
                _rawCounterToken_set = (i, ct, v) => {
                    i._inner.SetCounterValue(ct, v);
                };
            }

            if (_filters.IsNamedCounterConfigured())
            {
                _namedCounterToken_increment = (i, ct, lt, v) => {
                    if (!i._filters.IsCounterEnabled(lt, ct)) { return; }
                    i._inner.IncrementCounterBy(lt, ct, v);
                };
                _namedCounterToken_set = (i, ct, lt, v) => {
                    if (!i._filters.IsCounterEnabled(lt, ct)) { return; }
                    i._inner.SetCounterValue(lt, ct, v);
                };
            }
            else
            {
                _namedCounterToken_increment = (i, ct, lt, v) => {
                    i._inner.IncrementCounterBy(lt, ct, v);
                };
                _namedCounterToken_set = (i, ct, lt, v) => {
                    i._inner.SetCounterValue(lt, ct, v);
                };
            }
        }
    }
}
