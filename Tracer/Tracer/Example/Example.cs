using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Tracer.Core;
using Tracer.Serialization.Abstractions;
using Tracer.Serialization.JSON;
using Tracer.Serialization.XML;
using Tracer.Serialization.YAML;

namespace Tracer.Example
{
    class Examples
    {
        private static readonly ClassTracer tracer = new();
        private static readonly int framesSkipped = 2;
        private static readonly int threadFramesSkipped = 2;

        public static void Run()
        {
            /*
            tracer.StartTrace(framesSkipped);
            tracer.StopTrace();
            */
            
            M3(framesSkipped);
            Thread thread = new(() => M2(threadFramesSkipped));
            thread.Start();
            thread.Join();
            M2(framesSkipped);
            

            TraceResult traceResult = tracer.GetTraceResult();
            ResultWriter.Write("result", new JSONSerializer(), traceResult);
            ResultWriter.Write("result", new XMLSerializer(), traceResult);
            ResultWriter.Write("result", new YAMLSerializer(), traceResult);
        }

        private static void M1(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            Thread.Sleep(200);
            tracer.StopTrace();
        }

        private static void M2(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            Thread.Sleep(50);
            tracer.StopTrace();
        }

        private static void M3(int framesSkipped)
        {
            tracer.StartTrace(framesSkipped);
            M1(framesSkipped);
            M2(framesSkipped);
            tracer.StopTrace();
        }
    }

    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _bar.InnerMethod();
            _bar.InnerMethod2();
            _tracer.StopTrace();
        }

    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        public void InnerMethod2()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}
