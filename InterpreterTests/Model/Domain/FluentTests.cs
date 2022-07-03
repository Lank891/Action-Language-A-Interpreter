using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Model.Domain
{
    public class FluentTests
    {
        [Theory]
        [InlineData("Fluent", true, "Fluent")]
        [InlineData("Fluent", false, "~Fluent")]
        public void ToString_Returns_NameWithState(string fluentName, bool state, string expectedString)
        {
            Fluent fluent = new(fluentName, state);
            Assert.Equal(expectedString, fluent.ToString());
        }

        [Theory]
        [InlineData("Fluent", true, "Fluent", true)]
        public void Comparison_Returns_True_When_NamesAndStatesAreTheSame(string name1, bool state1, string name2, bool state2)
        {
            Fluent f1 = new(name1, state1);
            Fluent f2 = new(name2, state2);
            Assert.True(f1 == f2);
        }

        [Theory]
        [InlineData("Fluent1", true, "Fluent2", true)]
        public void Comparison_Returns_False_When_NamesAreNotTheSame(string name1, bool state1, string name2, bool state2)
        {
            Fluent f1 = new(name1, state1);
            Fluent f2 = new(name2, state2);
            Assert.False(f1 == f2);
        }

        [Theory]
        [InlineData("Fluent", true, "Fluent", false)]
        public void Comparison_Returns_False_When_StatesAreNotTheSame(string name1, bool state1, string name2, bool state2)
        {
            Fluent f1 = new(name1, state1);
            Fluent f2 = new(name2, state2);
            Assert.False(f1 == f2);
        }

        [Theory]
        [InlineData("Fluent1", true, "Fluent2", false)]
        public void Comparison_Returns_False_When_NamesAndStatesAreNotTheSame(string name1, bool state1, string name2, bool state2)
        {
            Fluent f1 = new(name1, state1);
            Fluent f2 = new(name2, state2);
            Assert.False(f1 == f2);
        }
    }
}
