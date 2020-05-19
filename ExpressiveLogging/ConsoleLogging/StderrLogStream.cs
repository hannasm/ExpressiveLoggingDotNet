using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public class StderrLogStream : TextWriterLogStream
    {
        public StderrLogStream() : base(System.Console.Error)
        { }
    }
}
