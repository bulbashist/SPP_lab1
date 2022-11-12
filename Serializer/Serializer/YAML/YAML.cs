using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.YAML
{
    public class YAMLSerializer : ITraceResultSerializer
    {
        public string Format { get; } = "yaml";

        public void Serialize(TraceResult traceResult, StreamWriter to)
        {
            Serializer yamlSerializer = new Serializer();
            yamlSerializer.Serialize(to, traceResult);
        }
    }
}

