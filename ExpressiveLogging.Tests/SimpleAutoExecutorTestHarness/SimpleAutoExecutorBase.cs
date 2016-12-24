using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests.SimpleAutoExecutorTestHarness
{
    public abstract class SimpleAutoExecutorBase : TestBase
    {
        ILogStream _log;

        Exception throwException()
        {
            throw new Exception("Generic Exception");
        }
        Exception getException()
        {
            try
            {
                throwException();

                throw new InvalidProgramException("This should never happen");
            }
            catch (Exception eError)
            {
                return eError;
            }
        }

        public abstract ILogStream ConstructLoggger();

        [TestInitialize]
        public override void OnTestStart()
        {
            base.OnTestStart();

            _log = ConstructLoggger();
        }

        [TestCleanup]
        public override void OnTestEnd()
        {
            _log.Dispose();

            base.OnTestEnd();
        }

        private static readonly ILogToken _lt = LogManager.GetToken();

        [TestMethod]
        public virtual void AlertTest001()
        {
            _log.Alert(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void AlertTest002()
        {
            _log.Alert(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AlertTest003()
        {
            _log.Alert(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AlertTest004()
        {
            _log.Alert(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void AlertTest005()
        {
            var exc = getException();

            _log.Alert(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void AlertTest006()
        {
            var exc = getException();
            _log.Alert(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AlertTest007()
        {
            var exc = getException();
            _log.Alert(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AlertTest008()
        {
            var exc = getException();
            _log.Alert(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void AlertTest011()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void AlertTest012()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AlertTest013()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AlertTest014()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void AlertTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Alert(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void AlertTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Alert(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AlertTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Alert(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AlertTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Alert(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void AuditTest001()
        {
            _log.Audit(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void AuditTest002()
        {
            _log.Audit(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AuditTest003()
        {
            _log.Audit(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AuditTest004()
        {
            _log.Audit(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void AuditTest005()
        {
            var exc = getException();

            _log.Audit(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void AuditTest006()
        {
            var exc = getException();
            _log.Audit(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AuditTest007()
        {
            var exc = getException();
            _log.Audit(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AuditTest008()
        {
            var exc = getException();
            _log.Audit(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void AuditTest011()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void AuditTest012()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AuditTest013()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AuditTest014()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void AuditTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Audit(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void AuditTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Audit(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void AuditTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Audit(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void AuditTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Audit(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void DebugTest001()
        {
            _log.Debug(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void DebugTest002()
        {
            _log.Debug(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void DebugTest003()
        {
            _log.Debug(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void DebugTest004()
        {
            _log.Debug(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void DebugTest005()
        {
            var exc = getException();

            _log.Debug(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void DebugTest006()
        {
            var exc = getException();
            _log.Debug(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void DebugTest007()
        {
            var exc = getException();
            _log.Debug(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void DebugTest008()
        {
            var exc = getException();
            _log.Debug(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void DebugTest011()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void DebugTest012()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void DebugTest013()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void DebugTest014()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void DebugTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Debug(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void DebugTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Debug(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void DebugTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Debug(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void DebugTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Debug(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void ErrorTest001()
        {
            _log.Error(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void ErrorTest002()
        {
            _log.Error(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void ErrorTest003()
        {
            _log.Error(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void ErrorTest004()
        {
            _log.Error(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void ErrorTest005()
        {
            var exc = getException();

            _log.Error(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void ErrorTest006()
        {
            var exc = getException();
            _log.Error(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void ErrorTest007()
        {
            var exc = getException();
            _log.Error(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void ErrorTest008()
        {
            var exc = getException();
            _log.Error(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void ErrorTest011()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void ErrorTest012()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void ErrorTest013()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void ErrorTest014()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void ErrorTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Error(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void ErrorTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Error(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void ErrorTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Error(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void ErrorTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Error(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void InfoTest001()
        {
            _log.Info(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void InfoTest002()
        {
            _log.Info(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void InfoTest003()
        {
            _log.Info(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void InfoTest004()
        {
            _log.Info(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void InfoTest005()
        {
            var exc = getException();

            _log.Info(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void InfoTest006()
        {
            var exc = getException();
            _log.Info(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void InfoTest007()
        {
            var exc = getException();
            _log.Info(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void InfoTest008()
        {
            var exc = getException();
            _log.Info(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void InfoTest011()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void InfoTest012()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void InfoTest013()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void InfoTest014()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void InfoTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Info(_lt, m => m(exc, unq, "Test"));
        }

        [TestMethod]
        public virtual void InfoTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Info(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void InfoTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Info(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void InfoTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Info(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void WarningTest001()
        {
            _log.Warning(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void WarningTest002()
        {
            _log.Warning(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WarningTest003()
        {
            _log.Warning(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WarningTest004()
        {
            _log.Warning(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void WarningTest005()
        {
            var exc = getException();

            _log.Warning(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void WarningTest006()
        {
            var exc = getException();
            _log.Warning(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WarningTest007()
        {
            var exc = getException();
            _log.Warning(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WarningTest008()
        {
            var exc = getException();
            _log.Warning(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void WarningTest011()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void WarningTest012()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WarningTest013()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WarningTest014()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void WarningTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Warning(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void WarningTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Warning(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WarningTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Warning(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WarningTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Warning(_lt, m => m(exc, unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void DisposeTest001()
        {
            // no exceptions calling dispose multiple times
            _log.Dispose();
            _log.Dispose();
        }

        [TestMethod]
        public virtual void LogScopeTest001()
        {
            using (LogManager.NewScope(_log, _lt))
            {

            }
        }
        [TestMethod]
        public virtual void LogScopeTest002()
        {
            using (LogManager.NewScope(_log, _lt, m => m("Custom begin message"), m => m("Custom end message")))
            {

            }
        }
        [TestMethod]
        public virtual void LogScopeTest003()
        {
            using (LogManager.BuildScope(_lt).NewScope(_log))
            {

            }
        }
        [TestMethod]
        public virtual void LogScopeTest004()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).NewScope(_log))
            {

            }
        }


        [TestMethod]
        public virtual void LogScopeTest005()
        {
            using (LogManager.BuildScope(_lt).
                AddContext("A", "B").
                NewScope(_log))
            {

            }            
        }
        [TestMethod]
        public virtual void LogScopeTest006()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).
                AddContext("A", "B").
                NewScope(_log))
            {

            }
        }

        [TestMethod]
        public virtual void LogScopeTest007()
        {
            using (LogManager.BuildScope(_lt).
                AddContext("A", "B").
                AddContext("C", "D").
                NewScope(_log))
            {

            }
        }
        [TestMethod]
        public virtual void LogScopeTest008()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).
                AddContext("A", "B").
                AddContext("C", "D").
                NewScope(_log))
            {

            }
        }
    }
}
