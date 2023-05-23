using Newtonsoft.Json;
using SimpleInjector;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

using API.Infrastructure.Attributes;
using API.Infrastructure.Formatters;
using API.Infrastructure.Handlers;
using API.Infrastructure.Ioc;
using API.Infrastructure.Selectors;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using System.Collections.Generic;


namespace API
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Setup the api, configure routes, ioc, query support and formatters.
        /// </summary>
        /// <param name="config"><see cref="HttpConfiguration"/></param>
        public static void Register(HttpConfiguration config)
        {
            #region Routes

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Error404",
                routeTemplate: "{*url}",
                defaults: new { controller = "Error", action = "Handle404" }
            );

            #endregion

            #region Filters

            config.Filters.Add(new ExceptionHandler());
            //config.Filters.Add(new RequireKeyAttribute());

            #endregion

            #region Handlers

            config.MessageHandlers.Add(new AuditHandler());

            #endregion

            #region formatters

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());


            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("fmt", "json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("fmt", "xml", "text/xml"));
            config.Formatters.XmlFormatter.UseXmlSerializer = true;


            #endregion

            #region Selectors

            config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareControllerSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareActionSelector());

            #endregion

            //Globally enable odata queries for IQueryable / IQueryable<T>
            //config.EnableQuerySupport();

            //Setup Ioc using simple injector
            var container = IocUtilities.SetupContainer();
            config.DependencyResolver = new SimpleInjectorWebApiResolver(container);
        }
    }
}
