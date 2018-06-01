using System;
using System.Collections.Generic;
using System.Text;

namespace Gedcom4Sharp.Utility.Extensions
{
    public static class ArrayExtensions
    {
        public static void Fill<T>(this IList<T> col, int fromIndex, int toIndex, T value)
        {
            if (fromIndex > toIndex)
                throw new ArgumentOutOfRangeException("fromIndex");

            for (var i = fromIndex; i <= toIndex; i++)
                col[i] = value;
        }
    }
}
