using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public interface ILogTokenFilter
    {
        bool IsWriteEnabled(ILogToken lt);
        bool IsWriteConfigured();

        bool IsCounterEnabled(ICounterToken ct);
        bool IsRawCounterConfigured();

        bool IsCounterEnabled(INamedCounterToken ct, ILogToken lt);
        bool IsNamedCounterConfigured();
    }
}
