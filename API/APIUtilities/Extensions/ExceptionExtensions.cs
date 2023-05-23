using System;
using System.Linq;

namespace API.Utilities.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Write out all the exception field and values to a string for
        /// storing in a database field.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>string exception</returns>
        public static string GetExceptionDetails(this Exception exception) 
        {
            if (exception == null)
                return string.Empty;
            var properties = exception.GetType().GetProperties();
            var fields = properties
                .Select(s => new {
                    Name = s.Name,
                    Value = s.GetValue(exception, null)
                })
                .Select(s => string.Format("{0} - {1}", s.Name,
                    s.Value != null ? s.Value.ToString() : string.Empty));
            return string.Join("\n", fields);
        }
    }
}
