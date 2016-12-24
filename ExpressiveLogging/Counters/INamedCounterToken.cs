using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.Counters
{
    public interface INamedCounterToken
    {
        string Name { get; }
    }
}
