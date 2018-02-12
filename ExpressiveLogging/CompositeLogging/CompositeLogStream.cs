using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressiveLogging.CompositeLogging
{
    public class CompositeLogStream : ILogStream
    {
        private ILogStream[] _loggers;

        private CompositeLogStream(params ILogStream[] loggersToCompose)
        {
            _loggers = loggersToCompose;
        }

        public static ILogStream Create(params ILogStream[] loggersToCompose)
        {
            if (loggersToCompose == null)
            {
                return null;
            }
            if (loggersToCompose.Length == 1)
            {
                return loggersToCompose[0];
            }
            return new CompositeLogStream(loggersToCompose);
        }

        private void CompositeAction(Action<ILogStream> action)
        {
            foreach (var logger in _loggers)
            {
                action(logger);
            }
        }
        
        private Action<LogExceptionMessage> CreateMessageClosure(Action<LogExceptionMessage> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }
        private Action<LogFormatMessage> CreateMessageClosure(Action<LogFormatMessage> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }        
        private Action<LogExceptionMessageWithCustomUniqueness> CreateMessageClosure(Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }
        private Action<LogFormatMessageWithCustomUniqueness> CreateMessageClosure(Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            CompositeAction(l => l.OnAttachScopeParameters(lt, parameters));
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            CompositeAction(l => l.BeginScope(ctx, t, msgBuilder));
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            CompositeAction(l => l.EndScope(ctx, t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);
            CompositeAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);
            CompositeAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);
            CompositeAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);
            CompositeAction(l => l.Debug(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Info(t, msgBuilder));
        }
        

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Audit(t, msgBuilder));
        }
                
        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Warning(t, msgBuilder));
        }
        
        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Error(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
                msgBuilder = CreateMessageClosure(msgBuilder);
                CompositeAction(l => l.Alert(t, msgBuilder));
        }
        
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            CompositeAction(l => l.IncrementCounterBy(ct, value));
        }
        
        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            CompositeAction(l => l.SetCounterValue(ct, value));
        }
        
        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            CompositeAction(l => l.IncrementCounterBy(lt, ct, value));
        }        
        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            CompositeAction(l => l.SetCounterValue(lt, ct, value));
        }
        
        public void Dispose()
        {
            CompositeAction(l => l.Dispose());
        }
    }
}
