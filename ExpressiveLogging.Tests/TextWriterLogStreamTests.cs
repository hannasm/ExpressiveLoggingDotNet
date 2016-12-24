using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests
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
            _log = new TextWriterLogging.TextWriterLogStream(_writer);
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
        public void AlertTest001()
        {
            _log.Alert(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest002()
        {
            _log.Alert(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest003()
        {
            _log.Alert(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest004()
        {
            _assert.Throws<FormatException>(() => _log.Alert(_lt, m => m("Test {0}")), 
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void AlertTest005()
        {
            var exc = getException();

            _log.Alert(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void AlertTest006()
        {
            var exc = getException();
            _log.Alert(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest007()
        {
            var exc = getException();
            _log.Alert(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Alert(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void AlertTest011()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest012()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest013()
        {
            int unq = -1;
            _log.Alert(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Alert(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void AlertTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Alert(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void AlertTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Alert(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Alert(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AlertTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Alert(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void AuditTest001()
        {
            _log.Audit(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest002()
        {
            _log.Audit(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest003()
        {
            _log.Audit(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest004()
        {
            _assert.Throws<FormatException>(() => _log.Audit(_lt, m => m("Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void AuditTest005()
        {
            var exc = getException();

            _log.Audit(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void AuditTest006()
        {
            var exc = getException();
            _log.Audit(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest007()
        {
            var exc = getException();
            _log.Audit(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Audit(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void AuditTest011()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest012()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest013()
        {
            int unq = -1;
            _log.Audit(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Audit(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void AuditTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Audit(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void AuditTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Audit(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Audit(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void AuditTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Audit(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void DebugTest001()
        {
            _log.Debug(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest002()
        {
            _log.Debug(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest003()
        {
            _log.Debug(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest004()
        {
            _assert.Throws<FormatException>(() => _log.Debug(_lt, m => m("Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void DebugTest005()
        {
            var exc = getException();

            _log.Debug(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void DebugTest006()
        {
            var exc = getException();
            _log.Debug(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest007()
        {
            var exc = getException();
            _log.Debug(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Debug(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void DebugTest011()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest012()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest013()
        {
            int unq = -1;
            _log.Debug(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Debug(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void DebugTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Debug(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void DebugTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Debug(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Debug(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void DebugTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Debug(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void ErrorTest001()
        {
            _log.Error(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest002()
        {
            _log.Error(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest003()
        {
            _log.Error(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest004()
        {
            _assert.Throws<FormatException>(() => _log.Error(_lt, m => m("Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void ErrorTest005()
        {
            var exc = getException();

            _log.Error(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void ErrorTest006()
        {
            var exc = getException();
            _log.Error(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest007()
        {
            var exc = getException();
            _log.Error(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Error(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void ErrorTest011()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest012()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest013()
        {
            int unq = -1;
            _log.Error(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Error(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void ErrorTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Error(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void ErrorTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Error(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Error(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void ErrorTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Error(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void InfoTest001()
        {
            _log.Info(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest002()
        {
            _log.Info(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest003()
        {
            _log.Info(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest004()
        {
            _assert.Throws<FormatException>(() => _log.Info(_lt, m => m("Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void InfoTest005()
        {
            var exc = getException();

            _log.Info(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void InfoTest006()
        {
            var exc = getException();
            _log.Info(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest007()
        {
            var exc = getException();
            _log.Info(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Info(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void InfoTest011()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest012()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest013()
        {
            int unq = -1;
            _log.Info(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Info(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void InfoTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Info(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void InfoTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Info(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Info(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void InfoTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Info(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void WarningTest001()
        {
            _log.Warning(_lt, m => m("Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest002()
        {
            _log.Warning(_lt, m => m("Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest003()
        {
            _log.Warning(_lt, m => m("Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest004()
        {
            _assert.Throws<FormatException>(() => _log.Warning(_lt, m => m("Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void WarningTest005()
        {
            var exc = getException();

            _log.Warning(_lt, m => m(exc, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void WarningTest006()
        {
            var exc = getException();
            _log.Warning(_lt, m => m(exc, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest007()
        {
            var exc = getException();
            _log.Warning(_lt, m => m(exc, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest008()
        {
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Warning(_lt, m => m(exc, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }


        [TestMethod]
        public void WarningTest011()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest012()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest013()
        {
            int unq = -1;
            _log.Warning(_lt, m => m(unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest014()
        {
            int unq = -1;
            _assert.Throws<FormatException>(() => _log.Warning(_lt, m => m(unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this common error");
        }

        [TestMethod]
        public void WarningTest015()
        {
            int unq = -1;
            var exc = getException();

            _log.Warning(_lt, m => m(exc, unq, "Test"));

            _writer.Flush();

            _assert.AreEqual("Test" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }
        [TestMethod]
        public void WarningTest016()
        {
            int unq = -1;
            var exc = getException();
            _log.Warning(_lt, m => m(exc, unq, "Test-{0}", 100));

            _writer.Flush();

            _assert.AreEqual("Test-100" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest017()
        {
            int unq = -1;
            var exc = getException();
            _log.Warning(_lt, m => m(exc, unq, "Test-{0}-{1}", 100, "ABC"));

            _writer.Flush();

            _assert.AreEqual("Test-100-ABC" + Environment.NewLine + exc.ToString() + Environment.NewLine, _string.ToString());
        }

        [TestMethod]
        public void WarningTest018()
        {
            int unq = -1;
            var exc = getException();
            _assert.Throws<FormatException>(() => _log.Warning(_lt, m => m(exc, unq, "Test {0}")),
                "Expected a format string error here because this is just a raw TextWriterLogStream. Use EmptyFormatMessageFixer to fix this issue");
        }

        [TestMethod]
        public void DisposeTest001()
        {
            // no exceptions calling dispose multiple times
            _log.Dispose();
            _log.Dispose();
        }

        [TestMethod]
        public void LogScopeTest001()
        {
            using (LogManager.NewScope(_log, _lt))
            {

            }

            _writer.Flush();
            _assert.AreEqual(string.Empty, _string.ToString());
        }
        [TestMethod]
        public void LogScopeTest002()
        {
            using (LogManager.NewScope(_log, _lt, m=>m("Custom begin message"), m=>m("Custom end message")))
            {

            }

            _writer.Flush();
            _assert.AreEqual(string.Empty, _string.ToString());
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
        public void LogScopeTest004()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).NewScope(_log))
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
        public void LogScopeTest006()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).
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
        [TestMethod]
        public void LogScopeTest008()
        {
            using (LogManager.BuildScope(_lt, m => m("Custom begin message"), m => m("Custom end message")).
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
