using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.PowershellV5Logging.Commandlets
{
    [Cmdlet(VerbsCommunications.Write, "ExpressiveInfo")]
    public class WriteInfo : LoggingCommandletBase
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
                log.Info(token, m => m(Exception, UniquenessCode.Value, Message, Format));
            }
            else if (Exception != null)
            {
                log.Info(token, m => m(Exception, Message, Format));
            }
            else if (UniquenessCode != null)
            {
                log.Info(token, m => m(UniquenessCode.Value, Message, Format));
            }
            else
            {
                log.Info(token, m => m(Message, Format));
            }
        }
    }
}
