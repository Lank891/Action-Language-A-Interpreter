using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.UtilityNodes
{
    public class ActionNode
    {
        public string Name { get; set; }

        public ActionNode(string name)
        {
            Name = name;
        }
    }
}
