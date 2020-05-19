using ExpressiveLogging.V3;
using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressiveLogging.V3
{
    /// <summary>
    /// Implements the ILogger interface but provides no functionality
    /// </summary>
    public class NullLogStream : ILogStream
    {
        public void Dispose()
        {
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }
        public void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
        }

        public void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
        }


        public void IncrementCounterBy(ICounterToken ct, long value)
        {
        }
        public void SetCounter(ICounterToken ct, long value)
        {
        }      
    }
}
