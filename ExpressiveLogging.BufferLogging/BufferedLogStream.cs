using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressiveLogging.BufferLogging
{
    /// <summary>
    /// Buffers all logging actions as they are executed,
    /// these buffered action can then be invoked at a later point in time.
    /// </summary>
    public class BufferedLogStream : ILogStream
    {
        private ConcurrentQueue<Action<ILogStream>> _messageQueue = new ConcurrentQueue<Action<ILogStream>>();
        private static readonly ILogToken _lt = LogManager.GetToken();

        public BufferedLogStream()
        {
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

        /// <summary>
        /// Execute all actions in the buffer on the specified logger
        /// </summary>
        public bool ExecuteBuffer(ILogStream log)
        {
            return ExecuteBuffer(log, TimeSpan.FromMilliseconds(10));
        }
        /// <summary>
        /// Execute as many actions from the buffer in the given timeout interval 
        /// on the specified logger
        /// </summary>
        public bool ExecuteBuffer(ILogStream log, TimeSpan timeout)
        {
            Stopwatch timer = Stopwatch.StartNew();
            while (timer.Elapsed < timeout)
            {
                if (ExecuteBuffer(log, 1) != 1) { break; }
            }

            if (timer.Elapsed > timeout)
            {
                log.Warning(_lt, m => m("Timeout exceeded while executing buffer"));
                return false;
            }
            else
            {
                return true;
            }
        }

        public int ExecuteBuffer(ILogStream log, int count)
        {
            int oldCount = count;
            Action<ILogStream> nextAction;
            while (_messageQueue.TryDequeue(out nextAction) && count > 0)
            {
                nextAction(log);
                count -= 1;
            }

            return oldCount - count;
        }

        protected void BufferedAction(Action<ILogStream> action)
        {
            BufferedAction((l, c) => action(l));
        }
        protected virtual void BufferedAction(Action<ILogStream,ILoggingContext> action)
        {
            // A buffered log stream is required to be thread safe, and must properly
            // close over the call context from one thread and transfer it to a separate
            // thread (and the call context is thread local storage). To achieve this
            // we create a full capture of the call context from each log message,
            // and import that data into the call context on the other thread
            // at the time the log message is being evaluated. This is probably a
            // pretty heavy performance penalty so an option to opt-out might be beneficial
            // if somebody knows they don't want this. 
            List<KeyValuePair<string, object>> keys = LoggingCallContextStore.ExportKeys();
            Action <ILogStream> act2 = l =>
            {
                var tok = keys.Single(kvp => kvp.Key == BUFFERED_LOGTOKEN_KEY).Value.ToString();
                l.OnAttachScopeParameters(LogManager.GetToken(tok), keys);
                var ctx = LoggingCallContextStore.Push(keys);
                action(l, ctx);
                LoggingCallContextStore.Pop(ctx);
            };

            _messageQueue.Enqueue(act2);
        }
        
        private string BUFFERED_LOGTOKEN_KEY = Guid.NewGuid().ToString("N");
        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            // we attach the logging token to the scope and then we can reuse it when attaching the
            // scope parameters to each BufferedAction() closure
            parameters.Add(new KeyValuePair<string, object>(BUFFERED_LOGTOKEN_KEY, lt.Name));
        }
        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            var cls = CreateMessageClosure(msgBuilder);

            BufferedAction((l,c) => l.BeginScope(c, t, msgBuilder));
        }
        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            var cls = CreateMessageClosure(msgBuilder);

            BufferedAction((l, c) => l.EndScope(c, t, msgBuilder));
        }
        
        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Debug(t, msgBuilder));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Debug(t, msgBuilder));
        }
        
        
        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Info(t, msgBuilder));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Info(t, msgBuilder));
        }
        
        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Audit(t, msgBuilder));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Audit(t, msgBuilder));
        }
        
        
        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Warning(t, msgBuilder));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Warning(t, msgBuilder));
        }
        
        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Error(t, msgBuilder));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Error(t, msgBuilder));
        }
        

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Alert(t, msgBuilder));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Alert(t, msgBuilder));
        }
        
        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            BufferedAction(l => l.IncrementCounterBy(ct, value));
        }
        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            BufferedAction(l => l.SetCounterValue(ct, value));
        }
        
        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            BufferedAction(l => l.IncrementCounterBy(lt, ct, value));
        }
        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            BufferedAction(l => l.SetCounterValue(lt, ct, value));
        }
        
        public void Dispose()
        {
        }
    }
}
