using System.Collections.Generic;
using System.Linq;

namespace ExpressiveLogging.Context
{
    public class LoggingCallContextStackFrame
    {
        public LoggingCallContextStackFrame(string frameID)
        {
            FrameID = frameID;
        }

        public static LoggingCallContextStackFrame Initialize(ILoggingContext context, IEnumerable<KeyValuePair<string, object>> data)
        {
            string FrameID = LoggingCallContextStore.GetFrameID(context);

            var valueStore = ValueStore(FrameID);
            List<string> keys = new List<string>();
            foreach (var pair in data)
            {
                keys.Add(pair.Key);
                valueStore.AddOrUpdate(pair.Key, ()=>pair.Value, (o)=>pair.Value);
            }
            ActiveFramesMap().AddOrUpdate(FrameID, ()=>"active", (o)=>"active");
            KeysStore().AddOrUpdate(FrameID, () => keys, (o) => keys);

            return new LoggingCallContextStackFrame(FrameID);
        }
        public readonly string FrameID;

        public IEnumerable<string> Keys
        {
            get
            {
                IEnumerable<string> result;
                if (!KeysStore().TryGetValue(FrameID, out result))
                {
                    return Enumerable.Empty<string>();
                }
                return result;
            }
        }
        public bool GetValue(string key, out object value)
        {
            return ValueStore(FrameID).TryGetValue(key, out value);
        }
        public bool GetValue<T>(string key, out T value)
        {
            object innerValue;
            if (!GetValue(key, out innerValue))
            {
                value = default(T);
                return false;
            }
            else
            {
                value = (T)innerValue;
                return true;
            }
        }

        private static CallContextDictionary<IEnumerable<string>> KeysStore()
        {
            return new CallContextDictionary<IEnumerable<string>>(LoggingCallContextStore.CALL_CONTEXT_ROOT + ".Keys");
        }
        private static CallContextDictionary<object> ValueStore(string frameID)
        {
            return new CallContextDictionary<object>(LoggingCallContextStore.CALL_CONTEXT_ROOT + "." + frameID + ".Values");
        }
        private static CallContextDictionary<string> ActiveFramesMap()
        {
            return new CallContextDictionary<string>(LoggingCallContextStore.CALL_CONTEXT_ROOT + ".ActiveFramesMap");
        }

        public bool Deactivate()
        {
            string result;
            return ActiveFramesMap().TryRemove(FrameID, out result);
        }
        public bool IsActive()
        {
            string result;
            return ActiveFramesMap().TryGetValue(FrameID, out result);
        }

        public void FreeKey(string key)
        {
            var store = ValueStore(FrameID);
            store.Remove(key);

            IEnumerable<string> keys;
            if (KeysStore().TryGetValue(FrameID, out keys))
            {
                keys = keys.Where(k => k != key).ToList();
                if (keys.Any())
                {
                    KeysStore().AddOrUpdate(FrameID, () => keys, (o) => keys);
                }
                else
                {
                    KeysStore().Remove(FrameID);
                }
            }
        }
    }
}