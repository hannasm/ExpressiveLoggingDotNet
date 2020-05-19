using System.Collections.Generic;

namespace ExpressiveLogging.V3 {
  public class ScopedActionLogStream : DelegatingLogStream
  {
      public ScopedActionLogStream(
          ScopedAction onAttach,
          ScopedAction onDetach,
          ILogStream inner) : base(inner) {
          _onAttach = onAttach;
          _onDetach = onDetach;
      }
      public ScopedActionLogStream(
          ScopedAction onAttach,
          ScopedAction onDetach) : base(new NullLogStream()) {
          _onAttach = onAttach;
          _onDetach = onDetach;
      }

      public delegate void ScopedAction (ILogToken lt, List<KeyValuePair<string, object>> parameters);
      ScopedAction _onAttach, _onDetach;

      public override void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
      {
          _inner.OnAttachScopeParameters(lt, parameters);
          _onAttach(lt, parameters);
      }
      public override void OnDetachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
      {
          _inner.OnDetachScopeParameters(lt, parameters);
          _onDetach(lt, parameters);
      }
  }
}
