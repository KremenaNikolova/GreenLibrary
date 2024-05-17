namespace GreenLibrary.Server.Controllers
{
    using System.Text.Json;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using GreenLibrary.Extensions;
    using GreenLibrary.Server.Dtos.Article;
    using GreenLibrary.Services.Interfaces;
    using static GreenLibrary.Common.ApplicationConstants;
    using static GreenLibrary.Common.ErrorMessages.ArticleErrorMessages;
    using static GreenLibrary.Common.SuccessfulMessage.ArticleSuccesfulMessage;

    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetArticles(int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var (articles, paginationMetadata) = await articleService.GetAllArticlesAsync(page, pageSize);

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));

            if(!User.Identity.IsAuthenticated) 
            {
                Response.Headers.Append("X-Auth-Status", "401");
            }

            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpGet("details")]
        public async Task<IActionResult> GetArticle(Guid articleId)
        {
            var article = await articleService.GetArticleByIdAsync(articleId);
            return Ok(article);
        }

        [HttpPost("details/like")]
        public async Task<IActionResult> LikeArticle(Guid articleId)
        {
            var userId = Guid.Parse(User.GetId()!);
            //var userId = Guid.Parse("59dc4c83-cf09-48da-a0df-6e07187b910b");

            var articleLike = await articleService.AddLikeAsync(articleId, userId);
            await articleService.SaveAsync();
            
            if(articleLike == null)
            {
                return Ok(false);
            }

            return Ok(true);
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

            //var userId = Guid.Parse("59dc4c83-cf09-48da-a0df-6e07187b910b");

            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (!isUserLogged)
            {
                return Unauthorized();
            }

            var newArticle = await articleService.CreateArticleFromDto(article, userId);

            if (newArticle == null)
            {
                return BadRequest();
            }

            await articleService.SaveAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = newArticle.Id }, newArticle);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchArticles(string query, int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var (articles, paginationMetadata) = await articleService.SearchedArticlesAsync(query, page, pageSize);

            if (!articles.Any())
            {
                return NotFound(NotFountArticles);
            }

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(articles);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditArticle(Guid articleId, [FromForm] CreateArticleDto articleDto)
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (!isUserLogged)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            if (articleDto.ImageFile != null && articleDto.ImageFile.Length > 0)
            {
                var filePath = await imageService.SaveImageAsync(articleDto.ImageFile);
                articleDto.ImageName = filePath;
            }

            await articleService.EditArticleAsync(userId, articleId, articleDto);
            await articleService.SaveAsync();

            return Ok(SuccessfullEditedArticle);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(Guid articleId)
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (!isUserLogged)
            {
                return Unauthorized();
            }

            await articleService.DeleteArticle(articleId, userId);

            return Ok(SuccessfullDeletedArticle);
        }

    }
}
