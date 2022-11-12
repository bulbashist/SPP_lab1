using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.JSON
{
    public class JSONSerializer : ITraceResultSerializer
    {
        public string Format { get; } = "json";

        public void Serialize(TraceResult traceResult, StreamWriter to)
        {
            string jsonString = JsonSerializer.Serialize(traceResult, new JsonSerializerOptions { WriteIndented = true });
            to.Write(jsonString);
        }
    }
}