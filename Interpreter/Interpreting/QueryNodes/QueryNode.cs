using Interpreter.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreting.QueryNodes
{
    public abstract class QueryNode
    {
        public abstract Query BuildQuery();
    }
}
