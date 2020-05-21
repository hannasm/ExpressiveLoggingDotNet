using System;

namespace ExpressiveLogging.V3 {
  public static class BufferLogStreamExtensions {
    /// <summary>
    /// Execute buffered stream with default timeout of 10 milliseconds. If buffer becomes empty
    /// return immediately.
    /// </summary>
    /// <return>
    /// true if buffer was empty, false if timeout was reached
    /// </return>
    public static bool ExecuteBuffer(this IBufferLogStream buffer, ILogStream stream) {
      return buffer.ExecuteBuffer(stream, TimeSpan.FromMilliseconds(10), null);
    }
    /// <summary>
    /// Execute as many events from buffered stream within specified timeout. If buffer becomes empty
    /// return immediately.
    /// </summary>
    /// <return>
    /// true if buffer was empty, false if timeout was reached
    /// </return>
    public static bool ExecuteBuffer(this IBufferLogStream buffer, ILogStream stream, TimeSpan timeout) {
      return buffer.ExecuteBuffer(stream, timeout, null);
    }
  }
}
