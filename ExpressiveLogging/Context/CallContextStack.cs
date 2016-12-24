using System;
using System.Runtime.Remoting.Messaging;

namespace ExpressiveLogging.Context
{
    /// <summary>
    /// Implements basic stack functionality and ensures that all stack
    /// buckets are cloned when flowed to another context
    /// </summary>
    public class CallContextStack<T>
    {
        private readonly string _name;
        private readonly string _size;
        public CallContextStack(string name)
        {
            _name = name;
            _size = String.Format("{0}.{1}", _name, "size");

            var size = CallContext.LogicalGetData(_size);
            if (size == null)
            {
                CallContext.LogicalSetData(_size, 0);
            }
        }

        private int Length()
        {
            return (int)CallContext.LogicalGetData(_size);
        }
        private string IndexName(int len)
        {
            return String.Format("{0}.{1}[{2}]", _name, "frame", len);
        }

        public void Push(T data)
        {
            int len = Length();
            var name = IndexName(len);
            CallContext.LogicalSetData(name, data);
            CallContext.LogicalSetData(_size, len + 1);
        }

        public bool Pop()
        {
            var len = Length();
            if (len <= 0)
            {
                return false;
            }
            len = len -1;

            CallContext.FreeNamedDataSlot(IndexName(len));
            CallContext.LogicalSetData(_size, len);
            return true;
        }

        public bool TryPeek(out T data)
        {
            int len = Length() - 1;
            var name = IndexName(len);

            var result = CallContext.LogicalGetData(name);
            if (result == null) { 
                data = default(T);
                return false; 
            }
            data = (T)result;
            return true;
        }
    }
}