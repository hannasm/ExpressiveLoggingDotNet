using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressiveLogging.V3
{
    /// <summary>
    /// Merge multiple buffered streams to a single stream
    /// </summary>
    public class MergeBufferLogStream : IBufferLogStream
    {
        static readonly ILogToken _lt = LogManager.GetToken();
        readonly ConcurrentDictionary<IBufferLogStream, IBufferLogStream> _buffers;
        
        public MergeBufferLogStream() : this(Enumerable.Empty<BufferedLogStream>())
        {}
        public MergeBufferLogStream(IEnumerable<IBufferLogStream> streams)
        {
          _buffers = new ConcurrentDictionary<IBufferLogStream, IBufferLogStream>(streams.ToDictionary(s=>s, s=>s));
        }

        /// <summary>
        /// Execute as many actions from the buffer in the given timeout interval 
        /// on the specified logger
        /// </summary>
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
          uint total = 0;
          foreach (var buf in _buffers.Values) {
            total += buf.ExecuteBuffer(log, count);
          }
          return total;
        }

        public void AddBuffer(IBufferLogStream buffer) {
          _buffers.TryAdd(buffer, buffer);
        }
        public BufferedLogStream CreateAndAddBuffer() {
          var result = new BufferedLogStream();
          AddBuffer(result);
          return result;
        }
    }
}
