using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressiveLogging.V3
{
    public delegate void CompleteLogMessage(Exception e, int? uniqueness, string message, params object[] format);

    public interface ILogStream : IDisposable
    {
        void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters);
        void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters);

        void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder);

        void IncrementCounterBy(ICounterToken ct, long value);
        void SetCounter(ICounterToken ct, long value);
    }    
}
