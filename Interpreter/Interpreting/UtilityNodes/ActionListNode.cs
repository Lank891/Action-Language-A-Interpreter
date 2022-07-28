using Interpreter.Interpreting.StatementNodes;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;

namespace Interpreter.Interpreting.UtilityNodes
{
    public class ActionListNode
    {
        public List<ActionNode> Actions { get; }

        public ActionListNode()
        {
            Actions = new();
        }

        public ActionListNode(ActionNode action)
        {
            Actions = new List<ActionNode> { action };
        }

        public ActionListNode Concat(ActionListNode actionList)
        {
            Actions.AddRange(actionList.Actions);
            return this;
        }

        public IEnumerable<Action> BuildActionList()
        {
            return Actions.Select(action => action.BuildAction());
        }
    }
}
