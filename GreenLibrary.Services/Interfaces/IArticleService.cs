namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Server.Dtos.Article;

    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();

        Task<Article?> GetArticleByIdAsync(Guid id);
        
        Article CreateArticleFromDto(CreateArticleDto article);

        Task<Article> CreateAsync(Article article);

    }
}
