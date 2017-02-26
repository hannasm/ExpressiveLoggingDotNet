using ExpressiveAssertions;
using ExpressiveAssertions.Tooling;
using ExpressiveLogging.AssertableLogging;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using ExpressiveLogging.SystemDiagnosticsLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests
{
    [TestClass]
    public class AssertionLogStreamTests : TestBase
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        private static readonly IRawCounterToken _rct = LogManager.CreateRawCounterToken("raw_test");
        private static readonly INamedCounterToken _nct = LogManager.CreateNamedCounterToken("named_test");

        public AssertableLogStream CreateStandardAssertionLogger()
        {
            return new AssertableLogging.AssertableLogStream(() => _assert.Fail("unexpected or missing log message"));
        }

        public AssertableLogStream CreateInvertedAssertionLogger()
        {
            _assert = new IgnoreDeclaredFailureAssertionTool(
                new AssertionInverterTool(_assert)
            );
            return CreateStandardAssertionLogger();
        }

        [TestMethod]
        public void Test001()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test002()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test003()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test004()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test005()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test006()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Audit(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test007()
        {
            var log = CreateStandardAssertionLogger();
            log.Audit(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }


        [TestMethod]
        public void Test011()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test012()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test013()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.INFO, l));
        }
        [TestMethod]
        public void Test014()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test015()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test016()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Info(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test017()
        {
            var log = CreateStandardAssertionLogger();
            log.Info(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }

        [TestMethod]
        public void Test021()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test022()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test023()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.WARNING, l));
        }
        [TestMethod]
        public void Test024()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test025()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test026()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Warning(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test027()
        {
            var log = CreateStandardAssertionLogger();
            log.Warning(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }

        [TestMethod]
        public void Test031()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test032()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test033()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.ERROR, l));
        }
        [TestMethod]
        public void Test034()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test035()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test036()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Error(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test037()
        {
            var log = CreateStandardAssertionLogger();
            log.Error(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }


        [TestMethod]
        public void Test041()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test042()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test043()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.ALERT, l));
        }
        [TestMethod]
        public void Test044()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test045()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test046()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Alert(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test047()
        {
            var log = CreateStandardAssertionLogger();
            log.Alert(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }


        [TestMethod]
        public void Test051()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test052()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test053()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m("Something"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.DEBUG, l));
        }
        [TestMethod]
        public void Test054()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test055()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test056()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Debug(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test057()
        {
            var log = CreateStandardAssertionLogger();
            log.Debug(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }

        [TestMethod]
        public void Test060()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_rct, 10);

            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test061()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_rct, 10);

            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test062()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounterValue(_rct, 10);

            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test063()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounterValue(_rct, 10);

            log.AssertSetRawCounter((c, v) => _assert.AreEqual(10, v));
        }
        [TestMethod]
        public void Test070()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);

            log.AssertIncrementNamedCounter((t, c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test071()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);

            log.AssertIncrementNamedCounter((t, c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test072()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);

            log.AssertIncrementNamedCounter((t, c, v) => _assert.AreEqual(_lt, t));
        }

        [TestMethod]
        public void Test073()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);

            log.AssertSetNamedCounter((t, c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test074()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);

            log.AssertSetNamedCounter((t, c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test075()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);

            log.AssertSetNamedCounter((t, c, v) => _assert.AreEqual(_lt, t));
        }

        [TestMethod]
        public void Test080()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test081()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call {0} {1}", 1024, "abcd"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test082()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call {0} {1}", 1024, "abcd"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test083()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call {0} {1}", 1024, "abcd"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual("Begin scope call {0} {1}", m));
        }
        [TestMethod]
        public void Test084()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("a", "b")
            });

            log.BeginScope(ctx, _lt, m => m("Begin scope call {0} {1}", 1024, "abcd"));
            // TODO: shouldn't these be equal, or should we be asserting something different like the keys inside of the context are correct maybe?
            log.AssertBeginScope((c, t, m, f) => _assert.AreNotEqual(ctx, c));
        }
        
        [TestMethod]
        public void Test090()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test091()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call {0} {1}", 1024, "abcd"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test092()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call {0} {1}", 1024, "abcd"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test093()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call {0} {1}", 1024, "abcd"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual("End scope call {0} {1}", m));
        }
        [TestMethod]
        public void Test094()
        {
            var log = CreateStandardAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call {0} {1}", 1024, "abcd"));
            // TODO: shouldn't these be equal, or should we be asserting something different like the keys inside of the context are correct maybe?
            log.AssertEndScope((c, t, m, f) => _assert.AreNotEqual(ctx, c));
        }


        [TestMethod]
        public void Test101()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // if no logging message invoked we should fail 
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test102()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test103()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test104()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test105()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test106()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test107()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test108()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test109()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_rct, 10);
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test110()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test111()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }
        [TestMethod]
        public void Test112()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertMessage((l, t, e, u, m, f) => _assert.AreEqual(LoggingLevel.AUDIT, l));
        }        

        [TestMethod]
        public void Test120()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test121()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test122()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test123()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test124()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test125()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test126()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test127()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test128()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test129()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test130()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test131()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_rct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test140()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test141()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test142()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test143()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test144()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test145()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test146()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test147()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test148()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test149()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test150()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test151()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }


        [TestMethod]
        public void Test160()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test161()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test162()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test163()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test164()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test165()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test166()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test167()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test168()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_rct, 10);
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test169()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test170()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertIncrementNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test171()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test172()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test173()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test174()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test175()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test176()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test177()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test178()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test179()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_rct, 10);
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test180()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test181()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("Begin scope call"));
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test182()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test183()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test184()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test185()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test186()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test187()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test188()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test189()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test190()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test191()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test192()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.EndScope(ctx, _lt, m => m("End scope call"));
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test193()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.SetCounterValue(_rct, 10);
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test194()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertBeginScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }

        [TestMethod]
        public void Test203()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test204()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Error(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test205()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Warning(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test206()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Debug(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test207()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Info(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test208()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Audit(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test209()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Alert(_lt, m => m("Something"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test210()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_lt, _nct, 10);
            log.AssertSetNamedCounter((l, c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test211()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounterValue(_lt, _nct, 10);
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test212()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.BeginScope(ctx, _lt, m => m("End scope call"));
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test213()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            var ctx = LoggingCallContextStore.Push(new KeyValuePair<string, object>[] { });

            log.SetCounterValue(_rct, 10);
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test214()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertEndScope((c, t, m, f) => _assert.AreEqual(_lt, t));
        }

    }
}
