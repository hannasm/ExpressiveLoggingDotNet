using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Counters
{
    class NamedCounterToken : INamedCounterToken
    {
        public string Name { get; private set; }
        internal NamedCounterToken(string name)
        {
            this.Name = name;
        }
    }
}
