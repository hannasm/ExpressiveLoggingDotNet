using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Tests
{
    public class TestBase
    {
        public IAssertionTool _assert;

        [TestInitialize]
        public virtual void OnTestStart()
        {
            _assert = new ExpressiveAssertions.MSTest.MSTestAssertionTool();
        }
        [TestCleanup]
        public virtual void OnTestEnd()
        {
        }

    }
}
