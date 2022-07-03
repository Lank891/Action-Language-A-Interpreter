using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Model.Domain.Statement
{
    public class AfterStatementTests
    {
        public static IEnumerable<object?[]> StatementsEqualData =>
        new List<object?[]>
        {
            new object?[] { new AfterStatement(new Fluent("fluent", true)), new AfterStatement(new Fluent("fluent", true)) },
            new object?[] { new AfterStatement(new Fluent("fluent", false), "Action1"), new AfterStatement(new Fluent("fluent", false), "Action1") },
            new object?[] { new AfterStatement(new Fluent("fluent", true), "Action1", "Action2"), new AfterStatement(new Fluent("fluent", true), "Action1", "Action2") },
        };

        public static IEnumerable<object?[]> StatementsNotEqualDifferentFluentData =>
        new List<object?[]>
        {
            new object?[] { new AfterStatement(new Fluent("fluent", true)), new AfterStatement(new Fluent("fluent", false)) },
            new object?[] { new AfterStatement(new Fluent("fluent1", false), "Action1"), new AfterStatement(new Fluent("fluent2", false), "Action1") },
            new object?[] { new AfterStatement(new Fluent("fluent1", true), "Action1", "Action2"), new AfterStatement(new Fluent("fluent2", false), "Action1", "Action2") },
        };

        public static IEnumerable<object?[]> StatementsNotEqualDifferentActionData =>
        new List<object?[]>
        {
            new object?[] { new AfterStatement(new Fluent("fluent", true)), new AfterStatement(new Fluent("fluent", true), "Action1") },
            new object?[] { new AfterStatement(new Fluent("fluent", false), "Action1"), new AfterStatement(new Fluent("fluent", false)) },
            new object?[] { new AfterStatement(new Fluent("fluent", true), "Action1", "Action2"), new AfterStatement(new Fluent("fluent", true), "Action1") },
        };

        [Theory]
        [InlineData("fluent", true, "initially fluent")]
        [InlineData("fluent", false, "initially ~fluent")]
        [InlineData("fluent", true, "fluent after Action", "Action")]
        [InlineData("fluent", false, "~fluent after Action1, Action2", "Action1", "Action2")]
        public void ToString_Returns_FormattedStatement(string fluentName, bool fluentState, string expectedString, params string[] actionNames)
        {
            Fluent fluent = new(fluentName, fluentState);
            AfterStatement statement = new(fluent, actionNames);
            Assert.Equal(expectedString, statement.ToString());
        }

        [Theory]
        [MemberData(nameof(StatementsEqualData))]
        public void Comparison_Returns_True_When_FluentAndActionsAreTheSame(AfterStatement a, AfterStatement b)
        {
            Assert.True(a == b);
        }

        [Theory]
        [MemberData(nameof(StatementsNotEqualDifferentFluentData))]
        public void Comparison_Returns_False_When_FluentAreDifferent(AfterStatement a, AfterStatement b)
        {
            Assert.False(a == b);
        }

        [Theory]
        [MemberData(nameof(StatementsNotEqualDifferentActionData))]
        public void Comparison_Returns_False_When_ActionsAreDifferent(AfterStatement a, AfterStatement b)
        {
            Assert.False(a == b);
        }
    }
}
