using Interpreter.Interpreting.UtilityNodes;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.StatementNodes
{
    public class CausesStatementNode : StatementNode
    {
        public FluentNode Fluent { get; }
        public ActionNode Action { get; }
        public FluentListNode Conditions { get; }

        public CausesStatementNode(FluentNode fluent, ActionNode action)
        {
            Fluent = fluent;
            Action = action;
            Conditions = new();
        }

        public CausesStatementNode(FluentNode fluent, ActionNode action, FluentListNode conditions)
        {
            Fluent = fluent;
            Action = action;
            Conditions = conditions;
        }
    }
}
