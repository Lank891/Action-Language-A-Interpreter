using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
