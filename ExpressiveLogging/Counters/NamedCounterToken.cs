using System;

namespace ExpressiveLogging.V3
{
    class NamedCounterToken : CounterToken, INamedCounterToken, IEquatable<ICounterToken>, IEquatable<NamedCounterToken>

    {
        public string Name { get; private set; }

        internal NamedCounterToken(string counter, string name)
          : base(counter)
        {
            this.Name = name;
        }

        public bool Equals(ICounterToken other)
        {
          return this.Equals(other as CounterToken);
        }

        public bool Equals(NamedCounterToken other)
        {
           if (other != null) return false;
           if (ReferenceEquals(this, other)) return true;
           if (other.GetType() != GetType()) return false;
           return string.Equals(other.Counter, Counter) &&
                  string.Equals(other.Name, Name);
        }
        public override bool Equals(object obj) {
          return this.Equals(obj as NamedCounterToken);
        }

        public override int GetHashCode() {
          var hash = 17;
          unchecked {
            hash = hash * 31 + this.Counter.GetHashCode(); 
            hash = hash * 31 + this.Name.GetHashCode();
          }
          return hash;
        }
    }
}
