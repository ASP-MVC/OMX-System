namespace OMX.Web.Controllers
{
    using System.Web.Mvc;

    using OMX.Data.UoW;

    public class CategoriesController : BaseController
    {
        public CategoriesController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            return this.View();
        }
    }
}