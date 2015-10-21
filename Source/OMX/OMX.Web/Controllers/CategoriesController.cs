namespace OMX.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OMX.Data.UoW;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.BindingModels;
    using OMX.Web.Areas.Administration.Models.ViewModels;
    
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
    }
}