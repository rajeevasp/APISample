using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace API.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);

            BundleTable.Bundles.Add(new ScriptBundle("~/help/styles").Include(
                "~/Areas/HelpPage/Content/HelpPage.css",
                "~/Areas/HelpPage/Content/TestClient.css",
                "~/Content/themes/base/jquery.ui.all.css"));

            BundleTable.Bundles.Add(new ScriptBundle("~/help/scripts").Include(
                "~/Scripts/jquery-1.6.4.js",
                "~/Scripts/jquery-ui-1.9.2.js",
                "~/Scripts/knockout-2.2.1.js",
                "~/Scripts/WebApiTestClient.js"));
        }
    }
}