namespace OMX.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.UI;

    using OMX.Data.UoW;

    public class HomeController : BaseController
    {
        public HomeController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Footer()
        {
            return this.PartialView("_FooterPartial");
        }
    }
}