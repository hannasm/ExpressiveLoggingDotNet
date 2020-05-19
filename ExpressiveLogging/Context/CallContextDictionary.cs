using System;

#if ASYNC_LOCAL
using CallContext = ExpressiveLogging.V3.Context.CallContextStore;
#else 
using System.Runtime.Remoting.Messaging;
#endif

namespace ExpressiveLogging.V3.Context
{
    /// <summary>
    /// Implements basic dictionary functionality that uses the CallContext
    /// as storage medium, and the dictionary buckets are properly cloned when 
    /// call context is flowed to a different scope
    /// </summary>
    public class CallContextDictionary<T>
    {
        private readonly string _name;
        public CallContextDictionary(string name)
        {
            this._name = name;
        }

        //private string CountKey()
        //{
        //    return string.Format("{0}[{1}]", _name, "count");
        //}
        //public int Count()
        //{
        //    var data = CallContext.LogicalGetData(CountKey());
        //    if (data == null) { return 0; }
        //    return (int)data;
        //}
        //private void IncrementCount()
        //{
        //    CallContext.LogicalSetData(CountKey(), Count() + 1);
        //}
        //private void DecrementCount()
        //{
        //    int count = Count();
        //    if (count > 1)
        //    {
        //        CallContext.LogicalSetData(CountKey(), count - 1);
        //    }
        //    else
        //    {
        //        CallContext.FreeNamedDataSlot(CountKey());
        //    }

        //}
        public void AddOrUpdate<T>(string key, Func<T> newDataFunc, Func<T, T> updateDataFunc)
        {
            var name = String.Format("{0}.{1}", _name, key);
            var data = CallContext.LogicalGetData(name);
            if (data == null)
            {
                var newData = newDataFunc();
                if (newData == null)
                {
                    throw new InvalidOperationException("Cannot insert null");
                }
                CallContext.LogicalSetData(name, newData);
                //IncrementCount();
            }
            else
            {
                var updateData = updateDataFunc((T)data);
                if (updateData == null)
                {
                    throw new InvalidOperationException("Cannot update null");
                }
                CallContext.LogicalSetData(name, updateData);
            }
        }

        public bool Remove(string key)
        {
            var name = String.Format("{0}.{1}", _name, key);
            var data = CallContext.LogicalGetData(name);
            CallContext.FreeNamedDataSlot(name);
            //DecrementCount();
            return data == null;
        }
        public bool TryRemove(string key, out T result)
        {
            var name = String.Format("{0}.{1}", _name, key);
            var data = CallContext.LogicalGetData(name);
            CallContext.FreeNamedDataSlot(name);
            //DecrementCount();
            if (data == null)
            {
                result = default(T);
                return false;
            }
            else
            {
                result = (T)data;
                return true;
            }
        }


        public bool TryGetValue(string key, out T data)
        {
            var name = String.Format("{0}.{1}", _name, key);
            var innerData = CallContext.LogicalGetData(name);
            if (innerData == null)
            {
                data = default(T);
                return false;
            }
            data = (T)innerData;
            return true;
        }
    }
}
