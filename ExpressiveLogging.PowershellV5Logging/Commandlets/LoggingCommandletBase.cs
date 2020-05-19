using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.PowershellV5Logging.Commandlets
{
    public abstract class LoggingCommandletBase : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Token;

        [Parameter(Position = 0, Mandatory = true)]
        public string Message;

        [Parameter(ValueFromRemainingArguments = true)]
        public object[] Format;

        [Parameter(Mandatory = false)]
        public Exception Exception = null;

        [Parameter(Mandatory = false)]
        public int? UniquenessCode;

        [Parameter(Mandatory = false)]
        public ILogStream StreamTo;

        protected abstract ILogStream GetLogger();
        protected abstract ILogToken GetDefaultLogToken();

        PowershellLoggingInit _init;
        protected override void BeginProcessing() {
          base.BeginProcessing();
          _init = new PowershellLoggingInit(this);
        }
        protected override void EndProcessing()
        {
            ILogToken token;
            if (Token != null)
            {
                token = LogManager.GetToken(Token);
            }
            else
            {
                token = GetDefaultLogToken();
            }
            
            if (Format != null && Format.Length > 0)
            {
                Message += " " + string.Join(" ", Format);
            }

            if (UniquenessCode == null)
            {
                UniquenessCode = LogManager.GenerateUniquenessCode(Exception, Message);
            }
            Format = new[] { Message };
            Message = "{0}";

            GetLogger().Write(token, m=>m(Exception, UniquenessCode, Message, Format));

            _init = new PowershellLoggingInit(this);
            base.EndProcessing();
        }
    }
}
