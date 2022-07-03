using Interpreter.Extensions;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;

namespace Interpreter.Model.Query
{
    public class AfterQuery : IEquatable<AfterQuery?>
    {
        public Fluent Fluent { get; }

        public IReadOnlyList<Action> Actions { get; }

        public AfterQuery(Fluent fluent, IEnumerable<Action> actions)
        {
            Fluent = fluent;
            Actions = actions.ToList();
        }

        public AfterQuery(Fluent fluent, params string[] actions)
        {
            Fluent = fluent;
            Actions = actions.Select(a => new Action(a)).ToList();
        }

        public override string ToString()
        {
            if (Actions.Any())
            {
                return $"{Fluent} after {String.Join(", ", Actions)}";
            }
            else
            {
                return $"{Fluent} after";
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AfterQuery);
        }

        public bool Equals(AfterQuery? other)
        {
            return other is not null &&
                   EqualityComparer<Fluent>.Default.Equals(Fluent, other.Fluent) &&
                   Actions.IsEqualTo(other.Actions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fluent, Actions);
        }

        public static bool operator ==(AfterQuery? left, AfterQuery? right)
        {
            return EqualityComparer<AfterQuery>.Default.Equals(left, right);
        }

        public static bool operator !=(AfterQuery? left, AfterQuery? right)
        {
            return !(left == right);
        }
    }
}
