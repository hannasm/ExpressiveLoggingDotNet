using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Filtering
{
    public static class FilterManager
    {
        /// <summary>
        /// Generates a filter token, all logging levels filtered the same way
        /// Just pass null for unused arguments
        /// </summary>
        public static ILogTokenFilter CreateFilter(
                Func<ILogToken, bool> messageFilter,
                Func<IRawCounterToken, bool> rawCounterFilter,
                Func<ILogToken, INamedCounterToken, bool> namedCounterFilter)
        {
            return new LambdaFilter(
                messageFilter,
                messageFilter,
                messageFilter,
                messageFilter,
                messageFilter,
                messageFilter,
                rawCounterFilter,
                namedCounterFilter);
        }

        /// <summary>
        /// Generate a filter token, separate lambda expression accepted for each possible logging level
        /// Just pass null for unused arguments
        /// </summary>
        public static ILogTokenFilter CreateFilter(
                Func<ILogToken, bool> debugFilter,
                Func<ILogToken, bool> infoFilter,
                Func<ILogToken, bool> auditFilter,
                Func<ILogToken, bool> warningFilter,
                Func<ILogToken, bool> errorFilter,
                Func<ILogToken, bool> alertFilter,
                Func<IRawCounterToken, bool> rawCounterFilter,
                Func<ILogToken, INamedCounterToken, bool> namedCounterFilter)
        {
            return new LambdaFilter(
                debugFilter,
                infoFilter,
                auditFilter,
                warningFilter,
                errorFilter,
                alertFilter,
                rawCounterFilter,
                namedCounterFilter);
        }
        
        public static ILogStream CreateStream(
            ILogStream inner,
            params ILogTokenFilter[] filters)
        {
            var result = new FilteringLogStream(inner);
            if (filters != null && filters.Length > 0)
            {
                result.AddFilter(filters);
            }
            return result;
        }
    }
}
