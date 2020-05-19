using System;
using ExpressiveLogging.V3.Filtering;
namespace ExpressiveLogging.V3
{
    public static class FilterManager
    {
        /// <summary>
        /// Generates a filter token, all logging levels filtered the same way
        /// Just pass null for unused arguments
        /// </summary>
        public static ILogTokenFilter CreateFilter(
                Func<ILogToken, bool> messageFilter,
                Func<ICounterToken, bool> rawCounterFilter,
                Func<INamedCounterToken, ILogToken, bool> namedCounterFilter)
        {
            return new LambdaFilter(
                messageFilter,
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
