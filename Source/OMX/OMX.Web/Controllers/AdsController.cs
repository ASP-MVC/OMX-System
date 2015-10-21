namespace OMX.Web.Controllers
{
    using System.Web.Mvc;

    using OMX.Data.UoW;

    public class AdsController : BaseController
    {
        public AdsController(IOMXData data)
            : base(data)
        {
        }

        // GET: Ads for sub category
        public ActionResult Ads(int id)
        {
            return this.View();
        }
    }
}