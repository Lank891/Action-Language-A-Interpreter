using Interpreter.Parsing.StatementNodes;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using Interpreter.Parsing.UtilityNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parsing.StatementNodes
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

        public AfterStatement BuildAfterStatement()
        {
            return new AfterStatement(Fluent.BuildFluent(), Actions.BuildActionList());
        }
    }
}
