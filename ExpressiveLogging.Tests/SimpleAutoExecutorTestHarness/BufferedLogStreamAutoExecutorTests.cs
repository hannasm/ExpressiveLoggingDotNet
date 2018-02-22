using ExpressiveAssertions;
using ExpressiveLogging.BufferedLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests.SimpleAutoExecutorTestHarness
{
    [TestClass]
    public class BufferedLogStreamAutoExecutorTests : SimpleAutoExecutorBase
    {
        public override ILogStream ConstructLoggger()
        {
            return new BufferedLogStream();
        }
    }
}
