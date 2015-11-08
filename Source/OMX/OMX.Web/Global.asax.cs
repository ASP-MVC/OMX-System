namespace OMX.Web
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using OMX.Data;
    using OMX.Data.Migrations;
    using OMX.Infrastructure.Mappings;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            this.LoadAutoMapper();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void LoadAutoMapper()
        {
            var autoMapperConfig = new AutoMapperConfig(new List<Assembly>() { Assembly.GetExecutingAssembly() });
            autoMapperConfig.Execute();
        }
    }
}