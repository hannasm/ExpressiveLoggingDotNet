using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressiveLogging.V3
{
    public class CompositeLogStream : ILogStream
    {
        private readonly ILogStream[] _loggers;

        private CompositeLogStream(params ILogStream[] loggersToCompose)
        {
            _loggers = loggersToCompose;
        }

        public static ILogStream Create(IEnumerable<ILogStream> loggersToCompose) {
          return CompositeLogStream.Create(loggersToCompose.ToArray());
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
        
        private Action<CompleteLogMessage> CreateMessageClosure(Action<CompleteLogMessage> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            CompositeAction(l => l.OnAttachScopeParameters(lt, parameters));
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            CompositeAction(l => l.OnDetachScopeParameters(lt, parameters));
        }

        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);
            CompositeAction(l => l.Write(t, msgBuilder));
        }

        public void IncrementCounterBy(ICounterToken ct, long value)
        {
            CompositeAction(l => l.IncrementCounterBy(ct, value));
        }
        
        public void SetCounter(ICounterToken ct, long value)
        {
            CompositeAction(l => l.SetCounter(ct, value));
        }
        
        public void Dispose()
        {
            CompositeAction(l => l.Dispose());
        }
    }
}
