namespace OMX.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
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

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryBindingModel model)
        {
            this.ModelState.Remove("Id");
            if (model != null && this.ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(model);
                this.Data.Categories.Add(category);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest]DataSourceRequest request, CategoryBindingModel model)
        {
            var category = this.Data.Categories.GetById(model.Id);
            category.Title = model.Title;
            this.ChangeEntityStateAndSave(category, EntityState.Modified);
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