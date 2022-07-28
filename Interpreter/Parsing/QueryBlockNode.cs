using Interpreter.Parsing.QueryNodes;
using Interpreter.Parsing.StatementNodes;
using Interpreter.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parsing
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
        
        public IEnumerable<Query> BuildQueryList()
        {
            return Queries.Select(x => x.BuildQuery());
        }
    }
}
