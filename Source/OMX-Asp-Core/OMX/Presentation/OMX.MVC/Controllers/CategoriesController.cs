namespace OMX.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OMX.Application.Categories.Commands.CreateCategory;
    using OMX.Application.Categories.Queries.GetCategoriesWithSubCategories;
    using OMX.Application.Categories.Queries.GetCategorySubCategories;
    using System.Threading.Tasks;

    public class CategoriesController : BaseController
    {
        private const int TopNineCategories = 9;

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await Mediator.Send(new CategoriesWithSubCategoriesQuery());

            return PartialView("_AllCategoriesPartial", categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopCategories()
        {
            var categories = await Mediator.Send(new CategoriesWithSubCategoriesQuery() { TakeCount = TopNineCategories });

            return PartialView("_AllCategoriesPartial", categories);
        }

        [HttpGet]
       // [Authorize]
        public ActionResult Create()
            => View();

        [HttpPost]
       // [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (command != null && ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction("Index", "Home");
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> CategorySubCategories(int id)
        {
            var subCategories = await Mediator.Send(new GetCategorySubCategoriesQuery() { CategoryId = id });
            
            return PartialView("_SubCategoriesPartial", subCategories);
        }
    }
}
