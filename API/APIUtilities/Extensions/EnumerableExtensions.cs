using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// A simple extension method to check whether an IEnumerable list
        /// of type is empty or not.
        /// </summary>
        /// <typeparam name="T">The type of the List</typeparam>
        /// <param name="source">The actual list</param>
        /// <returns>bool empty or not</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null) ? true : !source.Any();
        }
    }
}
