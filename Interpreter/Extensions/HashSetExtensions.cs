using Interpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Extensions
{
    public static class HashSetExtensions
    {
        public static HashSet<T> Copy<T>(this HashSet<T> set) where T : ICopyable<T>
        {
            HashSet<T> newSet = new();
            foreach (var item in set)
                newSet.Add(item.Copy());
            return newSet;
        }

        public static ImmutableHashSet<T> Copy<T>(this ImmutableHashSet<T> set) where T : ICopyable<T>
        {
            HashSet<T> newSet = new();
            foreach (var item in set)
                newSet.Add(item.Copy());
            return newSet.ToImmutableHashSet();
        }
    }
}
