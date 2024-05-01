namespace GreenLibrary.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using GreenLibrary.Server.Dtos.Article;
    using GreenLibrary.Services.Interfaces;

    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IImageService imageService)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.imageService = imageService;

        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var allArticles = await articleService.GetAllArticlesAsync();

            return Ok(allArticles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var article = await articleService.GetArticleByIdAsync(id);
            return Ok(article);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDto article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (article.ImageFile != null && article.ImageFile.Length > 0)
            {
                var filePath = await imageService.SaveImageAsync(article.ImageFile);
                article.ImageName = filePath;
            }

            article.ImageName ??= "default.jpg"; //if the imagename is null it will set it to default.jpg

            var userId = Guid.Parse("59dc4c83-cf09-48da-a0df-6e07187b910b");
            var newArticle = await articleService.CreateArticleFromDto(article, userId);

            if (newArticle == null)
            {
                return BadRequest();
            }

            await articleService.SaveAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = newArticle.Id }, newArticle);
        }

    }
}
