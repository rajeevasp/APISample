using SimpleInjector;
using System.Web.Http.Dependencies;

namespace API.Infrastructure.Ioc
{
    public class SimpleInjectorWebApiResolver : SimpleInjectorWebApiScope, IDependencyResolver
    {
        /// <summary>
        /// Get the container from the Scope
        /// </summary>
        /// <param name="container"><see cref="SimpleInjector.Container"/></param>
        public SimpleInjectorWebApiResolver(Container container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }   
    }
}