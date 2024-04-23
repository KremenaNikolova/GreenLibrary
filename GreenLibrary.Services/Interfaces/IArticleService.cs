namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;

    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
    }
}
