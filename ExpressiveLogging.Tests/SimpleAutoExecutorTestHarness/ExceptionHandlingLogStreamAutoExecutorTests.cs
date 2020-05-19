using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Tests.SimpleAutoExecutorTestHarness
{
    [TestClass]
    public class ExceptionHandlingLogStreamAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return new ExceptionHandlingLogStream(
                new TraceLogStream()
            );
        }
    }
}
