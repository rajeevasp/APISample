using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace API.Infrastructure.Ioc
{
    public class SimpleInjectorWebApiScope : IDependencyScope
    {
        private Container container;

        /// <summary>
        /// Initialize <see cref="SimpleInjectorWebApiScore"/>
        /// </summary>
        /// <param name="container">Container to create</param>
        public SimpleInjectorWebApiScope(Container container)
        {
            if (container == null) 
                throw new ArgumentNullException("Simple injector container");
            this.container = container;
        }

        /// <summary>
        /// Resolve a single object of a given type
        /// </summary>
        /// <param name="serviceType">Type to resolve</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            if (container == null)
                throw new ObjectDisposedException("this", "Scope already disposed");

            try
            {
                return container.GetInstance(serviceType);
            }
            catch (ActivationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves a collection of objects of a given type
        /// </summary>
        /// <param name="serviceType">Type to resolve</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (container == null)
                throw new ObjectDisposedException("this", "Scope already disposed");

            try
            {
                return container.GetAllInstances(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Dont need to implement as the simpleinjecterresolver returns
        /// 'this' for begin scope
        /// </summary>
        public void Dispose()
        {
        }
    }
}