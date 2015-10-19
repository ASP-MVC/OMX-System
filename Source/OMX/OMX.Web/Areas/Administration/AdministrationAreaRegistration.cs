using System.Web.Mvc;

namespace OMX.Web.Areas.Administration
{
    using OMX.Common;

    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return GlobalConstants.AdministrationArea; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}