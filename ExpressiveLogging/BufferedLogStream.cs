using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressiveLogging.V3
{
    /// <summary>
    /// Buffers all logging actions as they are executed,
    /// these buffered action can then be invoked at a later point in time.
    /// </summary>
    public class BufferedLogStream : ILogStream, IBufferLogStream
    {
        private ConcurrentQueue<Action<ILogStream>> _messageQueue = new ConcurrentQueue<Action<ILogStream>>();
        private static readonly ILogToken _lt = LogManager.GetToken();

        public BufferedLogStream()
        {
        }

        private Action<CompleteLogMessage> CreateMessageClosure(Action<CompleteLogMessage> msgBuilder)
        {
            return LogMessageClosureTool.CreateMessageClosure(msgBuilder);
        }

        public bool ExecuteBuffer(ILogStream log, TimeSpan timeout, uint? count)
        {
            Stopwatch timer = Stopwatch.StartNew();
            uint totalCount = 0;
            while (timer.Elapsed < timeout)
            {
              var localCount = ExecuteBuffer(log, 1);
              if (localCount == 0) { break; }
              totalCount += localCount;
              if (totalCount >= count) { break; }
            }

            if (timer.Elapsed > timeout)
            {
                LogManager.Warning.Write(_lt, m => m("Timeout exceeded while executing buffer"));
                return false;
            }
            else
            {
                return true;
            }
        }

        public uint ExecuteBuffer(ILogStream log, uint count)
        {
            uint oldCount = count;
            Action<ILogStream> nextAction; 
            while (count > 0 && _messageQueue.TryDequeue(out nextAction))
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
                var x = keys.Where(kvp => kvp.Key == BUFFERED_LOGTOKEN_KEY);
                if (x.Any())
                {
                    var tok = x.Single().Value.ToString();
                    l.OnAttachScopeParameters(LogManager.GetToken(tok), keys);
                }
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
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        
        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder = CreateMessageClosure(msgBuilder);

            BufferedAction(l => l.Write(t, msgBuilder));
        }

        public void IncrementCounterBy(ICounterToken ct, long value)
        {
            BufferedAction(l => l.IncrementCounterBy(ct, value));
        }
        public void SetCounter(ICounterToken ct, long value)
        {
            BufferedAction(l => l.SetCounter(ct, value));
        }
        
        public void Dispose()
        {
        }
    }
}
