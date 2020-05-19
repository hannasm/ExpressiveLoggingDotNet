using ExpressiveAssertions;
using ExpressiveAssertions.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Tests
{
    public class TestBase
    {
        public IAssertionTool _assert;

        [TestInitialize]
        public virtual void OnTestStart()
        {
            _assert = ExpressiveAssertions.Tooling.ShortAssertionRendererTool.Create(MSTestAssertionTool.Create());
        }
        [TestCleanup]
        public virtual void OnTestEnd()
        {
        }

    }
}
