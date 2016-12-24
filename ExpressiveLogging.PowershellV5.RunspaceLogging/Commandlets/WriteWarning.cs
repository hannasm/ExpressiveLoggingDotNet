using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.PowershellV5Logging.Commandlets
{
    [Cmdlet(VerbsCommunications.Write, "ExpressiveWarning")]
    public class WriteWarning : LoggingCommandletBase
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        protected override ILogToken GetDefaultLogToken()
        {
            return _lt;
        }

        protected override void WriteMessage(ILogStream log, ILogToken token)
        {
            if (Exception != null && UniquenessCode != null)
            {
                log.Warning(token, m => m(Exception, UniquenessCode.Value, Message, Format));
            }
            else if (Exception != null)
            {
                log.Warning(token, m => m(Exception, Message, Format));
            }
            else if (UniquenessCode != null)
            {
                log.Warning(token, m => m(UniquenessCode.Value, Message, Format));
            }
            else
            {
                log.Warning(token, m => m(Message, Format));
            }
        }
    }
}
