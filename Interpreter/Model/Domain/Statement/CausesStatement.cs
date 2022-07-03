using Interpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Domain.Statement
{
    public class CausesStatement : IEquatable<CausesStatement?>, ICopyable<CausesStatement>
    {
        public Fluent Fluent { get; }
        public Action Action { get; }
        public ImmutableHashSet<Fluent> Condition { get; }

        public CausesStatement(Fluent fluent, Action action, IEnumerable<Fluent> condition)
        {
            Fluent = fluent;
            Action = action;
            HashSet<Fluent> conditionSet = new();
            foreach (var c in condition)
                conditionSet.Add(c.Copy());
            Condition = conditionSet.ToImmutableHashSet();
        }

        public CausesStatement(Fluent fluent, Action action, params Fluent[] condition)
        {
            Fluent = fluent;
            Action = action;
            HashSet<Fluent> conditionSet = new();
            foreach (var c in condition)
                conditionSet.Add(c.Copy());
            Condition = conditionSet.ToImmutableHashSet();
        }

        public CausesStatement Copy()
        {
            List<Fluent> condition = new();
            foreach (var fluent in Condition)
                condition.Add(fluent.Copy());
            return new CausesStatement(Fluent.Copy(), Action.Copy(), condition);
        }

        public override string ToString()
        {
            if(Condition.Any())
            {
                return $"{Action} causes {Fluent} if {String.Join(", ", Condition)}";
            }
            else
            {
                return $"{Action} causes {Fluent}";
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CausesStatement);
        }

        public bool Equals(CausesStatement? other)
        {
            return other is not null &&
                   EqualityComparer<Fluent>.Default.Equals(Fluent, other.Fluent) &&
                   EqualityComparer<Action>.Default.Equals(Action, other.Action) &&
                   Condition.SetEquals(other.Condition);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fluent, Action, Condition);
        }

        public static bool operator ==(CausesStatement? left, CausesStatement? right)
        {
            return EqualityComparer<CausesStatement>.Default.Equals(left, right);
        }

        public static bool operator !=(CausesStatement? left, CausesStatement? right)
        {
            return !(left == right);
        }
    }
}
