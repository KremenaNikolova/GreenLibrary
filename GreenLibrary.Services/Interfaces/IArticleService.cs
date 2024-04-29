namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Server.Dtos.Article;
    using GreenLibrary.Services.Dtos.Article;

    public interface IArticleService
    {
        Task<IEnumerable<ArticlesDto>> GetAllArticlesAsync();

        Task<ArticlesDto?> GetArticleByIdAsync(Guid id);

        Task<Article> CreateArticleFromDto(CreateArticleDto article, Guid userId);

        Task SaveAsync();

    }
}
