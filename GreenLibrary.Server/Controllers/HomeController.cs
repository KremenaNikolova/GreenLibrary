namespace GreenLibrary.Server.Controllers
{
    using GreenLibrary.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allArticles = await articleService.GetAllArticlesAsync();

            return Ok(allArticles);
        }
    }
}
