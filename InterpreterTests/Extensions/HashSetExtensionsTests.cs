using Interpreter.Extensions;
using Interpreter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Extensions
{
    public class HashSetExtensionsTests
    {
        [Fact]
        public void HashSetCopy_Should_CreateDeepCopy()
        {
            HashSet<Fluent> set = new() { new Fluent("a", true) };
            HashSet<Fluent> newSet = set.Copy();

            Assert.Equal(set.ToArray()[0], newSet.ToArray()[0]);
            Assert.NotSame(set.ToArray()[0], newSet.ToArray()[0]);
        }

        [Fact]
        public void ImmutableHashSetCopy_Should_CreateDeepCopy()
        {
            ImmutableHashSet<Fluent> set = new HashSet<Fluent>() { new Fluent("a", true) }.ToImmutableHashSet();
            ImmutableHashSet<Fluent> newSet = set.Copy();

            Assert.Equal(set.ToArray()[0], newSet.ToArray()[0]);
            Assert.NotSame(set.ToArray()[0], newSet.ToArray()[0]);
        }

        [Fact]
        public void HashSetUpdateElement_Should_UpdateElementWithNewHashCodeWorkingProperly()
        {
            HashSet<Fluent> set = new() { new Fluent("a", true) };
            Assert.Contains(new Fluent("a", true), set);
            
            set.UpdateElement(set.ToArray()[0], item => item.Name = "b");
            Assert.Contains(new Fluent("b", true), set);
        }
    }
}
