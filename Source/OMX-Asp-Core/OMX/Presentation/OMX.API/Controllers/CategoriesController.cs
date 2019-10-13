namespace OMX.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OMX.Application.Categories.Commands.CreateCategory;
    using OMX.Application.Categories.Queries.GetCategoriesWithSubCategories;
    using OMX.Application.Categories.Queries.GetCategorySubCategories;
    using System.Threading.Tasks;

    public class CategoriesController : BaseController
    {
        private const int TopNineCategories = 9;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await Mediator.Send(new CategoriesWithSubCategoriesQuery());

            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopCategories()
        {
            var categories = await Mediator.Send(new CategoriesWithSubCategoriesQuery() { TakeCount = TopNineCategories });

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (command != null && ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction("Index", "Home");
            }

            return Created("some-url", command);
        }

        [HttpGet]
        public async Task<IActionResult> CategorySubCategories(int id)
        {
            var subCategories = await Mediator.Send(new GetCategorySubCategoriesQuery() { CategoryId = id });
            
            return Ok(subCategories);
        }
    }
}
