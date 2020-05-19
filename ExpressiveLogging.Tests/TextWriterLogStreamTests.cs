using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Tests
{
    [TestClass]
    public class TextWriterLogStreamTests : TestBase
    {
        StringBuilder _string;
        StringWriter _writer;
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

        [TestInitialize]
        public override void OnTestStart()
        {
            base.OnTestStart();

            _string = new StringBuilder();
            _writer = new StringWriter(_string);
            _log = new TextWriterLogStream(_writer);
        }

        [TestCleanup]
        public override void OnTestEnd()
        {
            _log.Dispose();
            _writer.Dispose();

            base.OnTestEnd();
        }

        private static readonly ILogToken _lt = LogManager.GetToken();

        [TestMethod]
        public void WriteTest001()
        {
            _log.Write(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest002()
        {
            _log.Write(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest003()
        {
            _log.Write(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest004()
        {
            _assert.Throws<FormatException>(() => _log.Write(_lt, m => m("Test {0}")), 
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void WriteTest005()
        {
            var exc = getException();

            _log.Write(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void WriteTest006()
        {
            var exc = getException();
            _log.Write(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest007()
        {
            var exc = getException();
            _log.Write(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Write(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void WriteTest011()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest012()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest013()
        {
            int unq = -1;
            _log.Write(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Write(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void WriteTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Write(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void WriteTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Write(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Write(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WriteTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Write(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void DisposeTest001()
        {
            // no exceptions calling dispose multiple times
            _log.Dispose();
            _log.Dispose();
        }

        [TestMethod]
        public void LogScopeTest003()
        {
            using (LogManager.BuildScope(_lt).NewScope(_log))
            {

            }

            _writer.Flush();
            _assert.AreEqual(string.Empty, _string.ToString());
        }


        [TestMethod]
        public void LogScopeTest005()
        {
            using (LogManager.BuildScope(_lt).
                AddContext("A", "B").
                NewScope(_log))
            {

            }

            _writer.Flush();
            _assert.AreEqual(string.Empty, _string.ToString());
        }

        [TestMethod]
        public void LogScopeTest007()
        {
            using (LogManager.BuildScope(_lt).
                AddContext("A", "B").
                AddContext("C", "D").
                NewScope(_log))
            {

            }

            _writer.Flush();
            _assert.AreEqual(string.Empty, _string.ToString());
        }
    }
}
