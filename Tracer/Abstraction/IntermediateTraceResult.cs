using System.Diagnostics;
using System.Linq;

namespace Tracer.Serialization.Abstractions
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public interface ITraceResultSerializer
    {
        string Format { get; }
        void Serialize(TraceResult traceResult, StreamWriter to);
    }

    public interface ICompatibleElement
    {
        public long Time { get; set; }
        public List<IntermediateMethodData> Methods { get; set; }
    }

    public class IntermediateTraceResult
    {
        public Dictionary<int, IntermediateThreadResult> threads { get; set; }

        public IntermediateTraceResult()
        {
            this.threads = new Dictionary<int, IntermediateThreadResult>();
        }
    }

    public class IntermediateThreadResult : ICompatibleElement
    {
        public int Id { get; set; }
        public long Time { get; set; }

        public List<IntermediateMethodData> Methods { get; set; }

        public IntermediateThreadResult()
        {
            this.Methods = new List<IntermediateMethodData>();
        }

        public IntermediateThreadResult(int id)
        {
            this.Id = id;
            this.Methods = new List<IntermediateMethodData>();
        }
    }

    public class IntermediateMethodData : ICompatibleElement
    {
        private Stopwatch stopWatch;

        public string Name { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }
        public List<IntermediateMethodData> Methods { get; set; }

        public IntermediateMethodData(string name, string className)
        {
            this.Name = name;
            this.ClassName = className;
            this.Time = 0;
            this.Methods = new List<IntermediateMethodData>();
            this.stopWatch = new Stopwatch();
        }

        public void StartTimer()
        {
            this.stopWatch.Start();
        }

        public void StopTimer()
        {
            this.stopWatch.Stop();
            this.Time = stopWatch.ElapsedMilliseconds;
        }
    }
}