using ExpressiveLogging.V3.Context;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExpressiveLogging.V3
{
    public static class LogManager
    {
        public static LogScopeBuilder BuildScope(
            ILogToken lt
        )
        {
            return new LogScopeBuilder(lt);
        }
        public static ILogContextScope NewScope(
            ILogToken lt
        )
        {
            return new LogScopeBuilder(lt).NewScope();
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(Exception error, string message, params object[] args)
        {
            return GenerateUniquenessCode(message, args) ^ GenerateUniquenessCode(error);
        }

        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(string message)
        {
            if (message == null) { return 27; }
            return message.GetHashCode();
        }

        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(string message, params object[] args)
        {
            return message == null ? 27 : message.GetHashCode();
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(Exception exc)
        {
            if (exc == null) { return 37; }
            return exc.ToString().GetHashCode();
        }
        /// <summary>
        /// Generate a proper uniqueness code for log aggregation
        /// </summary>
        public static int GenerateUniquenessCode(ILogToken lt)
        {
            return 97; // messages are already 'unique' per log token because the log token name is captured
        }

        public static ILogToken GetToken()
        {
            StackFrame frame = new StackFrame(1, false);
            return new LogToken(frame.GetMethod().DeclaringType);
        }
        public static ILogToken GetToken(ILogToken token, string name)
        {
            return new LogToken(string.Concat(token.Name, ".", name));
        }
        public static ILogToken GetToken(string name)
        {
            return new LogToken(name);
        }

        public static ILogStreamToken GetStreamToken()
        {
            StackFrame frame = new StackFrame(1, false);
            return new LogToken(frame.GetMethod().DeclaringType);
        }
        public static ILogStreamToken GetStreamToken(ILogToken token, string name)
        {
            return new LogToken(string.Concat(token.Name, ".", name));
        }
        public static ILogStreamToken GetStreamToken(string name)
        {
            return new LogToken(name);
        }

        public static ICounterToken CreateRawCounterToken(string counter)
        {
            return new CounterToken(counter);
        }
        public static INamedCounterToken CreateNamedCounterToken(string counter, ILogToken tok)
        {
          return CreateNamedCounterToken(counter, tok.Name);
        }
        public static INamedCounterToken CreateNamedCounterToken(string counter, string name)
        {
            return new NamedCounterToken(counter, name);
        }

        static ConditionalWeakTable<ILogStream, ILogStreamToken> _streamToken = new ConditionalWeakTable<ILogStream, ILogStreamToken>();
        public static void AssignStreamToken(ILogStream stream, ILogStreamToken token) {
          if (!_streamToken.TryGetValue(stream, out var theToken)) {
            _streamToken.Add(stream, token);
          }
        }
        public static ILogStreamToken GetToken(this ILogStream stream) {
          if (!_streamToken.TryGetValue(stream, out var theToken)) {
            return STREAM_TOKEN_UNASSIGNED;
          }
          return theToken;
        }

        public const string STREAM_TOKEN_UNASSIGNED_NAME = "UNASSIGNED";
        public const string STREAM_TOKEN_ALERT_NAME = "ALERT";
        public const string STREAM_TOKEN_ERROR_NAME = "ERROR";
        public const string STREAM_TOKEN_WARNING_NAME = "WARNING";
        public const string STREAM_TOKEN_AUDIT_NAME = "AUDIT";
        public const string STREAM_TOKEN_INFO_NAME = "INFO";
        public const string STREAM_TOKEN_DEBUG_NAME = "DEBUG";

        public static readonly ILogStreamToken STREAM_TOKEN_UNASSIGNED = LogManager.GetStreamToken(STREAM_TOKEN_UNASSIGNED_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_ALERT = LogManager.GetStreamToken(STREAM_TOKEN_ALERT_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_ERROR = LogManager.GetStreamToken(STREAM_TOKEN_ERROR_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_WARNING = LogManager.GetStreamToken(STREAM_TOKEN_WARNING_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_AUDIT = LogManager.GetStreamToken(STREAM_TOKEN_AUDIT_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_INFO = LogManager.GetStreamToken(STREAM_TOKEN_INFO_NAME);
        public static readonly ILogStreamToken STREAM_TOKEN_DEBUG = LogManager.GetStreamToken(STREAM_TOKEN_DEBUG_NAME);

        public static ILogStream Alert {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_ALERT);
          }
        }
        public static ILogStream Error {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_ERROR);
          }
        }
        public static ILogStream Warning {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_WARNING);
          }
        }
        public static ILogStream Audit {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_AUDIT);
          }
        }
        public static ILogStream Info {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_INFO);
          }
        }
        public static ILogStream Debug {
          get {
            return StaticLogRepository.GetLogger(STREAM_TOKEN_DEBUG);
          }
        }

        static readonly IExpressiveLogs _iel = new ExpressiveLogs();
        public static IExpressiveLogs Logs {
          get {
            return _iel;
          }
        }
    }
}
