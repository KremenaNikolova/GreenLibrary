namespace GreenLibrary.Server.Controllers
{
    using GreenLibrary.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }


        [HttpGet("{category}")]
        public async Task<IActionResult> GetArticlesByCategory(string category)
        {
            var articles = await categoryService.GetAllArticlesByCategoryNameAsync(category);

            return Ok(articles);
        }
    }
}
