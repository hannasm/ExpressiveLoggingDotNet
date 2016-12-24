using System;

namespace ExpressiveLogging
{
    public sealed class LoggingLevel
    {
        LoggingLevel(
            string name,
            Action<ILogStream, ILogToken, Action<LogFormatMessage>> logMessage,
            Action<ILogStream, ILogToken, Action<LogExceptionMessage>> logExceptionMessage,
            Action<ILogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> logExceptionMessageWithCustomUniqueness,
            Action<ILogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> logFormatMessageWithCustomUniqueness
        )
        {
            Name = name;
            LogMessage = logMessage;
            LogExceptionMessage = logExceptionMessage;
            LogExceptionMessageWithCustomUniqueness = logExceptionMessageWithCustomUniqueness;
            LogFormatMessageWithCustomUniqueness = logFormatMessageWithCustomUniqueness;
        }

        public override string ToString() { return this.Name; }

        public readonly string Name;
        public readonly Action<ILogStream, ILogToken, Action<LogFormatMessage>> LogMessage;
        public readonly Action<ILogStream, ILogToken, Action<LogExceptionMessage>> LogExceptionMessage;
        public readonly Action<ILogStream, ILogToken, Action<LogExceptionMessageWithCustomUniqueness>> LogExceptionMessageWithCustomUniqueness;
        public readonly Action<ILogStream, ILogToken, Action<LogFormatMessageWithCustomUniqueness>> LogFormatMessageWithCustomUniqueness;

        public static readonly LoggingLevel DEBUG, INFO, AUDIT, WARNING, ERROR, ALERT;

        static LoggingLevel()
        {
            DEBUG = new LoggingLevel(
                "DEBUG",
                (l, t, m) => l.Debug(t, m),
                (l, t, m) => l.Debug(t, m),
                (l, t, m) => l.Debug(t, m),
                (l, t, m) => l.Debug(t, m)
            );
            INFO = new LoggingLevel(
                "INFO",
                (l, t, m) => l.Info(t, m),
                (l, t, m) => l.Info(t, m),
                (l, t, m) => l.Info(t, m),
                (l, t, m) => l.Info(t, m)
            );
            AUDIT = new LoggingLevel(
                "AUDIT",
                (l, t, m) => l.Audit(t, m),
                (l, t, m) => l.Audit(t, m),
                (l, t, m) => l.Audit(t, m),
                (l, t, m) => l.Audit(t, m)
            );
            WARNING = new LoggingLevel(
                "WARNING",
                (l, t, m) => l.Warning(t, m),
                (l, t, m) => l.Warning(t, m),
                (l, t, m) => l.Warning(t, m),
                (l, t, m) => l.Warning(t, m)
            );
            ERROR = new LoggingLevel(
                "ERROR",
                (l, t, m) => l.Error(t, m),
                (l, t, m) => l.Error(t, m),
                (l, t, m) => l.Error(t, m),
                (l, t, m) => l.Error(t, m)
            );
            ALERT = new LoggingLevel(
                "ALERT",
                (l, t, m) => l.Alert(t, m),
                (l, t, m) => l.Alert(t, m),
                (l, t, m) => l.Alert(t, m),
                (l, t, m) => l.Alert(t, m)
            );
        }
    }
}
