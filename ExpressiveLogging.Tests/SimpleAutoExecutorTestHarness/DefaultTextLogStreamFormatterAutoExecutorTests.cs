using ExpressiveLogging.StreamFormatters;
using ExpressiveLogging.SystemDiagnosticsLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests.SimpleAutoExecutorTestHarness
{
    [TestClass]
    public class DefaultTextLogStreamFormatterAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return new DefaultTextLogStreamFormatter(
                new TraceLogStream() // trace stream is only chosen here because it will actually render the text stream data, several other options would be equally viable for this purpose
            );
        }

        [TestMethod]
        public override void AlertTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AlertTest004();
        }
        [TestMethod]
        public override void AlertTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AlertTest008();
        }
        [TestMethod]
        public override void AlertTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AlertTest014();
        }
        [TestMethod]
        public override void AlertTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AlertTest018();
        }

        [TestMethod]
        public override void AuditTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AuditTest004();
        }
        [TestMethod]
        public override void AuditTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AuditTest008();
        }
        [TestMethod]
        public override void AuditTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AuditTest014();
        }
        [TestMethod]
        public override void AuditTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.AuditTest018();
        }

        [TestMethod]
        public override void DebugTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.DebugTest004();
        }
        [TestMethod]
        public override void DebugTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.DebugTest008();
        }
        [TestMethod]
        public override void DebugTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.DebugTest014();
        }
        [TestMethod]
        public override void DebugTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.DebugTest018();
        }

        [TestMethod]
        public override void ErrorTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.ErrorTest004();
        }
        [TestMethod]
        public override void ErrorTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.ErrorTest008();
        }
        [TestMethod]
        public override void ErrorTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.ErrorTest014();
        }
        [TestMethod]
        public override void ErrorTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.ErrorTest018();
        }

        [TestMethod]
        public override void InfoTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.InfoTest004();
        }
        [TestMethod]
        public override void InfoTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.InfoTest008();
        }
        [TestMethod]
        public override void InfoTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.InfoTest014();
        }
        [TestMethod]
        public override void InfoTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.InfoTest018();
        }

        [TestMethod]
        public override void WarningTest004()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.WarningTest004();
        }
        [TestMethod]
        public override void WarningTest008()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.WarningTest008();
        }
        [TestMethod]
        public override void WarningTest014()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.WarningTest014();
        }
        [TestMethod]
        public override void WarningTest018()
        {
            // this is kind of a wierd outcome but i guess we'll take it
            base.WarningTest018();
        }
    }
}
