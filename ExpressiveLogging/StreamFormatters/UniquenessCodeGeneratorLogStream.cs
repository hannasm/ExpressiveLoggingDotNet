using ExpressiveLogging.V3.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    /// <summary>
    /// Intercepts any logging calls that are missing a uniqueness code, and generates a uniqueness code using appropriate default heuristics
    /// </summary>
    public class UniquenessCodeGeneratorLogStream : DelegatingLogStream
    {
        public UniquenessCodeGeneratorLogStream(ILogStream inner) : base(inner)
        {
        }

        public override void Write(ILogToken t, Action<CompleteLogMessage> msgBuilder)
        {
            msgBuilder((e, u, m, f) => _inner.Write(t, log => log(e, u ?? LogManager.GenerateUniquenessCode(e,m), m, f)));
        }
    }
}
