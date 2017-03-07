using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    public static class LogStreamExtensions
    {
        public static void IncrementCounter(this ILogStream log, IRawCounterToken ct)
        {
            log.IncrementCounterBy(ct, 1);
        }
        public static void DecrementCounter(this ILogStream log, IRawCounterToken ct)
        {
            log.IncrementCounterBy(ct, -1);
        }
        public static void IncrementCounterBy(this ILogStream log, IRawCounterToken ct, Stopwatch watch)
        {
            log.IncrementCounterBy(ct, watch.ElapsedTicks);
        }
        public static void IncrementCounter(this ILogStream log, ILogToken tok, INamedCounterToken ct)
        {
            log.IncrementCounterBy(tok, ct, 1);
        }
        public static void DecrementCounter(this ILogStream log, ILogToken tok, INamedCounterToken ct)
        {
            log.IncrementCounterBy(tok, ct, -1);
        }
        public static void IncrementCounterBy(this ILogStream log, ILogToken tok, INamedCounterToken ct, Stopwatch watch)
        {
            log.IncrementCounterBy(tok, ct, watch.ElapsedTicks);
        }
    }
}
