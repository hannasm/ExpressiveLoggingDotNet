using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    public static class StaticLogRepository
    {
        static ThreadedLogStreamRepository _repo = new ThreadedLogStreamRepository(() => { throw new NotSupportedException("You must call StaticLogRepository.Init() before creating loggers"); });

        public static ILogStream GetLogger()
        {
            return _repo.GetLogger();
        }

        public static void Init(Func<ILogStream> factory)
        {
            _repo = new ThreadedLogStreamRepository(factory);
        }
    }
}
