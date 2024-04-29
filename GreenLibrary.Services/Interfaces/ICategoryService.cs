namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Server.Dtos.Category;
    using GreenLibrary.Services.Dtos.Article;

    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();

        Task<IEnumerable<ArticlesDto>> GetAllArticlesByCategoryNameAsync(string categoryName);
    }
}
