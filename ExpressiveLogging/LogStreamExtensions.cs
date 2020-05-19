using System;
using System.Diagnostics;

namespace ExpressiveLogging.V3
{
    public static class LogStreamExtensions
    {
        public static void IncrementCounter(this ILogStream log, ICounterToken ct)
        {
            log.IncrementCounterBy(ct, 1);
        }
        public static void DecrementCounter(this ILogStream log, ICounterToken ct)
        {
            log.IncrementCounterBy(ct, -1);
        }
        public static void IncrementCounterBy(this ILogStream log, ICounterToken ct, Stopwatch watch)
        {
            log.IncrementCounterBy(ct, watch.ElapsedTicks);
        }
        public static void IncrementCounter(this ILogStream log, ICounterToken ct, ILogToken lt)
        {
            var nct = LogManager.CreateNamedCounterToken(ct.Counter, lt.Name);
            log.IncrementCounterBy(ct, 1);
        }
        public static void DecrementCounter(this ILogStream log, ICounterToken ct, ILogToken lt)
        {
            var nct = LogManager.CreateNamedCounterToken(ct.Counter, lt.Name);
            log.IncrementCounterBy(nct, -1);
        }
        public static void IncrementCounterBy(this ILogStream log, ICounterToken ct, ILogToken lt, long value)
        {
            var nct = LogManager.CreateNamedCounterToken(ct.Counter, lt.Name);
            log.IncrementCounterBy(nct, value);
        }
        public static void IncrementCounterBy(this ILogStream log, ICounterToken ct, ILogToken lt, Stopwatch watch)
        {
            var nct = LogManager.CreateNamedCounterToken(ct.Counter, lt.Name);
            log.IncrementCounterBy(nct, watch.ElapsedTicks);
        }
        public static void SetCounter(this ILogStream log, ICounterToken ct, ILogToken lt, long value) {
            var nct = LogManager.CreateNamedCounterToken(ct.Counter, lt.Name);
            log.SetCounter(nct, value);
        }

				public delegate void LogException(Exception e);
				public delegate void LogExceptionUniqueness(Exception e, int uniqueness);
				public delegate void LogExceptionMessage(Exception e, string message, params object[] format);
    		public delegate void LogFormatMessage(string message, params object[] format);
    		public delegate void LogFormatMessageWithCustomUniqueness(int uniqueness, string message, params object[] format);

        public static void Write(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
					log.Write(token, mo=>{
						msgBuilder((m,f)=>{
							mo(null, LogManager.GenerateUniquenessCode(m,f), m, f);
						});
					});
				}
        public static void Write(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
					log.Write(token, mo=>{
						msgBuilder( (e,m,f)=>{
							mo(e, LogManager.GenerateUniquenessCode(e,m,f), m, f);
						});
					});
				}
        public static void Write(this ILogStream log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
					log.Write(token, mo=>{
						msgBuilder( (e,u)=>{
							mo(e, u, null, null);
						});
					});
				}
        public static void Write(this ILogStream log, ILogToken token, Action<LogException> msgBuilder) {
					log.Write(token, mo=>{
						msgBuilder( (e)=>{
							mo(e, LogManager.GenerateUniquenessCode(e), null, null);
						});
					});
				}
        public static void Write(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
					log.Write(token, mo=>{
						msgBuilder( (u,m,f)=>{
							mo(null, u, m, f);
						});
					});
				}
/* TODO: Make separate DLL you can load to get these if you decide it would be beneficial
        public static void Debug(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Debug.Write(token, msgBuilder);
				}
        public static void Debug(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Debug.Write(token, msgBuilder);
				}
        public static void Debug(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Debug.Write(token, msgBuilder);
				}
        public static void Debug(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Debug.Write(token, msgBuilder);
				}

        public static void Info(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Info.Write(token, msgBuilder);
				}
        public static void Info(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Info.Write(token, msgBuilder);
				}
        public static void Info(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Info.Write(token, msgBuilder);
				}
        public static void Info(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Info.Write(token, msgBuilder);
				}

        public static void Audit(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Audit.Write(token, msgBuilder);
				}
        public static void Audit(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Audit.Write(token, msgBuilder);
				}
        public static void Audit(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Audit.Write(token, msgBuilder);
				}
        public static void Audit(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Audit.Write(token, msgBuilder);
				}
        public static void Warning(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Warning.Write(token, msgBuilder);
				}
        public static void Warning(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Warning.Write(token, msgBuilder);
				}
        public static void Warning(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Warning.Write(token, msgBuilder);
				}
        public static void Warning(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Warning.Write(token, msgBuilder);
				}
        public static void Error(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Error.Write(token, msgBuilder);
				}
        public static void Error(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Error.Write(token, msgBuilder);
				}
        public static void Error(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Error.Write(token, msgBuilder);
				}
        public static void Error(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Error.Write(token, msgBuilder);
				}

        public static void Alert(this ILogStream log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
          LogManager.Alert.Write(token, msgBuilder);
				}
        public static void Alert(this ILogStream log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
          LogManager.Alert.Write(token, msgBuilder);
				}
        public static void Alert(this ILogStream log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
          LogManager.Alert.Write(token, msgBuilder);
				}
        public static void Alert(this ILogStream log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
          LogManager.Alert.Write(token, msgBuilder);
				}
        */
    }
}
