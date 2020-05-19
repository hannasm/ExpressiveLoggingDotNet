using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Filtering
{
    public class LambdaFilter : ILogTokenFilter
    {
        private Func<ILogToken, bool> _writeFilter;
        private Func<ICounterToken, bool> _rawCounterFilter;
        private Func<INamedCounterToken, ILogToken, bool> _namedCounterFilter;

        private bool _writeConfigured = true;
        private bool _rawCounterConfigured = true;
        private bool _namedCounterConfigured = true;


        public LambdaFilter(
            Func<ILogToken, bool> debugFilter,
            Func<ICounterToken, bool> rawCounterFilter,
            Func<INamedCounterToken, ILogToken, bool> namedCounterFilter)
        {
            if (debugFilter == null) { debugFilter = _ => true; _writeConfigured = false; }
            if (rawCounterFilter == null) { rawCounterFilter = _ => true; _rawCounterConfigured = false; }
            if (namedCounterFilter == null) { namedCounterFilter = (_1, _2) => true; _namedCounterConfigured = false; }

            _writeFilter = debugFilter;
            _rawCounterFilter = rawCounterFilter;
            _namedCounterFilter = namedCounterFilter;
        }

        public bool IsWriteEnabled(ILogToken lt)
        {
            return _writeFilter(lt);
        }

        public bool IsCounterEnabled(ICounterToken ct)
        {
            return _rawCounterFilter(ct);
        }

        public bool IsCounterEnabled(INamedCounterToken ct, ILogToken lt)
        {
            return _namedCounterFilter(ct, lt);
        }

        public bool IsWriteConfigured()
        {
            return _writeConfigured;
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
