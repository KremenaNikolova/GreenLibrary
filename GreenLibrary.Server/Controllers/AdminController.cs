namespace GreenLibrary.Server.Controllers
{
    using System.Text.Json;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Extensions;
    using static GreenLibrary.Common.ApplicationConstants;
    using static GreenLibrary.Common.SuccessfulMessage.ArticleSuccesfulMessage;
    using static GreenLibrary.Common.SuccessfulMessage.UserSuccessfulMessages;

    [Authorize]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public AdminController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
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

        [HttpGet("allusers")]
        public async Task<IActionResult> GetAllUsers(int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var (users, paginationMetadata) = await userService.GetAllUsers(page, pageSize);

            Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(users);
        }

        [HttpPut("delete")]
        public async Task<IActionResult> SoftDeleteUser(Guid chooseUserId)
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (isUserLogged)
            {
                await userService.SoftDeleteUser(chooseUserId);

                return Ok(SuccessfullDeleteUser);
            }

            return Unauthorized();
        }
    }
}
