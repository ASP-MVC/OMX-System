namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OMX.Data.UoW;
    using OMX.Web.Models.ViewModels;

    public class UsersController : KendoAdminGridController
    {
        public UsersController(IOMXData data)
            : base(data)
        {
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Project().To<UserViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.GetById(id) as T;
        }
    }
}