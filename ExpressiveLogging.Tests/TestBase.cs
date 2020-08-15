using ExpressiveAssertions;
using ExpressiveAssertions.MSTest;
using ExpressiveAssertions.Tooling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressiveLogging.V3.Tests
{
    public class TestBase
    {
        public IAssertionTool _assert;

        [TestInitialize]
        public virtual void OnTestStart()
        {
            _assert = AssertionRendererTool.Create(MSTestAssertionTool.Create());
        }
        [TestCleanup]
        public virtual void OnTestEnd()
        {
        }

    }
}
