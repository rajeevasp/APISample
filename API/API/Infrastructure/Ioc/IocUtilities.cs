using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using APIApplication.Service;
using APIApplication.Services;
using API.Data;
using API.Data.Repository;
using API.Domain.Repository;
using API.Domain.Services;

namespace API.Infrastructure.Ioc
{
    public static class IocUtilities
    {
        /// <summary>
        /// Build the simple injector container and register all the services.
        /// </summary>
        /// <returns><see cref="SimpleInjector.Container"/></returns>
        public static Container SetupContainer()
        {
            var container = new Container();
            container.RegisterApiControllers();

            #region Services
            
            //container.RegisterPerWebRequest<IBlogService, BlogService>();
           

            //container.RegisterPerWebRequest<IApiService, ApiService>();

            #endregion

            #region Records

            
            container.RegisterPerWebRequest<IBlogRepository, BlogRepository>();
           
            container.RegisterPerWebRequest<IApiLogRecords, ApiLogRecords>();
            container.RegisterPerWebRequest<IApiUserRecords, ApiUserRecords>();
            container.RegisterPerWebRequest<DomainNameDbContext>(() => new DomainNameDbContext());

            #endregion

            //Check for any potential incompatible services and fail before application start
            container.Verify();
            return container;
        }
    }
}