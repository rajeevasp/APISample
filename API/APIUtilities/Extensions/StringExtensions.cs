using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Trims a given amount of chars from a string form the end
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="count">Amount of chars to remove</param>
        /// <returns>String</returns>
        public static string TrimEnd(this string source, int count)
        {
            return source.Remove(source.Length -count, count);
        }

        /// <summary>
        /// Trims a given amount of chars from a string form the end
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="count">Amount of chars to remove</param>
        /// <returns>String</returns>
        public static string TrimStart(this string source, int count)
        {
            return source.Remove(0, count);
        }

    }
}
