using Interpreter.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Logic
{
    public class QueryExecutor
    {
        private ExecutionContext ExecutionContext { get; }

        public QueryExecutor(ExecutionContext executionContext)
        {
            ExecutionContext = executionContext;
        }

        public bool ExecuteQuery(AfterQuery query)
        {
            IEnumerable<(Model.State initial, Model.State final)> executionResults = ExecutionContext.GetResultsOfProgramExecution(query.Actions);

            if (executionResults.Count() == 0)
                return true;

            bool result = true;
            foreach (var (_, final) in executionResults)
            {
                result = result && final.Contains(query.Fluent);
            }

            return result;
        }
    }
}
