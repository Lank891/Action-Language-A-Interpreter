using Interpreter.Parsing.StatementNodes;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parsing.UtilityNodes
{
    public class FluentListNode
    {
        public List<FluentNode> Fluents { get; }

        public FluentListNode()
        {
            Fluents = new();
        }

        public FluentListNode(FluentNode fluent)
        {
            Fluents = new List<FluentNode> { fluent };
        }

        public FluentListNode Concat(FluentListNode fluentList)
        {
            Fluents.AddRange(fluentList.Fluents);
            return this;
        }

        public IEnumerable<Fluent> BuildFluentList()
        {
            return Fluents.Select(fluent => fluent.BuildFluent());
        }
    }
}
