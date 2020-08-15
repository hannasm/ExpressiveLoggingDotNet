using System;

namespace ExpressiveLogging.V3
{
    class CounterToken : ICounterToken, IEquatable<ICounterToken>, IEquatable<CounterToken>

    {
        public string Counter { get; private set; }
        internal CounterToken(string counter)
        {
            if (counter == null) { throw new ArgumentNullException(nameof(counter)); }
            this.Counter = counter;
        }

        public bool Equals(ICounterToken other)
        {
          return this.Equals(other as CounterToken);
        }

        public bool Equals(CounterToken other)
        {
           if (other == null) return false;
           if (ReferenceEquals(this, other)) return true;
           if (other.GetType() != GetType()) return false;
           return string.Equals(other.Counter, Counter);
        }
        public override bool Equals(object obj) {
          return this.Equals(obj as CounterToken);
        }

        public override int GetHashCode() {
          return this.Counter.GetHashCode();
        }
    }
}
