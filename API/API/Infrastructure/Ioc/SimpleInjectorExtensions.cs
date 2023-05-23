using SimpleInjector;
using System.Web.Http;

namespace API.Infrastructure.Ioc
{
    public static class SimpleInjectorExtensions
    {
        /// <summary>
        /// Registers all Web API controllers with the simple injector container
        /// </summary>
        /// <param name="container"><see cref="SimpleInjector.Container"/></param>
        public static void RegisterApiControllers(this Container container)
        {
            var services = GlobalConfiguration.Configuration.Services;
            var controllerTypes = services.GetHttpControllerTypeResolver()
                .GetControllerTypes(services.GetAssembliesResolver());

            foreach (var controllerType in controllerTypes)
                container.Register(controllerType);
        }
    }
}