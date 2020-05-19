using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3.Context
{

    /// <summary>
    /// Describes a custom logging scope for 
    /// hierarchical logging data.
    /// The constructor denotes the beginning of the scope
    /// and calling the IDisposable.Dispose method
    /// denotes the end of the scope
    /// </summary>
    public interface ILogContextScope : IDisposable
    {
    }
}
