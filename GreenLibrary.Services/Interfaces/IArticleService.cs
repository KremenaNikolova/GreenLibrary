namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Server.Dtos.Article;
    using GreenLibrary.Services.Dtos.Article;
    using GreenLibrary.Services.Helpers;

    public interface IArticleService
    {
        Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllApprovedArticlesAsync(int currentPage, int pageSize);

        Task<ArticlesDto?> GetArticleByIdAsync(Guid id);

        Task<Article> CreateArticleFromDto(CreateArticleDto article, Guid userId);

        Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> SearchedArticlesAsync(string query, int currentPage, int pageSize);

        Task<ArticleLike?> AddLikeAsync(Guid articleId, Guid userId);

        Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetUserArticlesAsync(Guid userId, int currentPage, int pageSize);

        Task EditArticleAsync(Guid userId, Guid articleId, CreateArticleDto articleDto);

        Task DeleteArticle(Guid articleId, Guid userId);

        Task SaveAsync();

        Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllArticlesAsync(int currentPage, int pageSize);

        Task ApproveArticle(Guid articleId);

    }
}
