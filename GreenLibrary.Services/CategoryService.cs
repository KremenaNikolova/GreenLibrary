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
    using GreenLibrary.Data.Entities;

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

        public async Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllArticlesByCategoryNameAsync(string categoryName, int currentPage, int pageSize, string sortBy)
        {
            var articles = dbContext
                .Articles
                .Where(a => a.Category.Name == categoryName && a.IsApproved == true)
                .OrderByDescending(a => a.CreatedOn)
                .AsQueryable();

            var sortedArticles = SortArticles(articles, sortBy);

            var result = await PaginationHelper.CreatePaginatedResponseAsync(sortedArticles, currentPage, pageSize);
            return result;

        }

        private IQueryable<ArticlesDto> SortArticles(IQueryable<Article> articlesDto, string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "title-asc":
                    articlesDto = articlesDto.OrderBy(p => p.Title);
                    break;
                case "title-desc":
                    articlesDto = articlesDto.OrderByDescending(p => p.Title);
                    break;
                case "createon-newest":
                    articlesDto = articlesDto.OrderByDescending(p => p.CreatedOn);
                    break;
                case "createon-oldest":
                    articlesDto = articlesDto.OrderBy(p => p.CreatedOn);
                    break;
                default:
                    articlesDto = articlesDto.OrderByDescending(p => p.CreatedOn);
                    break;
            }

            var result = articlesDto
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                    UserId = a.UserId,
                    Image = a.Image,
                    Likes = a.ArticleLikes.Count(),
                    IsApproved = a.IsApproved,
                })
                .AsQueryable();

            return result;
        }

    }
}
