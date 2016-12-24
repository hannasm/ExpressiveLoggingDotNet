using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace ExpressiveLogging.Context
{
    /// <summary>
    /// Implements basic set functionality and ensures that
    /// all of the set's buckets are cloned when flowed to
    /// another context
    /// </summary>
    public class CallContextSet<T>
    {
        private string _name;
        private string _dataKeyName;
        public CallContextSet(string name)
        {
            _name = name;
            _dataKeyName = String.Format("{0}.{1}", _name, "data");

            var data = CallContext.LogicalGetData(_dataKeyName);
            if (data == null)
            {
                Update(new HashSet<T>());
            }
        }

        internal HashSet<T> Data()
        {
            return (HashSet<T>)CallContext.LogicalGetData(_dataKeyName);
        }
        internal void Update(HashSet<T> newData)
        {
            CallContext.LogicalSetData(_dataKeyName, newData);
        }

        public IEnumerable<T> GetData()
        {
            return Data().AsEnumerable();
        }
        public bool Contains(T value)
        {
            return Data().Contains(value);
        }

        public CallContextSetEditor<T> GetEditor()
        {
            return new CallContextSetEditor<T>(this);
        }

        public class CallContextSetEditor<T> : IDisposable
        {
            private HashSet<T> _editorVersion;
            CallContextSet<T> _parent;
            public CallContextSetEditor(CallContextSet<T> parent)
            {
                _editorVersion = new HashSet<T>(parent.Data());
                _parent = parent;
            }

            public IEnumerable<T> DataStore() { return _editorVersion; }

            public bool Add(T data)
            {
                return _editorVersion.Add(data);
            }
            public bool Remove(T data)
            {
                return _editorVersion.Remove(data);
            }

            public void Dispose()
            {
                _parent.Update(_editorVersion);   
            }
        }
    }
}
