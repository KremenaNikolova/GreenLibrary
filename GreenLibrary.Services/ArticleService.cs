namespace GreenLibrary.Services
{
    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

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
                .OrderByDescending(a=>a.CreatedOn)
                .ToArrayAsync();

            return allArticles;
        }
    }
}
