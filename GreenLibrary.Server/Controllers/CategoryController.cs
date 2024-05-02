namespace GreenLibrary.Server.Controllers
{
    using System.Text.Json;
    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    
    using GreenLibrary.Services.Dtos.Article;
    using GreenLibrary.Services.Interfaces;
    using static GreenLibrary.Common.ApplicationConstants;

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
        public async Task<ActionResult<IEnumerable<ArticlesDto>>> GetArticlesByCategory(string category, int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var (articles, paginationMetadata) = await categoryService.GetAllArticlesByCategoryNameAsync(category, page, pageSize);

            if (articles == null)
            {
                return NotFound();
            }

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(articles);
        }

    }
}
