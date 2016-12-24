using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;

namespace ExpressiveLogging.StreamFormatters
{
    public class ExceptionFormatterLogStream : ILogStream
    {
        ILogStream _inner;
        public ExceptionFormatterLogStream(ILogStream inner)
        {
            _inner = inner;

            AddFormatter(typeof(AggregateException), AggregateExceptionRenderer);
        }

        public delegate void ExceptionRendererCallback(StringBuilder buffer, Exception exc, string indent, ExceptionPropertyRenderer message, ExceptionPropertyRenderer stackTrace, ExceptionRendererCallback recursive);
        public delegate void ExceptionPropertyRenderer(StringBuilder buffer, Exception exc, string indent);
        
        public ExceptionFormatterLogStream AddFormatter<T>(ExceptionRendererCallback callback)
        {
            _renderers.Add(new ExceptionRenderer(typeof(T), callback));
            return this;
        }
        public ExceptionFormatterLogStream AddFormatter(Type t, ExceptionRendererCallback callback)
        {
            _renderers.Add(new ExceptionRenderer(t, callback));
            return this;
        }

        class ExceptionRenderer
        {
            public ExceptionRenderer(Type crit, ExceptionRendererCallback callback)
            {
                Criteria = crit;
                Callback = callback;
            }

            public Type Criteria;
            public ExceptionRendererCallback Callback;
        }
        List<ExceptionRenderer> _renderers = new List<ExceptionRenderer>();

        public static void AggregateExceptionRenderer(StringBuilder buffer, Exception exc, string indent, ExceptionPropertyRenderer message, ExceptionPropertyRenderer stackTrace, ExceptionRendererCallback recursive)
        {
            var ag = exc as AggregateException;
            if (ag == null) { throw new InvalidCastException("Expected AggregateException but got " + exc.GetType().AssemblyQualifiedName); }

            buffer.Append(indent);
            message(buffer, exc, indent);
            buffer.AppendLine();

            buffer.Append(indent);
            stackTrace(buffer, exc, indent);
            buffer.AppendLine();
            
            if (ag.InnerExceptions != null)
            {
                foreach (var inner in ag.InnerExceptions)
                {
                    buffer.Append(indent);
                    buffer.Append("inner exception: ");
                    buffer.AppendLine();

                    recursive(buffer, exc.InnerException, indent + MORE_INDENT, message, stackTrace, recursive);
                }
            }
        }

        public const string MORE_INDENT = "  ";
        public static void DefaultRenderer(StringBuilder buffer, Exception exc, string indent, ExceptionPropertyRenderer message, ExceptionPropertyRenderer stackTrace, ExceptionRendererCallback recursive)
        {
            buffer.Append(indent);
            message(buffer, exc, indent);
            buffer.AppendLine();

            buffer.Append(indent);
            stackTrace(buffer, exc, indent);
            buffer.AppendLine();

            if (exc.InnerException != null)
            {
                buffer.Append(indent);
                buffer.Append("inner exception: ");
                buffer.AppendLine();

                recursive(buffer, exc.InnerException, indent + MORE_INDENT, message, stackTrace, recursive);
            }
        }

        public void RecursiveRenderer(StringBuilder buffer, Exception exc, string indent, ExceptionPropertyRenderer message, ExceptionPropertyRenderer stackTrace, ExceptionRendererCallback recursive)
        {
            if (exc == null) { return; }

            for (int i = 0, n = _renderers.Count; i < n; i++)
            {
                var r = _renderers[i];

                if (exc.GetType().IsInstanceOfType(r.Criteria))
                {
                    r.Callback(buffer, exc, indent, message, stackTrace, recursive);
                    return;
                }
            }

            DefaultRenderer(buffer, exc, indent, message, stackTrace, recursive);
        }

        void GetExceptionMessage(StringBuilder buffer, Exception exc, string indent)
        {
            if (exc == null) { return; }

            buffer.Append(indent);
            buffer.AppendFormat("--> {0} (of type {1})\r\n", exc.Message, exc.GetType().FullName);
            buffer.AppendLine();
        }
        void GetStackTrace(StringBuilder buffer, Exception exc, string indent)
        {
            if (exc == null) { return; }

            var frames = new System.Diagnostics.StackTrace(exc, true).GetFrames();
            for (int i = 0; i < frames.Length; i++) /* Ignore current StackTraceToString method...*/
            {
                var currFrame = frames[i];
                var method = currFrame.GetMethod();
                buffer.Append(string.Format("{0}:{1} ({2} [line {3} column {4}])\r\n",
                    method.ReflectedType != null ? method.ReflectedType.Name : string.Empty,
                    method.Name,
                    currFrame.GetFileName(),
                    currFrame.GetFileLineNumber(),
                    currFrame.GetFileColumnNumber()));
            }
        }

        public Exception FormatException(Exception ex)
        {
            var buffer = new StringBuilder();
            RecursiveRenderer(buffer, ex, string.Empty, this.GetExceptionMessage, this.GetStackTrace, this.RecursiveRenderer);

            return new RenderedException(ex, buffer.ToString());
        }

        public class RenderedException : Exception
        {
            public string RenderedText;
            public Exception Original;

            public RenderedException(Exception original, string txt)
            {
                this.Original = original;
                this.RenderedText = txt;
            }

            public override string ToString()
            {
                return RenderedText;
            }
        }

        public void OnAttachScopeParameters(ILogToken lt, List<KeyValuePair<string, object>> parameters)
        {
            _inner.OnAttachScopeParameters(lt, parameters);
        }

        public void BeginScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.BeginScope(ctx, t, msgBuilder);
        }

        public void EndScope(ILoggingContext ctx, ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.EndScope(ctx, t, msgBuilder);
        }

        public void Debug(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Debug(t, g => g(FormatException(e), m, f)));
        }

        public void Debug(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Debug(t, msgBuilder);
        }

        public void Debug(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Debug(t, g => g(FormatException(e), u, m, f)));
        }

        public void Debug(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Debug(t, msgBuilder);
        }

        public void Info(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Info(t, g => g(FormatException(e), m, f)));
        }

        public void Info(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Info(t, msgBuilder);
        }

        public void Info(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Info(t, g => g(FormatException(e), u, m, f)));
        }

        public void Info(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Info(t, msgBuilder);
        }

        public void Audit(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Audit(t, g => g(FormatException(e), m, f)));
        }

        public void Audit(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Audit(t, msgBuilder);
        }

        public void Audit(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Audit(t, g => g(FormatException(e), u, m, f)));
        }

        public void Audit(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Audit(t, msgBuilder);
        }

        public void Warning(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Warning(t, g => g(FormatException(e), m, f)));
        }

        public void Warning(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Warning(t, msgBuilder);
        }

        public void Warning(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Warning(t, g => g(FormatException(e), u, m, f)));
        }

        public void Warning(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Warning(t, msgBuilder);
        }

        public void Error(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Error(t, g => g(FormatException(e), m, f)));
        }

        public void Error(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Error(t, msgBuilder);
        }

        public void Error(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Error(t, g => g(FormatException(e), u, m, f)));
        }

        public void Error(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Error(t, msgBuilder);
        }

        public void Alert(ILogToken t, Action<LogExceptionMessage> msgBuilder)
        {
            msgBuilder((e, m, f) => _inner.Alert(t, g => g(FormatException(e), m, f)));
        }

        public void Alert(ILogToken t, Action<LogFormatMessage> msgBuilder)
        {
            _inner.Alert(t, msgBuilder);
        }

        public void Alert(ILogToken t, Action<LogExceptionMessageWithCustomUniqueness> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Alert(t, g => g(FormatException(e), u, m, f)));
        }

        public void Alert(ILogToken t, Action<LogFormatMessageWithCustomUniqueness> msgBuilder)
        {
            _inner.Alert(t, msgBuilder);
        }

        public void IncrementCounterBy(IRawCounterToken ct, long value)
        {
            _inner.IncrementCounterBy(ct, value);
        }

        public void SetCounterValue(IRawCounterToken ct, long value)
        {
            _inner.SetCounterValue(ct, value);
        }

        public void IncrementCounterBy(ILogToken lt, INamedCounterToken ct, long value)
        {
            _inner.IncrementCounterBy(lt, ct, value);
        }

        public void SetCounterValue(ILogToken lt, INamedCounterToken ct, long value)
        {
            _inner.SetCounterValue(lt, ct, value);
        }

        public void Dispose()
        {
            _inner.Dispose();
        }
    }
}
