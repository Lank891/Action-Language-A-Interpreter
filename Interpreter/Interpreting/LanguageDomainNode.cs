using Interpreter.Interpreting.StatementNodes;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
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

        public LanguageDomain BuildLanguageDomain()
        {
            var domain = new LanguageDomain();
            
            foreach (var statementNode in Statements)
            {
                switch(statementNode)
                {
                    case AfterStatementNode node:
                        domain.AddAfterStatement(node.BuildAfterStatement());
                        break;
                    case CausesStatementNode node:
                        domain.AddCausesStatement(node.BuildCausesStatement());
                        break;
                    default:
                        throw new ArgumentException("Unknown node type during building language domain.");
                }
            }

            return domain;
        }
    }
}
