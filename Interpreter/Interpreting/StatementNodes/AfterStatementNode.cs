using Interpreter.Interpreting.UtilityNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.StatementNodes
{
    public class AfterStatementNode : StatementNode
    {
        public FluentNode Fluent { get; }
        public ActionListNode Actions { get; }

        public AfterStatementNode(FluentNode fluent)
        {
            Fluent = fluent;
            Actions = new();
        }

        public AfterStatementNode(FluentNode fluent, ActionListNode actions)
        {
            Fluent = fluent;
            Actions = actions;
        }
    }
}
