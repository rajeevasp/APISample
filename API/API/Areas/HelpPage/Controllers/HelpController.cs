using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Linq;
using API.Areas.HelpPage.Models;
using API.Infrastructure.Attributes;
using API.Infrastructure.Extensions;

namespace API.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            var apiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions
                .Where(w => !w.ActionDescriptor.ControllerDescriptor.ControllerType
                    .IsDefined(typeof(HideFromDocumentation), false));

            return View(apiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View("Error");
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult Auth()
        {
            return View();
        }

        public FileResult DownloadExampleApp()
        {
            var path = Server.MapPath("~/App_Data/ExampleConsumer.zip");
            return File(path, "application/zip", "exampleconsumer.zip");
        }
    }
}