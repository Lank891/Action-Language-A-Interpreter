using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEqualTo<T>(this IReadOnlyList<T>? left, IReadOnlyList<T>? right)
        {
            if (left is null && right is null)
            {
                return true;
            }
            if (left is null || right is null)
            {
                return false;
            }
            if (left.Count != right.Count)
            {
                return false;
            }
            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] == null && right[i] == null)
                {
                    continue;
                }

                if (!left[i]?.Equals(right[i]) ?? true)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
