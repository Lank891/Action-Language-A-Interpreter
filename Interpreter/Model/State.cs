using Interpreter.Interfaces;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model
{
    public class State : HashSet<Fluent>, ICopyable<State>
    {
        public State() : base()
        {
        }

        public State(IEnumerable<Fluent> fluents) : base(fluents)
        {
        }

        public State Copy()
        {
            State newSet = new();
            foreach (var item in this)
                newSet.Add(item.Copy());
            return newSet;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (var fluent in this)
            {
                hashCode ^= fluent.GetHashCode();
            }
            return hashCode;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not State other)
                return false;

            if (Count != other.Count)
                return false;

            return this.All(f => other.Contains(f));
        }
    }
}
