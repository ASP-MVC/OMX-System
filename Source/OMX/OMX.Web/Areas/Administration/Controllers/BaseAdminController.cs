namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using OMX.Common;
    using OMX.Data.UoW;
    using OMX.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IOMXData data)
            : base(data)
        {
        }
    }
}