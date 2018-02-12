using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.PowershellV5Logging.Commandlets
{
    [Cmdlet(VerbsCommunications.Write, "ExpressiveAudit")]
    public class WriteAudit : LoggingCommandletBase
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
                log.Audit(token, m => m(Exception, UniquenessCode.Value, Message, Format));
            }
            else if (Exception != null)
            {
                log.Audit(token, m => m(Exception, Message, Format));
            }
            else if (UniquenessCode != null)
            {
                log.Audit(token, m => m(UniquenessCode.Value, Message, Format));
            }
            else
            {
                log.Audit(token, m => m(Message, Format));
            }
        }
    }
}
