using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Domain
{
    public class Fluent : IEquatable<Fluent?>
    {
        public string Name { get; set; }
        public bool State { get; set; }

        public Fluent(string name, bool state)
        {
            Name = name;
            State = state;
        }

        public override string ToString()
        {
            return $"{(State ? "" : "~")}{Name}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Fluent);
        }

        public bool Equals(Fluent? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, State);
        }

        public static bool operator ==(Fluent? left, Fluent? right)
        {
            return EqualityComparer<Fluent>.Default.Equals(left, right);
        }

        public static bool operator !=(Fluent? left, Fluent? right)
        {
            return !(left == right);
        }
    }
}
