using Interpreter.Interpreting.QueryNodes;
using Interpreter.Interpreting.StatementNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting
{
    public class QueryBlockNode
    {
        public List<QueryNode> Queries { get; }

        public QueryBlockNode(QueryNode node)
        {
            Queries = new List<QueryNode> { node };
        }

        public QueryBlockNode Concat(QueryBlockNode tail)
        {
            Queries.AddRange(tail.Queries);
            return this;
        }
    }
}
