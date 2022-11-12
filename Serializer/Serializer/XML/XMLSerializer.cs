using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.XML
{
    public class XMLSerializer : ITraceResultSerializer
    {
        public string Format { get; } = "xml";

        public void Serialize(TraceResult traceResult, StreamWriter to)
        {
            XmlSerializer serializer = new(typeof(XMLTraceResult));
            serializer.Serialize(to, Convert(traceResult));
        }

        private static XMLTraceResult Convert(TraceResult traceResult)
        {
            return new XMLTraceResult(traceResult);
        }
    }
}
