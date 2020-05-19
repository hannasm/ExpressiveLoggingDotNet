using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.V3.Context;
using System.Management.Automation;

namespace ExpressiveLogging.V3.PowershellV5Logging
{
    /// <summary>
    /// This log stream allows emitting log messages through the powershell logging channels.
    /// It is not thread safe. It may only be used from the thread that the powershell commandlet
    /// has been executed in. It also may only be used from inside
    /// the BeginProcessing() / ProcessRecords() / EndProcessing()
    /// methods.
    /// </summary>
    public abstract class CmdletLogStream : ILogStream
    {

        protected readonly Cmdlet _target;
        public CmdletLogStream(Cmdlet target)
        {
            _target = target;
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }

        public void Dispose()
        {
        }
        
        public void IncrementCounterBy(ICounterToken ct, long value)
        {
        }        

        public void SetCounter(ICounterToken ct, long value)
        {
        }

        public abstract void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder);

    }

    public class CmdletErrorLogStream  : CmdletLogStream {
        public CmdletErrorLogStream(Cmdlet target) : base(target)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteError(new ErrorRecord(new Exception(string.Format(m, f), e), t.Symbol + "(" + u + ")", ErrorCategory.NotSpecified, null));
            });
        }
    }
    public class CmdletInformationLogStream  : CmdletLogStream {
        public CmdletInformationLogStream(Cmdlet target) : base(target)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteInformation(string.Format(m, f), new[] { "uniquenessCode(" + u + ")" });
                _target.WriteInformation(e, null);
            });
        }
    }
    public class CmdletDebugLogStream  : CmdletLogStream {
        public CmdletDebugLogStream(Cmdlet target) : base(target)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteDebug(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }
    }
    public class CmdletVerboseLogStream  : CmdletLogStream {
        public CmdletVerboseLogStream(Cmdlet target) : base(target)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteVerbose(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }
    }
    public class CmdletWarningLogStream  : CmdletLogStream {
        public CmdletWarningLogStream(Cmdlet target) : base(target)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) =>
            {
                _target.WriteWarning(string.Format(m, f) + Environment.NewLine + e.ToString());
            });
        }
    }
}
