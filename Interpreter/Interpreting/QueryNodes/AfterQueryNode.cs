using Interpreter.Interpreting.UtilityNodes;
using Interpreter.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.QueryNodes
{
    public class AfterQueryNode : QueryNode
    {
        public FluentNode Fluent { get; }
        public ActionListNode Actions { get; }

        public AfterQueryNode(FluentNode fluent, ActionListNode actions)
        {
            Fluent = fluent;
            Actions = actions;
        }

        public override Query BuildQuery()
        {
            return new AfterQuery(Fluent.BuildFluent(), Actions.BuildActionList());
        }
    }
}
