using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;

namespace ExpressiveLogging.Context
{
    public static class LoggingCallContextStore
    {
        internal static readonly string CALL_CONTEXT_ROOT = typeof(LoggingCallContextStore).Name;
        internal static readonly string SERIALIZATION_KEY = string.Format("{0}.{1}", CALL_CONTEXT_ROOT, "Serialization");

        internal class LoggingContext : ILoggingContext
        {
            public readonly Guid UID = Guid.NewGuid();
        }

        internal static string GetFrameID(ILoggingContext context)
        {
            LoggingContext ctx = (LoggingContext)context;
            return ctx.UID.ToString("N");
        }

        /// <summary>
        /// Retrieves the current active value for the key
        /// </summary>
        public static bool TryGetContextData(string key, out object result)
        {
            LoggingCallContextActiveFrameData data = new LoggingCallContextActiveFrameData(key);
            return data.TryGetValue(out result);
        }
        /// <summary>
        /// Retrieves the current active value for the key
        /// </summary>
        public static bool TryGetContextData<T>(string key, out T result)
        {
            object innerResult;
            var found = TryGetContextData(key, out innerResult);
            if (found)
            {
                result = (T)innerResult;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }
        public static bool TryGetContextData(ILoggingContext ctx, string key, out object result)
        {
            var frame = new LoggingCallContextStackFrame(GetFrameID(ctx));
            if (!frame.IsActive())
            {
                result = null;
                return false;
            }

            return frame.GetValue(key, out result);
        }
        public static bool TryGetContextData<T>(ILoggingContext ctx, string key, out T result)
        {
            object innerResult;
            var found = TryGetContextData(ctx, key, out innerResult);
            if (found)
            {
                result = (T)innerResult;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        public static List<KeyValuePair<string, object>> ExportKeys()
        {
            List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            foreach (var key in Keys().GetData())
            {
                object val;
                if (TryGetContextData(key, out val))
                {
                    result.Add(new KeyValuePair<string, object>(key, val));
                }
            }

            return result;
        }

        private static CallContextSet<string> Keys()
        {
            return new CallContextSet<string>(CALL_CONTEXT_ROOT + ".activeKeys");
        }

        public static ILoggingContext Push(IEnumerable<KeyValuePair<string, object>> data)
        {
            var context = new LoggingContext();
            var frame = LoggingCallContextStackFrame.Initialize(context, data);

            using (var keySet = Keys().GetEditor())
            {
                foreach (var key in frame.Keys)
                {
                    LoggingCallContextActiveFrameData.Initialize(frame, key);
                    keySet.Add(key);
                }

                CallContext.LogicalSetData(SERIALIZATION_KEY, SerializeToXML(keySet.DataStore()));
            }

            return context;
        }

        public static void Pop(ILoggingContext context)
        {
            var frameID = GetFrameID(context);

            var frame = new LoggingCallContextStackFrame(frameID);

            if (!frame.Deactivate()) { return; }

            using (var keySet = Keys().GetEditor())
            {
                foreach (var key in frame.Keys)
                {
                    var activeFrameData = new LoggingCallContextActiveFrameData(key);

                    LoggingCallContextStackFrame currentFrame;
                    while (activeFrameData.TryGetFrame(out currentFrame) &&
                            !currentFrame.IsActive())
                    {
                        currentFrame.FreeKey(key);

                        if (!activeFrameData.Pop())
                        {
                            keySet.Remove(key);
                            break;
                        }
                    }
                }

                CallContext.LogicalSetData(SERIALIZATION_KEY, SerializeToXML(keySet.DataStore()));
            }
        }

        /// <summary>
        /// Bring back an xml version of the active context data,
        /// a cached version will be brought back under normal
        /// circumstances, and a special un-cached version will be used
        /// </summary>
        public static string SerializeToXML()
        {
            var contextSerialization = (string)CallContext.LogicalGetData(SERIALIZATION_KEY);
            if (contextSerialization == null)
            {
                contextSerialization = SerializeToXML(Keys().GetData());
                CallContext.LogicalSetData(SERIALIZATION_KEY, contextSerialization);
            }
            return contextSerialization;
        }

        /// <summary>
        /// Enumerate the keys stored in the context and
        /// serialize them to an XML blob
        /// </summary>
        private static string SerializeToXML(IEnumerable<string> keyCollection)
        {
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            using (var xw = new XmlTextWriter(tw))
            {
                xw.WriteStartElement("loggingContext");
                foreach (var key in keyCollection)
                {
                    var frameData = new LoggingCallContextActiveFrameData(key);
                    object data;
                    xw.WriteStartElement(key);
                    if (frameData.TryGetValue(out data) && data != null)
                    {
                        xw.WriteString(data.ToString());
                    }
                    else
                    {
                        xw.WriteString("(Unknown)");
                    }
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
            }
            return sb.ToString();
        }
    }
}
