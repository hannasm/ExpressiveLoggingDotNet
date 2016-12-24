using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Filtering
{
    public interface ILogTokenFilter
    {
        bool IsDebugEnabled(ILogToken lt);
        bool IsDebugConfigured();

        bool IsInfoEnabled(ILogToken lt);
        bool IsInfoConfigured();

        bool IsAuditEnabled(ILogToken lt);
        bool IsAuditConfigured();

        bool IsWarningEnabled(ILogToken lt);
        bool IsWarningConfigured();

        bool IsErrorEnabled(ILogToken lt);
        bool IsErrorConfigured();

        bool IsAlertEnabled(ILogToken lt);
        bool IsAlertConfigured();

        bool IsCounterEnabled(IRawCounterToken ct);
        bool IsRawCounterConfigured();

        bool IsCounterEnabled(ILogToken lt, INamedCounterToken ct);
        bool IsNamedCounterConfigured();
    }
}
