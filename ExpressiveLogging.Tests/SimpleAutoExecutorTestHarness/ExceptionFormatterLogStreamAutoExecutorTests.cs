using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Tests.SimpleAutoExecutorTestHarness
{
    [TestClass]
    public class ExceptionFormatterLogStreamAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return new ExceptionFormatterLogStream(
                new TraceLogStream()
            );
        }

				[TestMethod]
        public override void WriteTest004()
        {
            Action x = base.WriteTest004;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WriteTest008()
        {
            Action x = base.WriteTest008;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WriteTest014()
        {
            Action x = base.WriteTest014;
            _assert.Throws<FormatException>(() => x());
        }
        [TestMethod]
        public override void WriteTest018()
        {
            Action x = base.WriteTest018;
            _assert.Throws<FormatException>(() => x());
        }
    }
}
