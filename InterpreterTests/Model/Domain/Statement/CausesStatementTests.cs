using Interpreter.Model.Domain.Statement;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;

namespace InterpreterTests.Model.Domain.Statement
{
    public class CausesStatementTests
    {
        public static IEnumerable<object?[]> StatementsEqualData =>
        new List<object?[]>
        {
            new object?[] { 
                new CausesStatement(
                    new Fluent("fluent", true), 
                    new Action("Action")),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action")),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action"),
                    new Fluent("Condition", true)),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action"),
                    new Fluent("Condition", true))
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition2", false), new Fluent("Condition1", true) ),
            },
        };

        public static IEnumerable<object?[]> StatementsNotEqualDifferentFluentData =>
        new List<object?[]>
        {
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action")),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action")),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent1", true),
                    new Action("Action"),
                    new Fluent("Condition", true)),
                new CausesStatement(
                    new Fluent("fluent2", true),
                    new Action("Action"),
                    new Fluent("Condition", true))
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent1", true),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
                new CausesStatement(
                    new Fluent("fluent2", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
            },
        };

        public static IEnumerable<object?[]> StatementsNotEqualDifferentActionData =>
        new List<object?[]>
        {
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action1")),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action2")),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action1"),
                    new Fluent("Condition", true)),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action2"),
                    new Fluent("Condition", true))
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action1"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action2"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
            },
        };

        public static IEnumerable<object?[]> StatementsNotEqualDifferentConditionData =>
        new List<object?[]>
        {
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action")),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action"),
                    new Fluent("Condition", true)),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action"),
                    new Fluent("Condition1", true)),
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action"),
                    new Fluent("Condition2", true))
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", false), new Fluent("Condition2", true) ),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false) ),
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", false) ),
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", false), new Fluent("Condition2", false) ),
            },
        };

        public static IEnumerable<object?[]> ToStringData =>
        new List<object?[]>
        {
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", true),
                    new Action("Action")),
                "Action causes fluent"
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition", true)),
                "Action causes ~fluent if",
                "Condition"
            },
            new object?[] {
                new CausesStatement(
                    new Fluent("fluent", false),
                    new Action("Action"),
                    new Fluent("Condition1", true), new Fluent("Condition2", false)),
                "Action causes ~fluent if",
                "Condition1", "~Condition2"
            },
        };

        [Theory]
        [MemberData(nameof(ToStringData))]
        public void ToString_Returns_FormattedStatement(CausesStatement statement, string noConditionPartString, params string[] conditionsStrings)
        {
            string result = statement.ToString();
            Assert.StartsWith(noConditionPartString, result);
            if(conditionsStrings.Any())
            {
                int ifIndex = result.IndexOf(" if ");
                Assert.True(ifIndex > 0);

                int conditionsIndex = ifIndex + 4;
                string resultConditionsString = result.Substring(conditionsIndex);
                string[] resultConditions = resultConditionsString.Split(", ");

                Assert.True(conditionsStrings.ToHashSet().SetEquals(resultConditions.ToHashSet()));
            }
        }

        [Theory]
        [MemberData(nameof(StatementsEqualData))]
        public void Comparison_Returns_True_When_FluentAndActionsAreTheSame(CausesStatement a, CausesStatement b)
        {
            Assert.True(a == b);
        }

        [Theory]
        [MemberData(nameof(StatementsNotEqualDifferentFluentData))]
        public void Comparison_Returns_False_When_FluentAreDifferent(CausesStatement a, CausesStatement b)
        {
            Assert.False(a == b);
        }

        [Theory]
        [MemberData(nameof(StatementsNotEqualDifferentActionData))]
        public void Comparison_Returns_False_When_ActionsAreDifferent(CausesStatement a, CausesStatement b)
        {
            Assert.False(a == b);
        }

        [Theory]
        [MemberData(nameof(StatementsNotEqualDifferentConditionData))]
        public void Comparison_Returns_False_When_ConditionsAreDifferent(CausesStatement a, CausesStatement b)
        {
            Assert.False(a == b);
        }
    }
}
