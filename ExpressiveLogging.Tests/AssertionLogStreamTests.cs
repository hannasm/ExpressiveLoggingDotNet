using ExpressiveAssertions;
using ExpressiveAssertions.Tooling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ExpressiveLogging.V3.Tests
{
    [TestClass]
    public class AssertionLogStreamTests : TestBase
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        private static readonly ICounterToken _rct = LogManager.CreateRawCounterToken("raw_test");
        private static readonly INamedCounterToken _nct = LogManager.CreateNamedCounterToken("named_test", _lt);

        public AssertableLogStream CreateStandardAssertionLogger()
        {
            return new AssertableLogStream(() => _assert.Fail("unexpected or missing log message"));
        }

        public AssertableLogStream CreateInvertedAssertionLogger()
        {
            _assert = new IgnoreDeclaredFailureAssertionTool(
                new AssertionInverterTool(_assert)
            );
            return CreateStandardAssertionLogger();
        }

        [TestMethod]
        public void Test001()
        {
            var log = CreateStandardAssertionLogger();
            log.Write(_lt, m => m("Something"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }
        [TestMethod]
        public void Test002()
        {
            var log = CreateStandardAssertionLogger();
            log.Write(_lt, m => m("Something"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual("Something", m));
        }
        [TestMethod]
        public void Test004()
        {
            var log = CreateStandardAssertionLogger();
            log.Write(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual(1024, (int)f[0]));
        }
        [TestMethod]
        public void Test005()
        {
            var log = CreateStandardAssertionLogger();
            log.Write(_lt, m => m("Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual("abcd", f[1]));
        }
        [TestMethod]
        public void Test006()
        {
            var log = CreateStandardAssertionLogger();
            var exc = new Exception("Hello World");
            log.Write(_lt, m => m(exc, "Something {0} {2}", 1024, "abcd"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual(exc, e));
        }
        [TestMethod]
        public void Test007()
        {
            var log = CreateStandardAssertionLogger();
            log.Write(_lt, m => m(1111, "Something {0} {2}"));

            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual(1111, u.Value));
        }

        [TestMethod]
        public void Test060()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_rct, 10);

            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test061()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_rct, 10);

            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test062()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounter(_rct, 10);

            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test063()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounter(_rct, 10);

            log.AssertSetRawCounter((c, v) => _assert.AreEqual(10, v));
        }
        [TestMethod]
        public void Test070()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_nct, 10);

            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(()=>_nct, ()=>c));
        }

        [TestMethod]
        public void Test071()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_nct, 10);

            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test072()
        {
            var log = CreateStandardAssertionLogger();
            log.IncrementCounterBy(_nct, 10);

            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(_lt.Name, c.Name));
        }

        [TestMethod]
        public void Test073()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounter(_nct, 10);

            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test074()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounter(_nct, 10);

            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(10, v));
        }

        [TestMethod]
        public void Test075()
        {
            var log = CreateStandardAssertionLogger();
            log.SetCounter(_nct, 10);

            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_lt.Name, c.Name));
        }

        [TestMethod]
        public void Test101()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // if no logging message invoked we should fail 
            log.AssertMessage((t, e, u, m, f) => _assert.AreEqual(_lt, t));
        }

        [TestMethod]
        public void Test120()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test125()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Write(_lt, m => m("Something"));
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test127()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_nct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test128()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounter(_nct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test131()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounter(_rct, 10);
            log.AssertIncrementRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }

        [TestMethod]
        public void Test140()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test145()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Write(_lt, m => m("Something"));
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test147()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_nct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test148()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounter(_nct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }
        [TestMethod]
        public void Test151()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertSetRawCounter((c, v) => _assert.AreEqual(_rct, c));
        }


        [TestMethod]
        public void Test160()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test165()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Write(_lt, m => m("Something"));
            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test167()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test168()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounter(_rct, 10);
            log.AssertIncrementNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test171()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            // no log message generated at all
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test176()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.Write(_lt, m => m("Something"));
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test178()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_nct, 10);
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test179()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.SetCounter(_rct, 10);
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }
        [TestMethod]
        public void Test182()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_rct, 10);
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }

        [TestMethod]
        public void Test190()
        {
            // NOTE: (INVERTED assertion)
            var log = CreateInvertedAssertionLogger();
            log.IncrementCounterBy(_nct, 10);
            log.AssertSetNamedCounter((c, v) => _assert.AreEqual(_nct, c));
        }

        static readonly ILogToken _lt1 = LogManager.GetToken();
        static readonly ILogToken _lt2 = LogManager.GetToken(_lt1, "Two");
        static readonly ILogToken _lt3 = LogManager.GetToken(_lt1, "Three");

        [TestMethod]
        public void Test200() {
          var _buffer = CreateStandardAssertionLogger();

          _buffer.Write(_lt1, m=>m("Hello World"));
          _assert.AreEqual(()=>1, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);
        }

        [TestMethod]
        public void Test201() {
          var _buffer = CreateStandardAssertionLogger();
          _buffer.Write(_lt1, m=>m("Hello World"));
          _buffer.Write(_lt2, m=>m("Hello World 2"));
          _assert.AreEqual(()=>2, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>1, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 2");
              _assert.AreEqual(t, _lt2);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);
        }

        [TestMethod]
        public void Test202() {
          var _buffer = CreateStandardAssertionLogger();

          _buffer.Write(_lt1, m=>m("Hello World"));
          _buffer.Write(_lt2, m=>m("Hello World 2"));
          _buffer.Write(_lt3, m=>m("Hello World 3"));
          _assert.AreEqual(()=>3, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>2, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 2");
              _assert.AreEqual(t, _lt2);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>1, ()=>_buffer.BufferSize);
          _buffer.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 3");
              _assert.AreEqual(t, _lt3);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);
        }
    }
}
