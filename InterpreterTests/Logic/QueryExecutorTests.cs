using Interpreter.Model.Domain.Statement;
using Interpreter.Model.Domain;
using Interpreter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExecutionContext = Interpreter.Logic.ExecutionContext;
using Action = Interpreter.Model.Domain.Action;
using Interpreter.Model.Query;
using Interpreter.Logic;

namespace InterpreterTests.Logic
{
    public class QueryExecutorTests
    {
        public static ExecutionContext YaleShootingProblemExecutionContext { get {
                LanguageDomain domain = new();
                domain.AddAfterStatement(new AfterStatement(new Fluent("alive", true)));
                domain.AddAfterStatement(new AfterStatement(new Fluent("alive", false), "Shoot"));
                domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", true), new Action("Load")));
                domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", false), new Action("Shoot")));
                domain.AddCausesStatement(new CausesStatement(new Fluent("alive", false), new Action("Shoot"), new Fluent("loaded", true)));

                return new ExecutionContext(domain);
            } 
        }

        public static ExecutionContext YaleShootingProblemWithUnknownLoadedExecutionContext
        {
            get
            {
                LanguageDomain domain = new();
                domain.AddAfterStatement(new AfterStatement(new Fluent("alive", true)));
                domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", true), new Action("Load")));
                domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", false), new Action("Shoot")));
                domain.AddCausesStatement(new CausesStatement(new Fluent("alive", false), new Action("Shoot"), new Fluent("loaded", true)));

                return new ExecutionContext(domain);
            }
        }        

        public static IEnumerable<object?[]> AfterQueryData =>
        new List<object?[]>
        {
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("alive", true)), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("alive", true), "Shoot"), false },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("alive", false), "Load", "Shoot"), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("loaded", false)), false },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("loaded", false), "Shoot"), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("loaded", true), "Load"), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("loaded", false), "Load", "Shoot"), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("alive", false), "Shoot", "Load"), true },
            new object?[] { YaleShootingProblemExecutionContext, new AfterQuery(new Fluent("loaded", true), "Load", "Shoot", "Load", "Shoot"), false },

            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("alive", true)), true },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("alive", true), "Shoot"), false },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("alive", false), "Shoot"), false },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("alive", false), "Load", "Shoot"), true },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", false)), false },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", true)), false },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", false), "Shoot"), true },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", true), "Load"), true },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", false), "Load", "Shoot"), true },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("alive", false), "Shoot", "Load"), false },
            new object?[] { YaleShootingProblemWithUnknownLoadedExecutionContext, new AfterQuery(new Fluent("loaded", true), "Load", "Shoot", "Load", "Shoot"), false },
        };

        [Theory]
        [MemberData(nameof(AfterQueryData))]
        public void ExecuteAfterQuery_Should_ReturnCorrectResult_When_AfterQueryApplied(ExecutionContext context, AfterQuery query, bool expectedResult)
        {
            QueryExecutor queryExecutor = new(context);

            bool result = queryExecutor.ExecuteAfterQuery(query);

            Assert.Equal(expectedResult, result);
        }
    }
}
