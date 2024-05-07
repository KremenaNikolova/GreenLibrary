namespace GreenLibrary.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;
    
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Server.Dtos.Category;
    using GreenLibrary.Services.Dtos.Article;
    using GreenLibrary.Services.Helpers;

    using static GreenLibrary.Common.ApplicationConstants;

    public class CategoryService : ICategoryService
    {
        private readonly GreenLibraryDbContext dbContext;

        public CategoryService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await dbContext
                .Categories
                .Where(c=>c.IsDeleted == false)
                .Select(c=> new CategoryDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categories;
        }

        public async Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllArticlesByCategoryNameAsync(string categoryName, int currentPage, int pageSize)
        {
            var articles = dbContext
                .Articles
                .Where(a => a.Category.Name == categoryName)
                .OrderByDescending(a => a.CreatedOn)
                .Select(a => new ArticlesDto
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(articles, currentPage, pageSize);
            return result;

        }

    }
}
