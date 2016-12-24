using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Counters
{
    class RawCounterToken : IRawCounterToken
    {
        public string Name { get; private set; }
        internal RawCounterToken(string name)
        {
            this.Name = name;
        }
    }
}
