using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.UtilityNodes
{
    public class FluentNode
    {
        public string Name { get; }
        public bool State { get; }

        public FluentNode(string name, bool negated)
        {
            Name = name;
            State = !negated;
        }
    }
}
