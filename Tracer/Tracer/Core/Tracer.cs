using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Collections.Concurrent;

using Tracer.Serialization.Abstractions;

namespace Tracer.Core
{
    public class ClassTracer : ITracer
    {
        private StackTrace? trace;
        private readonly IntermediateTraceResult res;
        public IntermediateThreadResult? currentThread;
        private int framesSkipped;
        private readonly Stack<IntermediateMethodData> methods = new();

        private ICompatibleElement? currElement;

        public ClassTracer()
        {
            res = new IntermediateTraceResult();
        }

        public void StartTrace(int framesSkipped = 0)
        {
            this.framesSkipped = framesSkipped;
            trace = new StackTrace(false);
            this.AnalyzeTrace();
        }

        private void AnalyzeTrace()
        {
            Queue<IntermediateMethodData> queue = new();

            int id = Environment.CurrentManagedThreadId;
            bool isThreadExists = res.threads.TryGetValue(id, out currentThread);
            if (!isThreadExists) currentThread = new IntermediateThreadResult(id);

            currElement = currentThread!;

            for (int i = trace!.FrameCount - 1 - framesSkipped; i >= 1; i--)
            {
                StackFrame frame = trace.GetFrame(i)!;
                StackTrace t = new StackTrace();
                MethodBase frameData = frame.GetMethod()!;
                IntermediateMethodData temp = new(frameData.Name, frameData.DeclaringType!.Name);

                bool flag = false;
                foreach (IntermediateMethodData methodData in currElement.Methods)
                {
                    if (methodData.Name == temp.Name && methodData.ClassName == temp.ClassName)
                    {
                        flag = true;
                        currElement = methodData;
                        temp = methodData;
                    }
                    if (flag) break;
                }
                if (!flag) queue.Enqueue(temp);

                methods.Push(temp);
                temp.StartTimer();
            }

            while (queue.Count > 0)
            {
                IntermediateMethodData method = queue.Dequeue();
                currElement.Methods.Add(method);
                currElement = method;
            }

        }

        public void StopTrace()
        {
            while (methods.Count > 0)
            {
                IntermediateMethodData method = methods.Pop();
                method.StopTimer();
            }

            if (!res.threads.ContainsKey(currentThread!.Id)) 
            {
                res.threads.Add(currentThread.Id, currentThread);
            }
        }

        public TraceResult GetTraceResult()
        {
            foreach (var thread in res.threads.Values)
            {
                foreach (var method in thread.Methods) thread.Time += method.Time;
            }
            Console.WriteLine("Completed");
            return new TraceResult(res);
        }
    }
}
