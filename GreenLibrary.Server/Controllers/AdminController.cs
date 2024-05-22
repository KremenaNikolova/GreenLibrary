namespace GreenLibrary.Server.Controllers
{
    using System.Text.Json;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using GreenLibrary.Services.Interfaces;
    using static GreenLibrary.Common.ApplicationConstants;
    using static GreenLibrary.Common.SuccessfulMessage.ArticleSuccesfulMessage;

    [Authorize]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IArticleService articleService;

        public AdminController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetAllArticles(int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var (articles, paginationMetadata) = await articleService.GetAllArticlesAsync(page, pageSize);

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(articles);
        }

        [HttpPost("articles")]
        public async Task<IActionResult> ApproveArticle(Guid articleId)
        {
            await articleService.ApproveArticle(articleId);

            return Ok(SuccessfullApprovedArticle);
        }
    }
}
