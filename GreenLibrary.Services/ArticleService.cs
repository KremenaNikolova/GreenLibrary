namespace GreenLibrary.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Server.Dtos.Article;
    using System.Linq;
    using GreenLibrary.Services.Dtos.Article;
    using GreenLibrary.Services.Helpers;

    public class ArticleService : IArticleService
    {
        private readonly GreenLibraryDbContext dbContext;

        public ArticleService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> GetAllArticlesAsync(int currentPage, int pageSize)
        {

            var allArticles = dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedOn)
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                    Image = a.Image
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(allArticles, currentPage, pageSize);
            return result;
        }

        public async Task<ArticlesDto?> GetArticleByIdAsync(Guid id)
        {
            var article = await dbContext
                .Articles
                .Where(a => a.Id == id)
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                })
                .FirstOrDefaultAsync();

            return article;
        }

        public async Task<Article> CreateArticleFromDto(CreateArticleDto article, Guid userId)
        {
            var newArticle = new Article()
            {
                UserId = userId,
                Title = article.Title,
                Description = article.Description,
                Image = !string.IsNullOrEmpty(article.ImageName) ? article.ImageName : "default.jpg",
                CategoryId = article.CategoryId
            };


            List<Tag> tags = new List<Tag>();
            Tag tag;
            foreach (var tagName in article.Tags)
            {
                tag = new Tag { Name = tagName, ArticleId = newArticle.Id };
                tags.Add(tag);
            }
            newArticle.Tags = tags;
            await dbContext.Articles.AddAsync(newArticle);

            return newArticle;
        }

        public async Task<(IEnumerable<ArticlesDto>, PaginationMetadata)> SearchedArticlesAsync(string query, int currentPage, int pageSize)
        {
            var articles = dbContext
                .Articles
                .Where(a => a.Title.Contains(query)
                || a.Description.Contains(query)
                || a.User.FirstName.Contains(query)
                || a.User.LastName.Contains(query)
                || a.Category.Name.Contains(query)
                || a.Image.Contains(query)
                || a.Tags.Any(t=>t.Name.Contains(query)))
                .OrderByDescending(a => a.CreatedOn)
                .Select(a=> new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(articles, currentPage, pageSize);
            return result;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
