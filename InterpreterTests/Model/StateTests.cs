using Interpreter.Model;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Model
{
    public class StateTests
    {
        [Fact]
        public void TwoStates_Should_HaveTheSameHashCode_When_TheyHaveTheSameFluents()
        {
            State state1 = new (new HashSet<Fluent>() { new Fluent("a", true) });
            State state2 = new (new HashSet<Fluent>() { new Fluent("a", true) });

            Assert.Equal(state1.GetHashCode(), state2.GetHashCode());
        }

        [Fact]
        public void TwoStates_Should_HaveDifferentHashCode_When_TheyHaveDifferentFluents()
        {
            State state1 = new (new HashSet<Fluent>() { new Fluent("a", true) });
            State state2 = new (new HashSet<Fluent>() { new Fluent("b", true) });

            Assert.NotEqual(state1.GetHashCode(), state2.GetHashCode());
        }
    }
}
