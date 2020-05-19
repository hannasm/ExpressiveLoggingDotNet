using System;
using static ExpressiveLogging.V3.LogStreamExtensions;

namespace ExpressiveLogging.V3 {
  public static class ExpressiveLoggingExtensions {
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }
    public static void Debug(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Debug.Write(token, msgBuilder);
    }

    public static void Info(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }
    public static void Info(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }
    public static void Info(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }
    public static void Info(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }
    public static void Info(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }
    public static void Info(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Info.Write(token, msgBuilder);
    }

    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }
    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }
    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }
    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }
    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }
    public static void Audit(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Audit.Write(token, msgBuilder);
    }

    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }
    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }
    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }
    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }
    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }
    public static void Warning(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Warning.Write(token, msgBuilder);
    }

    public static void Error(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }
    public static void Error(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }
    public static void Error(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }
    public static void Error(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }
    public static void Error(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }
    public static void Error(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Error.Write(token, msgBuilder);
    }

    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessage> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<LogException> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<LogExceptionUniqueness> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<LogExceptionMessage> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<LogFormatMessageWithCustomUniqueness> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
    public static void Alert(this IExpressiveLogs log, ILogToken token, Action<CompleteLogMessage> msgBuilder) {
      LogManager.Alert.Write(token, msgBuilder);
    }
  }
}
