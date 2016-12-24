using ExpressiveLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveLogging.Context;
using ExpressiveLogging.Counters;
using System.Diagnostics;

namespace ExpressiveLogging.ConsoleLogging
{
    public class StdoutLogStream : TextWriterLogging.TextWriterLogStream
    {
        public StdoutLogStream() 
            : base(System.Console.Out)
        {}
    }
}
