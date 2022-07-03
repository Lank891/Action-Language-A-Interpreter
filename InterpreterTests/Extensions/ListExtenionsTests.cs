using Interpreter.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTests.Extensions
{
    public class ListExtenionsTests
    {
        public static IEnumerable<object?[]> ListEqualData =>
        new List<object?[]>
        {
            new object?[] { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 } },
            new object?[] { new List<string> { "ab", "cd", "ef" }, new List<string> { "ab", "cd", "ef" } },
            new object?[] { new List<string?> { "ab", null, "ef" }, new List<string?> { "ab", null, "ef" } },
            new object?[] { null, null },
        };

        public static IEnumerable<object?[]> ListNotEqualData =>
        new List<object?[]>
        {
            new object?[] { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 } },
            new object?[] { new List<string> { "ab", "cd", "ef" }, new List<string> { "ab", "cd", "EF" } },
            new object?[] { new List<string?> { "ab", "cd", "ef" }, new List<string?> { "ab", null, "ef" } },
            new object?[] { new List<string?> { "ab", null, "ef" }, new List<string?> { "ab", "cd", "ef" } },
            new object?[] { new List<string> { "ab", "cd", "ef" }, null },
            new object?[] { null, new List<object?> { "ab", "cd", "ef" } },
        };

        [Theory]
        [MemberData(nameof(ListEqualData))]
        public void IsEqualTo_Reurns_True_When_ListsAreEqual<T>(List<T> a, List<T> b)
        {
            Assert.True(a.IsEqualTo(b));
        }

        [Theory]
        [MemberData(nameof(ListNotEqualData))]
        public void IsEqualTo_Reurns_False_When_ListsAreNotEqual<T>(List<T> a, List<T> b)
        {
            Assert.False(a.IsEqualTo(b));
        }
    }
}
