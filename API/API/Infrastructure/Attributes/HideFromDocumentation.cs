using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Infrastructure.Attributes
{
    /// <summary>
    /// Attribute to mark classes to support documentation or not
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HideFromDocumentation : Attribute
    {
    }
}