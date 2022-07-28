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
    public class ActionNode
    {
        public string Name { get; set; }

        public ActionNode(string name)
        {
            Name = name;
        }

        public Action BuildAction()
        {
            return new Action(Name);
        }
    }
}
