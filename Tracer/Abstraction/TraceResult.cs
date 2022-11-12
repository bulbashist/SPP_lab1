using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Serialization.Abstractions
{
    public class TraceResult
    {
        public TraceResult(IntermediateTraceResult res)
        {
            List<ThreadResult> t = new();

            foreach (IntermediateThreadResult thr in res.threads.Values)
            {
                t.Add(new ThreadResult(thr.Id, thr.Time, ThreadResult.CopyThread(thr)));
            }

            this.Threads = t.AsReadOnly();
        }

        public IReadOnlyList<ThreadResult> Threads { get; }
    }

    public class ThreadResult
    {
        public int Id { get; }
        public long Time { get; }

        public IReadOnlyList<MethodData> Methods { get; }

        public ThreadResult(int id, long time, List<MethodData> methods)
        {
            this.Id = id;
            this.Time = time;
            this.Methods = methods;
        }

        public static List<MethodData> CopyThread(IntermediateThreadResult threadResult)
        {

            List<MethodData> methods = new();
            foreach (IntermediateMethodData method in threadResult.Methods)
            {
                methods.Add(MethodData.CopyMethod(method));
            }

            //ThreadResultFinal thres = new(threadResult.Id, threadResult.Time, methods);
            return methods;
        }
    }

    public class MethodData
    {
        public string Name { get; }
        public string ClassName { get; }
        public long Time { get; }
        public IReadOnlyList<MethodData> Methods { get; set; }

        public MethodData(string name, string className, long Time, List<MethodData> methods)
        {
            this.Name = name;
            this.ClassName = className;
            this.Time = Time;
            this.Methods = methods;
        }

        public static MethodData CopyMethod(IntermediateMethodData methodData)
        {

            List<MethodData> methods = new();
            foreach (IntermediateMethodData method in methodData.Methods)
            {
                methods.Add(CopyMethod(method));
            }

            MethodData methodDataFinal = new(methodData.Name, methodData.ClassName, methodData.Time, methods);
            return methodDataFinal;
        }
    }
}
