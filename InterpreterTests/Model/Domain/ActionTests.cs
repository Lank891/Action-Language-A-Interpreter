using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Model.Domain
{
    public class ActionTests
    {
        [Theory]
        [InlineData("Action")]
        public void ToString_Returns_Name(string actionName)
        {
            Interpreter.Model.Domain.Action action = new(actionName);
            Assert.Equal(actionName, action.ToString());
        }

        [Theory]
        [InlineData("Action", "Action")]
        public void Comparison_Returns_True_When_NamesAreTheSame(string actionName1, string actionName2)
        {
            Interpreter.Model.Domain.Action action1 = new(actionName1);
            Interpreter.Model.Domain.Action action2 = new(actionName2);
            Assert.True(action1 == action2);
        }

        [Theory]
        [InlineData("Action", "OtherAction")]
        public void Comparison_Returns_False_When_NamesAreNotTheSame(string actionName1, string actionName2)
        {
            Interpreter.Model.Domain.Action action1 = new(actionName1);
            Interpreter.Model.Domain.Action action2 = new(actionName2);
            Assert.False(action1 == action2);
        }
    }
}
