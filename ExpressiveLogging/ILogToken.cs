using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging.V3
{
    public interface ILogToken : IEquatable<ILogToken>
    {
        /// <summary>
        /// Human Readable name for the token
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Token name converted to a string compatible with a C# variable declaration
        /// </summary>
        string Symbol { get; }
    }
    public interface ILogStreamToken : ILogToken
    {
    }
}
