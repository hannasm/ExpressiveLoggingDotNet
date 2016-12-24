using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.ConsoleLogging
{
    public class StderrLogStream : TextWriterLogging.TextWriterLogStream
    {
        public StderrLogStream() : base(System.Console.Error)
        { }
    }
}
