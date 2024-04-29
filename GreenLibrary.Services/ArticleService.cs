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
    using Microsoft.AspNetCore.Http;

    public class ArticleService : IArticleService
    {
        private readonly GreenLibraryDbContext dbContext;

        public ArticleService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ArticlesDto>> GetAllArticlesAsync()
        {

            var allArticles = await dbContext
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
                .ToArrayAsync();

            return allArticles;
        }

        public async Task<ArticlesDto?> GetArticleByIdAsync(Guid id)
        {
            var article = await dbContext
                .Articles
                .Where(a => a.Id == id)
                .Select(a=> new ArticlesDto()
                {
                    Id=a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString(),
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

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
