#if ASYNC_LOCAL
using System;
using System.Threading;
using System.Runtime.CompilerServices;

namespace ExpressiveLogging.V3.Context {
  public static class CallContextStore {
    private static ConditionalWeakTable<string, AsyncLocal<object>> _store = new ConditionalWeakTable<string, AsyncLocal<object>>();

    public static object LogicalGetData(string name) {
      AsyncLocal<object> result;
      if (!_store.TryGetValue(name, out result)) {
        return default(object);
      }
      return result.Value;
    }

    public static void LogicalSetData(string name, object val) {
      var al = _store.GetOrCreateValue(name);
      al.Value = val;
    }

    public static void FreeNamedDataSlot(string name) {
      AsyncLocal<object> result;
      if (_store.TryGetValue(name, out result)) {
        result.Value = default(object);
      }
      _store.Remove(name);
    }
  }
}
#endif
