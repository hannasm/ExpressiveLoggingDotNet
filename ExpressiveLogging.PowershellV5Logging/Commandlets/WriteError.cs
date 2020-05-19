using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.PowershellV5Logging.Commandlets
{
    [Cmdlet(VerbsCommunications.Write, "ExpressiveError")]
    public class WriteError : LoggingCommandletBase
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        protected override ILogToken GetDefaultLogToken()
        {
            return _lt;
        }

        protected override ILogStream GetLogger() {
          return LogManager.Error;
        }
    }
}
