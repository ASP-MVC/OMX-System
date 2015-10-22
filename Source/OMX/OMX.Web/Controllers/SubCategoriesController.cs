namespace OMX.Web.Controllers
{
    using System.Web.Mvc;

    using OMX.Data.UoW;
    using OMX.Infrastructure.Populators;
    using OMX.Models;
    using OMX.Web.Models.BindingModels;

    public class SubCategoriesController : BaseController
    {
        private IDropDownListPopulator populator;

        public SubCategoriesController(IOMXData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var subCatModel = new CreateSubCategoryBindingModel
            {
                Categories = this.populator.GetCategories()
            };

            return this.View(subCatModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSubCategoryBindingModel model)
        {
            if(model != null && this.ModelState.IsValid)
            {
                var subCategory = new SubCategory
                {
                    Title = model.Title,
                    CategoryId = model.CategoryId
                };
                this.Data.SubCategories.Add(subCategory);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }

            model.Categories = this.populator.GetCategories();
            return this.View(model);
        }
    }
}