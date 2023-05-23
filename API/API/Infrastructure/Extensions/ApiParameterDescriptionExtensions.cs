using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Reflection;

namespace API.Infrastructure.Extensions
{
    public static class ApiParameterDescriptionExtensions
    {
        /// <summary>
        /// Gets a friendly representation for the parameter type
        /// </summary>
        /// <param name="parameter"><see cref="ApiParameterDescription"/></param>
        /// <returns>string</returns>
        public static string GetFriendlyParameterType(this ApiParameterDescription parameter)
        {
            var type = parameter.ParameterDescriptor.ParameterType.Name;

            switch (type)
            {
                case "Int64": return "int";
                case "Int32": return "int";
                case "Int16": return "int";
                case "double": return "decimal";
                case "Double": return "decimal";
                case "Decimal": return "decimal";
                case "Boolean": return "bool";
                case "String": return "string";
                default: return type;
            }
        }
    }
}