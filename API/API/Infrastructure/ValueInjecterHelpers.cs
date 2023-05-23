using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Omu.ValueInjecter;

namespace API.Infrastructure
{
    public static class ValueInjecterHelpers
    {
        public static IList<Destination> InjectFrom<Source, Destination>(this IList<Destination> destination, params IEnumerable<Source>[] sources) where Destination : new()
        {
            foreach (var item in sources)
            {
                foreach (var source in item)
                {
                    var target = new Destination();
                    target.InjectFrom(source);
                    destination.Add(target);
                }
            }
            return destination;
        }
    }
}