using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.XML
{
    public class XMLTraceResult
    {
        public List<XMLThreadResult> Threads { get; set; }

        public XMLTraceResult() {}

        public XMLTraceResult(TraceResult res)
        {
            List<XMLThreadResult> t = new();

            foreach (ThreadResult thr in res.Threads)
            {
                t.Add(new XMLThreadResult(thr.Id, thr.Time, XMLThreadResult.CopyThread(thr)));
            }

            this.Threads = t;
        }
    }

    public class XMLThreadResult
    {
        public int Id { get; set; }
        public long Time { get; set; }

        public List<XMLMethodData> Methods { get; set; }

        public XMLThreadResult() {}

        public XMLThreadResult(int id, long time, List<XMLMethodData> methods)
        {
            this.Id = id;
            this.Time = time;
            this.Methods = methods;
        }

        public static List<XMLMethodData> CopyThread(ThreadResult threadResult)
        {

            List<XMLMethodData> methods = new();
            foreach (MethodData method in threadResult.Methods)
            {
                methods.Add(XMLMethodData.CopyMethod(method));
            }

            return methods;
        }
    }

    public class XMLMethodData
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }
        public List<XMLMethodData> Methods { get; set; }

        public XMLMethodData() {}

        public XMLMethodData(string name, string className, long Time, List<XMLMethodData> methods)
        {
            this.Name = name;
            this.ClassName = className;
            this.Time = Time;
            this.Methods = methods;
        }

        public static XMLMethodData CopyMethod(MethodData methodData)
        {

            List<XMLMethodData> methods = new();
            foreach (MethodData method in methodData.Methods)
            {
                methods.Add(CopyMethod(method));
            }

            XMLMethodData methodDataFinal = new(methodData.Name, methodData.ClassName, methodData.Time, methods);
            return methodDataFinal;
        }
    }
}
