using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;



namespace API.Utilities.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Extension method to include multiple included entities
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="query">The EF query</param>
        /// <param name="includes">The entities to include</param>
        /// <returns>A list of T with eager loaded entities</returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
