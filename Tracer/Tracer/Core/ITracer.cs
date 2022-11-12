﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracer.Serialization.Abstractions;

namespace Tracer.Core
{
    public interface ITracer
    {
        void StartTrace(int framesSkipped = 0);
        void StopTrace();

        TraceResult GetTraceResult();

    }
}
