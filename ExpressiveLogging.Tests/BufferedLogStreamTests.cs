using ExpressiveAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace ExpressiveLogging.V3.Tests
{
    [TestClass]
    public class BufferedLogStreamTests : TestBase
    {
        BufferedLogStream _buffer;
        AssertableLogStream _assertLog;


        [TestInitialize]
        public override void OnTestStart()
        {
            base.OnTestStart();

            _buffer = new BufferedLogStream();
            _assertLog = new AssertableLogStream(()=>_assert.Fail("Unexpected event encountered while asserting buffer"));
        }

        [TestCleanup]
        public override void OnTestEnd()
        {
            _buffer.Dispose();
            _assertLog.Dispose();
            base.OnTestEnd();
        }

        private static readonly ILogToken _lt1 = LogManager.GetToken();
        private static readonly ILogToken _lt2 = LogManager.GetToken(_lt1, "Two");
        private static readonly ILogToken _lt3 = LogManager.GetToken(_lt1, "Three");

        [TestMethod]
        public void BufferedLogStreamTest001() {
          _buffer.Write(_lt1, m=>m("Hello World"));
          _assert.AreEqual(()=>1, ()=>_buffer.BufferSize);

          _buffer.ExecuteBuffer(_assertLog, TimeSpan.MaxValue);
          _assert.AreEqual(()=>1, ()=>_assertLog.BufferSize);
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);

          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);
        }
        [TestMethod]
        public void BufferedLogStreamTest002() {
          _buffer.Write(_lt1, m=>m("Hello World"));
          _buffer.Write(_lt2, m=>m("Hello World 2"));
          _assert.AreEqual(()=>2, ()=>_buffer.BufferSize);

          _buffer.ExecuteBuffer(_assertLog, TimeSpan.MaxValue);
          _assert.AreEqual(()=>2, ()=>_assertLog.BufferSize);
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);

          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 2");
              _assert.AreEqual(t, _lt2);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
        }
        [TestMethod]
        public void BufferedLogStreamTest003() {
          _buffer.Write(_lt1, m=>m("Hello World"));
          _buffer.Write(_lt2, m=>m("Hello World 2"));
          _buffer.Write(_lt3, m=>m("Hello World 3"));
          _assert.AreEqual(()=>3, ()=>_buffer.BufferSize);

          _buffer.ExecuteBuffer(_assertLog, TimeSpan.MaxValue);
          _assert.AreEqual(()=>3, ()=>_assertLog.BufferSize);
          _assert.AreEqual(()=>0, ()=>_buffer.BufferSize);

          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 2");
              _assert.AreEqual(t, _lt2);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World 3");
              _assert.AreEqual(t, _lt3);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
        }
        [TestMethod]
        public void BufferedLogStreamTest004() {
          for (var i = 0; i < 10000; i++) {
            _buffer.Write(_lt1, m=>{m("Hello World"); });
            _buffer.Write(_lt2, m=>{m("Hello World 2"); });
            _buffer.Write(_lt3, m=>{m("Hello World 3"); });
          }

           var bufferSize = _buffer.BufferSize;
          _assert.AreEqual(()=>bufferSize, ()=>_buffer.BufferSize);

          _buffer.ExecuteBuffer(_assertLog, TimeSpan.FromMilliseconds(1));
          _assert.Check(()=>_assertLog.BufferSize < bufferSize);
          _assert.Check(()=>_assertLog.BufferSize > 0);
          _assert.Check(()=>_buffer.BufferSize > 0);

          _assertLog.AssertMessage((t,e,u,m,f)=>{
              _assert.AreEqual(m, "Hello World");
              _assert.AreEqual(t, _lt1);
              _assert.IsNull(()=>e);
              _assert.Count(0, ()=>f);
            });
        }
    }
}
