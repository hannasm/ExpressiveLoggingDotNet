using System;

namespace ExpressiveLogging.V3.Context
{
    public class LoggingCallContextActiveFrameData
    {
        private string _key;

        public LoggingCallContextActiveFrameData(string key)
        {
            this._key = key;
        }

        public static LoggingCallContextActiveFrameData Initialize(LoggingCallContextStackFrame frame, string key)
        {
            FrameStack(key).Push(frame.FrameID);

            return new LoggingCallContextActiveFrameData(key);
        }

        private static CallContextStack<string> FrameStack(string key)
        {
            return new CallContextStack<string>(String.Format("{0}.ActiveFrame[{1}]", LoggingCallContextStore.CALL_CONTEXT_ROOT, key));
        }

        public bool TryGetValue(out object result)
        {
            LoggingCallContextStackFrame frame;
            if (!TryGetFrame(out frame))
            {
                result = null;
                return false;
            }

            return frame.GetValue(_key, out result);
        }

        public bool TryGetFrame(out LoggingCallContextStackFrame result)
        {
            string frameID;
            if (!FrameStack(_key).TryPeek(out frameID))
            {
                result = null;
                return false;
            }

            result = new LoggingCallContextStackFrame(frameID);
            return true;
        }

        public bool Pop()
        {
            if (!FrameStack(_key).Pop())
            {
                return false;
            }

            string frameID;
            if (!FrameStack(_key).TryPeek(out frameID))
            {
                return false;
            }

            return true;
        }
    }
}
