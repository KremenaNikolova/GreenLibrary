﻿namespace GreenLibrary.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Server.Dtos.Article;
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
                    Image = a.Image,
                    Likes = a.ArticleLikes.Count()
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(allArticles, currentPage, pageSize);
            return result;
        }

        public async Task<ArticlesDto?> GetArticleByIdAsync(Guid id)
        {
            var article = await dbContext
                .Articles
                .Where(a => a.Id == id && a.User.IsDeleted == false)
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                    Likes = a.ArticleLikes.Count()
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
                .Where(a => a.User.IsDeleted == false 
                && (a.Title.Contains(query)
                || a.Description.Contains(query)
                || a.User.FirstName.Contains(query)
                || a.User.LastName.Contains(query)
                || a.Image.Contains(query)
                || a.Tags.Any(t => t.Name.Contains(query))))
                .OrderByDescending(a => a.CreatedOn)
                .ThenByDescending(a => a.Tags.Where(t => t.Name.Contains(query)).Count())
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                    Likes = a.ArticleLikes.Count()
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(articles, currentPage, pageSize);

            result.Item1 = result.Item1
                .OrderByDescending(i => i.Description.Split(new [] { ' ', ',', '-', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(word => word.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            return result;
        }

        public async Task<ArticleLike?> AddLikeAsync(Guid articleId, Guid userId)
        {
            var articleLike = await dbContext
                .ArticlesLikes
                .Where(a => a.ArticleId == articleId
                && a.UserId == userId)
                .FirstOrDefaultAsync();

            if(articleLike == null)
            {
                ArticleLike newLike = new ArticleLike()
                {
                    ArticleId = articleId,
                    UserId = userId
                };
                await dbContext.ArticlesLikes.AddAsync(newLike);

            }
            else
            {
                dbContext.ArticlesLikes.Remove(articleLike);
            }

            return (articleLike);
        }

        public async Task<IEnumerable<ArticlesDto>> GetUserArticlesAsync(Guid userId)
        {
            var artciles = await dbContext
                .Articles
                .Where(a => a.UserId == userId)
                .OrderByDescending(a=>a.CreatedOn)
                .Select(a => new ArticlesDto()
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("d"),
                    Category = a.Category.Name,
                    Image = a.Image,
                    User = a.User.FirstName + ' ' + a.User.LastName,
                    Likes = a.ArticleLikes.Count()
                })
                .ToListAsync();

            return artciles;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
