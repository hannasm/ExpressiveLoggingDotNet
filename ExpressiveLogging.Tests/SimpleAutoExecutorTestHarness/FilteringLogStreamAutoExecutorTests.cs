using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests.SimpleAutoExecutorTestHarness
{
    [TestClass]
    public class FilteringLogStreamAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return Filtering.FilterManager.CreateStream(
                new SystemDiagnosticsLogging.TraceLogStream(),
                Filtering.FilterManager.CreateFilter(
                    lt => true,
                    ct => true,
                    (ct, lt) => true)
            );
        }

        [TestMethod]
        public override void AlertTest004()
        {
            Action x = base.AlertTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AlertTest008()
        {
            Action x = base.AlertTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AlertTest014()
        {
            Action x = base.AlertTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AlertTest018()
        {
            Action x = base.AlertTest004;
            _assert.Throws<FormatException>(() => x());
        }

        [TestMethod]
        public override void AuditTest004()
        {
            Action x = base.AuditTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AuditTest008()
        {
            Action x = base.AuditTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AuditTest014()
        {
            Action x = base.AuditTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void AuditTest018()
        {
            Action x = base.AuditTest004;
            _assert.Throws<FormatException>(() => x());
        }

        [TestMethod]
        public override void DebugTest004()
        {
            Action x = base.DebugTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void DebugTest008()
        {
            Action x = base.DebugTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void DebugTest014()
        {
            Action x = base.DebugTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void DebugTest018()
        {
            Action x = base.DebugTest004;
            _assert.Throws<FormatException>(() => x());
        }

        [TestMethod]
        public override void ErrorTest004()
        {
            Action x = base.ErrorTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void ErrorTest008()
        {
            Action x = base.ErrorTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void ErrorTest014()
        {
            Action x = base.ErrorTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void ErrorTest018()
        {
            Action x = base.ErrorTest004;
            _assert.Throws<FormatException>(() => x());
        }

        [TestMethod]
        public override void InfoTest004()
        {
            Action x = base.InfoTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void InfoTest008()
        {
            Action x = base.InfoTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void InfoTest014()
        {
            Action x = base.InfoTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void InfoTest018()
        {
            Action x = base.InfoTest004;
            _assert.Throws<FormatException>(() => x());
        }

        [TestMethod]
        public override void WarningTest004()
        {
            Action x = base.WarningTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WarningTest008()
        {
            Action x = base.WarningTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WarningTest014()
        {
            Action x = base.WarningTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WarningTest018()
        {
            Action x = base.WarningTest004;
            _assert.Throws<FormatException>(() => x());
        }
    }
}
