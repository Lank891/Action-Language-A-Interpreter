using Interpreter.Model.Domain.Statement;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Model;
using Action = Interpreter.Model.Domain.Action;

namespace InterpreterTests.Model
{
    public class LanguageDomainTests
    {
        //After statements, Causes statements, expected string representation, expected number of fluents
        private static List<(AfterStatement[] afterStatements, CausesStatement[] causesStatements, string stringRepresentation, int numberOfFluents)> Domains =>
        new ()
        {
            {(
                new AfterStatement[]
                {
                    new AfterStatement(
                        new Fluent("fluent", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fluent2", false),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fluent3", true),
                        Array.Empty<Action>()
                    )
                },
                Array.Empty<CausesStatement>(),
                "initially fluent\ninitially fluent3\ninitially ~fluent2",
                3
            )},
            {(
                new AfterStatement[]
                {
                    new AfterStatement(
                        new Fluent("fluent", true),
                        new Action[]
                        {
                            new Action("Action"),
                        }
                    ),
                    new AfterStatement(
                        new Fluent("fluent", false),
                        new Action[]
                        {
                            new Action("Action2")
                        }
                    )
                },
                new CausesStatement[]
                {
                    new CausesStatement(
                        new Fluent("fluent", true),
                        new Action("Action")
                    ),
                    new CausesStatement(
                        new Fluent("fluent", false),
                        new Action("Action2"),
                        new Fluent[]
                        {
                            new Fluent("fluent", true)
                        }
                    )
                },
                "fluent after Action\n~fluent after Action2\nAction causes fluent\nAction2 causes ~fluent if fluent",
                1
            )},
            {(
                new AfterStatement[]
                {
                    new AfterStatement(
                        new Fluent("fluent", true),
                        Array.Empty<Action>()
                    ),
                },
                new CausesStatement[]
                {
                    new CausesStatement(
                        new Fluent("fluent2", false),
                        new Action("Action")
                    ),
                },
                "initially fluent\nAction causes ~fluent2",
                2
            )},
            {(
                new AfterStatement[]
                {
                    new AfterStatement(
                        new Fluent("f", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("ffffffffffffffffffff", true),
                        Array.Empty<Action>()
                    ),
                    new AfterStatement(
                        new Fluent("fffffffffffffffffffff", true),
                        Array.Empty<Action>()
                    )
                },
                Array.Empty<CausesStatement>(),
                "initially f\ninitially ff\ninitially fff\ninitially ffff\ninitially fffff\ninitially ffffff\ninitially fffffff\ninitially ffffffff\ninitially fffffffff\ninitially ffffffffff\ninitially fffffffffff\ninitially ffffffffffff\ninitially fffffffffffff\ninitially ffffffffffffff\ninitially fffffffffffffff\ninitially ffffffffffffffff\ninitially fffffffffffffffff\ninitially ffffffffffffffffff\ninitially fffffffffffffffffff\ninitially ffffffffffffffffffff\ninitially fffffffffffffffffffff",
                21
            )},
        };
        
        public static IEnumerable<object?[]> ToStringData =>
            Domains.Select(domainDescription =>
            {
                LanguageDomain languageDomain = new();
                foreach (var statement in domainDescription.afterStatements)
                    languageDomain.AddAfterStatement(statement);
                foreach (var statement in domainDescription.causesStatements)
                    languageDomain.AddCausesStatement(statement);

                return new object?[] { languageDomain, domainDescription.stringRepresentation };
            });
        
        public static IEnumerable<object?[]> GenerateInitialStatesData =>
            Domains.Where(domainDescription => domainDescription.numberOfFluents <= LanguageDomain.maximalNumberOfFluentsInDomain)
            .Select(domainDescription =>
            {
                LanguageDomain languageDomain = new();
                foreach (var statement in domainDescription.afterStatements)
                    languageDomain.AddAfterStatement(statement);
                foreach (var statement in domainDescription.causesStatements)
                    languageDomain.AddCausesStatement(statement);

                return new object?[] { languageDomain, 1 << domainDescription.numberOfFluents };
            });

        public static IEnumerable<object?[]> GenerateInitialStatesWithToManyFluentsData =>
            Domains.Where(domainDescription => domainDescription.numberOfFluents > LanguageDomain.maximalNumberOfFluentsInDomain)
            .Select(domainDescription =>
            {
                LanguageDomain languageDomain = new();
                foreach (var statement in domainDescription.afterStatements)
                    languageDomain.AddAfterStatement(statement);
                foreach (var statement in domainDescription.causesStatements)
                    languageDomain.AddCausesStatement(statement);

                return new object?[] { languageDomain };
            });

        [Theory]
        [MemberData(nameof(ToStringData))]
        public void ToString_Returns_FormattedDomain(LanguageDomain domain, string expectedString)
        {
            Assert.Equal(expectedString, domain.ToString());
        }

        [Theory]
        [MemberData(nameof(GenerateInitialStatesData))]
        public void GenerateAllInitialStates_Returns_ProperAmountOfStates_When_ThereIsNotTooManyFluents(LanguageDomain domain, int expectedNumberOfInitialStates)
        {
            var initialStates = domain.GenerateAllInitialStates();
            Assert.Equal(expectedNumberOfInitialStates, initialStates.Count);
        }

        [Theory]
        [MemberData(nameof(GenerateInitialStatesWithToManyFluentsData))]
        public void GenerateAllInitialStates_Throws_When_ThereIsTooManyFluents(LanguageDomain domain)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var initialStates = domain.GenerateAllInitialStates();
            });
        }        
    }
}
