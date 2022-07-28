using Interpreter.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Query
{
    public abstract class Query
    {
        public abstract bool Execute(QueryExecutor queryExecutor);
    }
}
