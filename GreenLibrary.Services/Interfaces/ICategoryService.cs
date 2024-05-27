namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Server.Dtos.Category;
    using GreenLibrary.Services.Dtos.Article;
    using GreenLibrary.Services.Helpers;

    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();

        Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllArticlesByCategoryNameAsync(string categoryName, int currentPage, int pageSize, string sortBy);
    }
}
