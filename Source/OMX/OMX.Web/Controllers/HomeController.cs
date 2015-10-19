namespace OMX.Web.Controllers
{
    using System.Web.Mvc;

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
    }
}