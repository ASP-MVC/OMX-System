namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.BindingModels;
    using OMX.Web.Areas.Administration.Models.ViewModels;

    public class CategoriesController : KendoAdminGridController
    {
        public CategoriesController(IOMXData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Categories.All().Project().To<CategoryViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryBindingModel model)
        {
            var dbModel = base.Create<Category>(model);
            if (dbModel != null)
            {
                model.Id = dbModel.Id;
            }
            return this.GridOperation(model, request);
        }
        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryBindingModel model)
        {
            base.Update<Category, CategoryBindingModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.Data.Categories.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}