using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;

namespace Interpreter.Exceptions
{
    public class InconsistentDomainException : Exception
    {
        public InconsistentDomainException(Action action, HashSet<Fluent> state, string inconsistentFluent)
            : base($"Action {action} requires fluent {inconsistentFluent} to be in different states. State before action execution: " + String.Join(", ", state) + ".")
        {
        }

        public InconsistentDomainException(string message) : base(message)
        {
        }
    }
}
