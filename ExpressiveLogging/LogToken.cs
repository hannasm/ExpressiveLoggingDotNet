using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveLogging
{
    class LogToken : ILogToken
    {
        internal LogToken(Type t)
        {
            this.Name = t.FullName;
        }
        internal LogToken(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
        private string _symbol;
        public string Symbol
        {
            get
            {
                if (_symbol == null) { _symbol = this.Name.Replace(".", "_"); }
                return _symbol;
            }
        }
        public bool Equals(ILogToken other)
        {
            if (other == null) { return false; }
            return this.Name == other.Name;
        }
        public override bool Equals(object obj)
        {
            var other = obj as LogToken;
            if (other == null) { return false; }
            return other.Name == this.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
