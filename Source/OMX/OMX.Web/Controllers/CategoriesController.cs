namespace OMX.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.BindingModels;
    using OMX.Web.Areas.Administration.Models.ViewModels;
    using OMX.Web.Models.ViewModels;

    public class CategoriesController : BaseController
    {
        private const int TopNineCategories = 9;
        public CategoriesController(IOMXData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var categories = 
                this.Data.Categories.All()
                .OrderByDescending(c => c.CreatedOn)
                .ThenBy(c => c.Id)
                .Where(s => s.SubCategories.Any())
                .Take(TopNineCategories)
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            return this.PartialView("_AllCategoriesPartial", categories);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var category = new Category { Title = model.Title };
                this.Data.Categories.Add(category);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CategorySubCategories(int id)
        {
            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound("The selected category no longer exists");
            }
            this.ViewBag.CategoryName = category.Title;

            var subCategories = 
                category.SubCategories
                .AsQueryable()
                .Project()
                .To<MinifiedSubCategoryViewModel>()
                .ToList();

            return this.PartialView("_SubCategoriesPartial", subCategories);
        }
    }
}