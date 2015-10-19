namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using OMX.Data.UoW;

    public class AdminController : BaseAdminController
    {
        public AdminController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult ManageUsers()
        {
            return this.View();
        }
    }
}