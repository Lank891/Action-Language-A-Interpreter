using Interpreter.Interpreting.StatementNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting
{
    public class LanguageDomainNode
    {
        public List<StatementNode> Statements { get; }

        public LanguageDomainNode(StatementNode node)
        {
            Statements = new List<StatementNode> { node };
        }

        public LanguageDomainNode Concat(LanguageDomainNode tail)
        {
            Statements.AddRange(tail.Statements);
            return this;
        }
    }
}
