using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Tests.SimpleAutoExecutorTestHarness
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
        public virtual void WriteTest001()
        {
            _log.Write(_lt, m => m("Test"));
        }

        [TestMethod]
        public virtual void WriteTest002()
        {
            _log.Write(_lt, m => m("Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WriteTest003()
        {
            _log.Write(_lt, m => m("Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WriteTest004()
        {
            _log.Write(_lt, m => m("Test {0}"));
        }

        [TestMethod]
        public virtual void WriteTest005()
        {
            var exc = getException();

            _log.Write(_lt, m => m(exc, "Test"));
        }
        [TestMethod]
        public virtual void WriteTest006()
        {
            var exc = getException();
            _log.Write(_lt, m => m(exc, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WriteTest007()
        {
            var exc = getException();
            _log.Write(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WriteTest008()
        {
            var exc = getException();
            _log.Write(_lt, m => m(exc, "Test {0}"));
        }


        [TestMethod]
        public virtual void WriteTest011()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test"));
        }

        [TestMethod]
        public virtual void WriteTest012()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WriteTest013()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WriteTest014()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test {0}"));
        }

        [TestMethod]
        public virtual void WriteTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Write(_lt, m => m(exc, unq, "Test"));
        }
        [TestMethod]
        public virtual void WriteTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Write(_lt, m => m(exc, unq, "Test-{0}", 100));
        }

        [TestMethod]
        public virtual void WriteTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Write(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));
        }

        [TestMethod]
        public virtual void WriteTest018()
        {
            int unq = -1;
            var exc = getException();
            _log.Write(_lt, m => m(exc, unq, "Test {0}"));
        }


        [TestMethod]
        public virtual void DisposeTest001()
        {
            // no exceptions calling dispose multiple times
            _log.Dispose();
            _log.Dispose();
        }

        [TestMethod]
        public virtual void LogScopeTest003()
        {
            using (LogManager.BuildScope(_lt).NewScope(_log))
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
        public virtual void LogScopeTest007()
        {
            using (LogManager.BuildScope(_lt).
                AddContext("A", "B").
                AddContext("C", "D").
                NewScope(_log))
            {

            }
        }
    }
}
