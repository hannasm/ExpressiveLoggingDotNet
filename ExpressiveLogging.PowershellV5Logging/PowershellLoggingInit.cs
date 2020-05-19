using System;
using System.Management.Automation;
namespace ExpressiveLogging.V3.PowershellV5Logging
{
    public class PowershellLoggingInit : IDisposable {

      ILogStream _error, _warning, _info, _verbose, _debug;
      public PowershellLoggingInit(Cmdlet cmd) {
        _error = new CmdletErrorLogStream(cmd);
        _warning = new CmdletWarningLogStream(cmd);  
        _info = new CmdletInformationLogStream(cmd);  
        _verbose = new CmdletVerboseLogStream(cmd); 
        _debug = new CmdletDebugLogStream(cmd);  

        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_ALERT).Add(_error);
        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_ERROR).Add(_error);
        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_WARNING).Add(_warning);  
        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_AUDIT).Add(_info);  
        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_INFO).Add(_verbose);  
        StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_DEBUG).Add(_debug);  
      }

        public void Dispose()
        {
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_ALERT).Remove(_error);
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_ERROR).Remove(_error);
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_WARNING).Remove(_warning);  
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_AUDIT).Remove(_info);  
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_INFO).Remove(_verbose);  
          StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_DEBUG).Remove(_debug);  

          _error.Dispose(); _warning.Dispose(); _info.Dispose(); _verbose.Dispose(); _debug.Dispose();
        }
    }
}
