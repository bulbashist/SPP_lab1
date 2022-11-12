using System.Diagnostics;
using System.Threading;

using Tracer.Core;
using Tracer.Serialization.Abstractions;

namespace Tracer.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ClassTracer tracer = new();
        private static readonly int framesSkipped = 36;
        private static readonly int threadFramesSkipped = 2;

        [TestMethod]
        public void TestMethod1()
        {
            Thread thread1 = new(() => M1(threadFramesSkipped));
            thread1.Start();
            thread1.Join();
            Thread thread2 = new(() => M2(threadFramesSkipped));
            thread2.Start();
            thread2.Join();
            TraceResult res = tracer.GetTraceResult();
          //  Debug.WriteLine(res.ToString());
            Assert.AreEqual(2, res.Threads.Count);
            Assert.AreEqual("M2", res.Threads.Last().Methods[0].Name);

        }

        private void M1(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        private void M2(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            Thread.Sleep(200);
            tracer.StopTrace();
        }

        private void M3(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            M1(framesSkipped);
            Thread.Sleep(100);
            M2(framesSkipped);
            tracer.StopTrace();
        }

        [TestMethod]
        public void TestMethod2()
        {
            //M3();
            //Thread thread = new Thread(M2);
            //thread.Start();
            //thread.Join();
            tracer.StartTrace(framesSkipped);
            tracer.StopTrace();
            TraceResult res = tracer.GetTraceResult();

            Assert.AreEqual(0, res.Threads.Count);
        }
    }
}