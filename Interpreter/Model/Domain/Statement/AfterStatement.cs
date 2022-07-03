using Interpreter.Extensions;
using Interpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Domain.Statement
{
    public class AfterStatement : IEquatable<AfterStatement?>, ICopyable<AfterStatement>
    {
        public Fluent Fluent { get;}
        
        public IReadOnlyList<Action> Actions { get;}

        public AfterStatement(Fluent fluent, IEnumerable<Action> actions)
        {
            Fluent = fluent;
            Actions = actions.ToList();
        }

        public AfterStatement(Fluent fluent, params string[] actions)
        {
            Fluent = fluent;
            Actions = actions.Select(a => new Action(a)).ToList();
        }

        public AfterStatement Copy()
        {
            List<Action> actions = new();
            foreach(var action in Actions)
                actions.Add(action.Copy());
            return new AfterStatement(Fluent.Copy(), actions);
        }

        public override string ToString()
        {
            if(Actions.Any())
            {
                return $"{Fluent} after {String.Join(", ", Actions)}";
            }
            else
            {
                return $"initially {Fluent}";
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AfterStatement);
        }

        public bool Equals(AfterStatement? other)
        {
            return other is not null &&
                   EqualityComparer<Fluent>.Default.Equals(Fluent, other.Fluent) &&
                   Actions.IsEqualTo(other.Actions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fluent, Actions);
        }

        public static bool operator ==(AfterStatement? left, AfterStatement? right)
        {
            return EqualityComparer<AfterStatement>.Default.Equals(left, right);
        }

        public static bool operator !=(AfterStatement? left, AfterStatement? right)
        {
            return !(left == right);
        }
    }
}
