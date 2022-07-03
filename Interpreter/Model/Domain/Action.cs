using Interpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Domain
{
    public class Action : IEquatable<Action?>, ICopyable<Action>
    {
        public string Name { get; set; }

        public Action(string name)
        {
            Name = name;
        }

        public Action Copy()
        {
            return new Action(Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Action);
        }

        public bool Equals(Action? other)
        {
            return other is not null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(Action? left, Action? right)
        {
            return EqualityComparer<Action>.Default.Equals(left, right);
        }

        public static bool operator !=(Action? left, Action? right)
        {
            return !(left == right);
        }
    }
}
