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
    public class EmptyFormatMessageFixerAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return new EmptyFormatMessageFixer(
                new TraceLogStream() // trace stream is only chosen here because it will actually render the text stream data, several other options would be equally viable for this purpose
            );
        }
    }
}
