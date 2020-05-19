using ExpressiveLogging.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.V3.Context;
using System.Diagnostics;

namespace ExpressiveLogging.V3
{
    public class StdoutLogStream : TextWriterLogStream
    {
        public StdoutLogStream() 
            : base(System.Console.Out)
        {}
    }
}
