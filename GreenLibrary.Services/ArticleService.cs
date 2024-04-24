namespace GreenLibrary.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Server.Dtos.Article;

    public class ArticleService : IArticleService
    {
        private readonly GreenLibraryDbContext dbContext;

        public ArticleService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            var allArticles = await dbContext
                .Articles
                .OrderByDescending(a => a.CreatedOn)
                .ToArrayAsync();

            return allArticles;
        }

        public async Task<Article?> GetArticleByIdAsync(Guid id)
        {
            var article = await dbContext
                .Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return article;
        }

        public Article CreateArticleFromDto(CreateArticleDto article)
        {
            var newArticle = new Article()
            {
                UserId = article.UserId,
                Title = article.Title,
                Description = article.Description,
                Image = article.ImagePath,
                CategoryId = article.CateogoryId
            };

            return newArticle;
        }

        public async Task<Article> CreateAsync(Article article)
        {
            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();
            return article;
        }

       
    }
}
