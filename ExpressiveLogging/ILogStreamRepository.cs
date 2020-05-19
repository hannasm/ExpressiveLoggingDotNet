using System;

namespace ExpressiveLogging.V3 {
  public interface ILogStreamRepository : IDisposable {
    ILogStream GetLogger();
  }
}
