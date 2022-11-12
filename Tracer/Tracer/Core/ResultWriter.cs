using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracer.Serialization.Abstractions;

namespace Tracer.Core
{
    public static class ResultWriter
    {
        public static void Write(string filename, ITraceResultSerializer serializer, TraceResult traceResult)
        {
            StreamWriter file = new StreamWriter($"{filename}.{serializer.Format}", false);
            serializer.Serialize(traceResult, file);
            file.Close();
        }

    }
}
