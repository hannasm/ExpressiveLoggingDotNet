using System;

namespace ExpressiveLogging.V3 {
  public interface IBufferLogStream {
        /// <summary>
        /// Execute as many events from the buffer in the given timeout interval 
        /// on the specified logger
        /// </summary>
        /// <return>
        /// true if buffer was empty, false if timeout was reached
        /// </return>
        bool ExecuteBuffer(ILogStream log, TimeSpan timeout, uint? count);
        /// <summary>
        /// Execute a fixed count number of events on the buffer. If the buffer becomes empty before
        /// reaching that count exit.
        /// </summary>
        /// <return>the number of events executed</return>
        uint ExecuteBuffer(ILogStream log, uint count);
        /// <summary>Total number of elements in the buffer</summary>
        int BufferSize { get; }
  }
}
