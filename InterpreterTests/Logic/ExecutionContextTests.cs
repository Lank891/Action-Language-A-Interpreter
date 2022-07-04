using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExecutionContext = Interpreter.Logic.ExecutionContext;
using Action = Interpreter.Model.Domain.Action;
using Interpreter.Exceptions;

namespace InterpreterTests.Logic
{
    public class ExecutionContextTests
    {
        [Fact]
        public void YaleShootingProblemExplicitInitialStateScenario()
        {
            LanguageDomain domain = new();
            domain.AddAfterStatement(new AfterStatement(new Fluent("loaded", false)));
            domain.AddAfterStatement(new AfterStatement(new Fluent("alive", true)));
            domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", true), new Action("Load")));
            domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", false), new Action("Shoot")));
            domain.AddCausesStatement(new CausesStatement(new Fluent("alive", false), new Action("Shoot"), new Fluent("loaded", true)));

            ExecutionContext executionContext = new(domain);

            var executionResult = executionContext.GetResultsOfProgramExecution(new List<Action>() { new Action("Load"), new Action("Shoot") });

            Assert.Single(executionResult);

            (_, HashSet<Fluent> final) = executionResult.First();

            Assert.Contains(new Fluent("loaded", false), final);
            Assert.Contains(new Fluent("alive", false), final);
        }

        [Fact]
        public void YaleShootingProblemInitialStateWithAfterScenario()
        {
            LanguageDomain domain = new();
            domain.AddAfterStatement(new AfterStatement(new Fluent("alive", true)));
            domain.AddAfterStatement(new AfterStatement(new Fluent("alive", false), "Shoot"));
            domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", true), new Action("Load")));
            domain.AddCausesStatement(new CausesStatement(new Fluent("loaded", false), new Action("Shoot")));
            domain.AddCausesStatement(new CausesStatement(new Fluent("alive", false), new Action("Shoot"), new Fluent("loaded", true)));

            ExecutionContext executionContext = new(domain);

            var executionResult = executionContext.GetResultsOfProgramExecution(new List<Action>() { new Action("Load"), new Action("Shoot") });

            Assert.Single(executionResult);

            (_, HashSet<Fluent> final) = executionResult.First();

            Assert.Contains(new Fluent("loaded", false), final);
            Assert.Contains(new Fluent("alive", false), final);
        }

        [Fact]
        public void InconsistentDomainImpossibleInitialStateScenario()
        {
            LanguageDomain domain = new();
            domain.AddAfterStatement(new AfterStatement(new Fluent("fluent", true)));
            domain.AddAfterStatement(new AfterStatement(new Fluent("fluent", false)));

            Assert.Throws<InconsistentDomainException>(() => new ExecutionContext(domain));
        }

        [Fact]
        public void InconsistentDomainImpossibleTransitionScenario()
        {
            LanguageDomain domain = new();
            domain.AddAfterStatement(new AfterStatement(new Fluent("fluent", true)));
            domain.AddCausesStatement(new CausesStatement(new Fluent("fluent", true), new Action("Action")));
            domain.AddCausesStatement(new CausesStatement(new Fluent("fluent", false), new Action("Action")));

            Assert.Throws<InconsistentDomainException>(() => new ExecutionContext(domain));
        }
    }
}
