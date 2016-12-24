using ExpressiveLogging.CompositeLogging;
using ExpressiveLogging.StreamFormatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.PowershellV5Logging.Commandlets
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

        protected virtual ILogStream GetLogger()
        {
            List<ILogStream> results = new List<ILogStream>(); 
            
            results.Add(new ExceptionFormatterLogStream(
                        new DefaultTextLogStreamFormatter(
                            new CmdletLogStream(this)
            )));

            var globalStream = this.GetVariableValue("ExpressiveStreamTo");
            if (globalStream != null && globalStream is ILogStream)
            {
                results.Add((ILogStream)globalStream);
            }

            if (StreamTo != null)
            {
                results.Add(StreamTo);
            }

            if (results.Count > 1)
            {
                return CompositeLogStream.Create(results.ToArray());
            }
            else
            {
                return results[0];
            }
        }
        protected abstract ILogToken GetDefaultLogToken();
        protected abstract void WriteMessage(ILogStream log, ILogToken token);

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

            WriteMessage(GetLogger(), token);
        }
    }
}
