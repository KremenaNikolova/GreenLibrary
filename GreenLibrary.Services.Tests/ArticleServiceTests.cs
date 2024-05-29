namespace GreenLibrary.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;

    using static DatabaseSeeder;
    using GreenLibrary.Server.Dtos.Article;
    using GreenLibrary.Data.Entities;
    using static GreenLibrary.Common.ApplicationConstants;
    using GreenLibrary.Services.Dtos.Article;

    public class ArticleServiceTests
    {
        private DbContextOptions<GreenLibraryDbContext> dbOptions;
        private GreenLibraryDbContext dbContext;
        private IArticleService articleService;

        int currentPage;
        int pageSize;
        string sortBy;
        Guid articleId;
        Guid userId;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<GreenLibraryDbContext>()
                .UseInMemoryDatabase("GreenLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;

            dbContext = new GreenLibraryDbContext(dbOptions);
            dbContext.Database.EnsureCreated();

            articleService = new ArticleService(dbContext);
            SeedDatabase(dbContext);

            currentPage = 1;
            pageSize = 5;
            sortBy = "createon-newest";
            articleId = Guid.Parse("57c07e68-da13-4690-977d-9953c2c8ca7b");
            userId = Guid.Parse("850e61e1-42d2-4632-aa00-0ccdf6c9784c");
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllApprovedArticlesAsync_ShouldReturnListOfApprovedArticles()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Where(a => a.IsApproved == true)
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await articleService.GetAllApprovedArticlesAsync(currentPage, pageSize, sortBy);

            //assert
            Assert.That(articles, Has.Count.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetArticleByIdAsync_ShouldRetrunOneArticleWithChoosenId()
        {
            //arange
            var article = await dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.User)
                .Where(a => a.Id == articleId)
                .FirstAsync();

            //act
            var result = await articleService.GetArticleByIdAsync(articleId);

            //assert
            Assert.That(article.Id.ToString(), Is.EqualTo(result.Id));
            Assert.That(article.Title, Is.EqualTo(result.Title));
            Assert.That(article.Description, Is.EqualTo(result.Description));
            Assert.That(article.Category.Name, Is.EqualTo(result.Category));
            Assert.That(article.Image, Is.EqualTo(result.Image));
            Assert.That(article.CreatedOn.ToString("d"), Is.EqualTo(result.CreatedOn));
            Assert.That($"{article.User.FirstName} {article.User.LastName}", Is.EqualTo(result.User));
            Assert.That(article.UserId, Is.EqualTo(result.UserId));
            Assert.That(article.ArticleLikes.Count(), Is.EqualTo(result.Likes));
            Assert.That(article.Tags.Count(), Is.EqualTo(result.Tags.Count()));
            Assert.That(article.IsApproved, Is.EqualTo(result.IsApproved));
        }

        [Test]
        public async Task CreateArticleFromDto_ShouldReturnNewArticleWithArticleDtoInformation()
        {
            //arrange 
            var newArticle = new CreateArticleDto()
            {
                Title = "Title 4",
                Description = "viverra maecenas accumsan lacus vel facilisis volutpat est velit egestas dui id ornare arcu odio ut sem nulla pharetra diam sit amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor elit sed vulputate mi sit amet mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada proin libero nunc consequat interdum varius sit amet mattis vulputate enim nulla aliquet porttitor lacus luctus accumsan tortor posuere ac ut consequat semper viverra nam libero justo laoreet sit amet cursus sit amet dictum sit amet justo donec enim diam vulputate ut pharetra sit amet aliquam id diam maecenas ultricies mi eget mauris pharetra et ultrices neque ornare aenean euismod elementum nisi quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis rhoncus urna neque viverra justo nec ultrices dui sapien eget mi proin sed libero enim sed faucibus turpis in eu mi bibendum neque egestas congue quisque egestas diam in arcu cursus euismod quis viverra nibh cras pulvinar mattis nunc sed blandit libero volutpat sed cras ornare arcu dui vivamus arcu felis bibendum ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum fusce ut placerat orci nulla pellentesque dignissim enim sit amet venenatis urna cursus eget nunc scelerisque",
                ImageName = "TestImageName.jpg",
                CategoryId = 3
            };

            //act
            var result = await articleService.CreateArticleFromDto(newArticle, userId);

            //assert
            Assert.That(userId, Is.EqualTo(result.UserId));
            Assert.That(newArticle.Title, Is.EqualTo(result.Title));
            Assert.That(newArticle.Description, Is.EqualTo(result.Description));
            Assert.That(newArticle.CategoryId, Is.EqualTo(result.CategoryId));
            Assert.That(newArticle.ImageName, Is.EqualTo(result.Image));
            Assert.That(newArticle.Tags.Count(), Is.EqualTo(result.Tags.Count()));
            Assert.That((result.IsApproved), Is.EqualTo(false));
        }

        [Test]
        public async Task SearchedArticlesAsync_ShouldReturnResultsConnectedWithQueryString()
        {
            //arrange
            string searchQuery = "Kremena";

            var articles = dbContext
               .Articles
               .Where(a => a.User.IsDeleted == false
               && a.IsApproved == true
               && (a.Title.Contains(searchQuery)
               || a.Description.Contains(searchQuery)
               || a.User.FirstName.Contains(searchQuery)
               || a.User.LastName.Contains(searchQuery)
               || a.Image.Contains(searchQuery)
               || a.Tags.Any(t => t.Name.Contains(searchQuery))))
               .Take(pageSize)
               .AsQueryable();

            //act
            var (result, paginationMetadata) = await articleService.SearchedArticlesAsync(searchQuery, currentPage, pageSize);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task AddLikeAsync_ShouldReturnNullIfNotExistAlready()
        {
            //arrange
            var articleLikes = await dbContext
                .ArticlesLikes
                .Where(al => al.ArticleId == articleId
                && al.UserId == userId)
                .FirstOrDefaultAsync();

            //act
            var result = await articleService.AddLikeAsync(articleId, userId);

            //assert
            Assert.That(articleLikes, Is.EqualTo(result));
        }

        [Test]
        public async Task AddLikeAsync_ShouldReturnArticleLikeIfAlreadyExist()
        {
            //arrange
            var newArtileLikes = new ArticleLike()
            {
                ArticleId = articleId,
                UserId = userId,
            };

            await dbContext.ArticlesLikes.AddAsync(newArtileLikes);
            await articleService.SaveAsync();

            var articleLikes = await dbContext
                .ArticlesLikes
                .Where(al => al.ArticleId == articleId
                && al.UserId == userId)
                .FirstOrDefaultAsync();

            //act
            var result = await articleService.AddLikeAsync(articleId, userId);

            //assert
            Assert.That(articleLikes, Is.EqualTo(result));
        }

        [Test]
        public async Task GetUserArticlesAsync_ShouldReturnOnlyOneUserApprovedArticles()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Where(a => a.UserId.ToString() == UserId
                && a.IsApproved == true)
                .Take(pageSize)
                .ToListAsync();

            //act 
            var (result, pagnationMetadata) = await articleService.GetUserArticlesAsync(Guid.Parse(UserId), currentPage, pageSize);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(result.Count()));
            foreach (var article in result)
            {
                Assert.That(article.UserId, Is.EqualTo(Guid.Parse(UserId)));
            }
        }

        [Test]
        public async Task GetMyArticlesAsync_ShouldReturnAllUsersArticlesEvenNotApproved()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Where(a => a.UserId.ToString() == UserId)
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, pagnationMetadata) = await articleService.GetMyArticlesAsync(Guid.Parse(UserId), currentPage, pageSize);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(result.Count()));
            foreach (var article in result)
            {
                Assert.That(article.UserId, Is.EqualTo(Guid.Parse(UserId)));
            }
        }

        [Test]
        public async Task GetUserArticleByArticleIdAsync_ShouldReturnOnlyOneArticle()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Include(a => a.Category)
                .Where(a => a.Id == articleId)
                .ToListAsync();

            var article = articles.First();

            //act
            var result = await articleService.GetUserArticleByArticleIdAsync(articleId);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(1));
            Assert.That(article.Title, Is.EqualTo(result.Title));
            Assert.That(article.Description, Is.EqualTo(result.Description));
            Assert.That(article.Category.Name, Is.EqualTo(result.Category));
            Assert.That(article.Image, Is.EqualTo(result.Image));
            Assert.That(article.Tags.Count(), Is.EqualTo(result.Tags.Count()));
            Assert.That(article.IsApproved, Is.EqualTo(result.IsApproved));

        }

        [Test]
        public async Task EditArticleAsync_ShouldChangeTheArticle()
        {
            //arrange
            var article = await dbContext
                .Articles
                .Where(a => a.Id == articleId)
                .FirstAsync();

            var oldDescription = article.Description;
            var newDescription = "libero justo laoreet sit amet cursus sit amet dictum sit amet justo donec enim diam vulputate ut pharetra sit amet aliquam id diam maecenas ultricies mi eget mauris pharetra et ultrices neque ornare aenean euismod elementum nisi quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis rhoncus urna neque viverra justo nec ultrices dui sapien eget mi proin sed libero enim sed faucibus turpis in eu mi bibendum neque egestas congue quisque egestas diam in arcu cursus euismod quis viverra nibh cras pulvinar mattis nunc sed blandit libero volutpat sed cras ornare arcu dui vivamus arcu felis bibendum ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum fusce ut placerat orci nulla pellentesque dignissim enim sit amet venenatis urna cursus eget nunc scelerisque viverra mauris in aliquam sem fringilla ut morbi tincidunt augue interdum velit euismod in pellentesque massa placerat duis ultricies lacus sed turpis tincidunt id aliquet risus feugiat in ante metus dictum at tempor commodo ullamcorper a lacus vestibulum sed arcu non odio euismod lacinia at quis risus sed vulputate odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam erat velit scelerisque in dictum non consectetur a erat nam at lectus urna duis convallis convallis";

            var articleDto = new CreateArticleDto()
            {
                Title = article.Title,
                Description = newDescription,
                CategoryId = article.CategoryId,
                ImageName = article.Image,
                IsApproved = false
            };

            //act
            await articleService.EditArticleAsync(articleId, articleDto);
            await articleService.SaveAsync();

            //assert
            Assert.That(article.Description, Is.Not.EqualTo(oldDescription));
            Assert.That(article.Description, Is.EqualTo(newDescription));
            Assert.That(article.IsApproved, Is.False);
        }

        [Test]
        public async Task EditArticleAsync_ShouldAddTagsToArticleIfNotExist()
        {
            //arrange
            var article = await dbContext
                .Articles
                .Include(a=>a.Tags)
                .Where(a => a.Id == articleId)
                .FirstAsync();

            var tagName = "Test";
            var isTagExist = article.Tags.Any(t => t.Name == tagName);
            
            var articleDto = new CreateArticleDto()
            {
                Title = article.Title,
                Description = article.Description,
                CategoryId = article.CategoryId,
                ImageName = article.Image,
                IsApproved = false
            };
            var tag = new Tag()
            {
                Name = tagName,
                ArticleId = articleId
            };

            articleDto.Tags.Add(tagName);

            //act
            await articleService.EditArticleAsync(articleId, articleDto);
            await articleService.SaveAsync();

            var isTagExistAfterEdit = article.Tags.Any(t => t.Name == tagName);

            //assert
            Assert.That(isTagExist, Is.False);
            Assert.That(isTagExistAfterEdit, Is.True);
            Assert.That(isTagExist, Is.Not.EqualTo(isTagExistAfterEdit));
        }

        [Test]
        public async Task EditArticleAsync_ShouldNotAddTagsToArticleIfAlreadyExist()
        {
            //arrange
            var tagName = "Test";
            var tag = new Tag()
            {
                Name = tagName,
                ArticleId = articleId
            };
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            var article = await dbContext
                .Articles
                .Include(a => a.Tags)
                .Where(a => a.Id == articleId)
                .FirstAsync();

            var articleTagsCount = article.Tags.Count();

            var articleDto = new CreateArticleDto()
            {
                Title = article.Title,
                Description = article.Description,
                CategoryId = article.CategoryId,
                ImageName = article.Image,
                IsApproved = false
            };
            articleDto.Tags.Add(tagName);

            //act
            await articleService.EditArticleAsync(articleId, articleDto);
            await articleService.SaveAsync();

            var articleAfterEdit = await dbContext
                .Articles
                .Include(a => a.Tags)
                .Where(a => a.Id == articleId)
                .FirstAsync();

            var result = article.Tags.Count();

            //assert
            Assert.That(result, Is.EqualTo(articleTagsCount));
        }

        [Test]
        public async Task DeleteArticle_ShouldRemoveArticleFromDatabase()
        {
            //arrange
            var article = await dbContext
                .Articles
                .Where(a => a.Id == articleId)
                .FirstOrDefaultAsync();

            //act
            await articleService.DeleteArticle(articleId);

            var articleAfterDelete = await dbContext
                .Articles
                .Where(a => a.Id == articleId
                && a.UserId == userId)
                .FirstOrDefaultAsync();

            //assert
            Assert.That(article, Is.Not.Null);
            Assert.That(articleAfterDelete, Is.Null);
        }

        [Test]
        public async Task GetAllArticlesAsync_ShouldReturnEvenNotApprovedArticles()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await articleService.GetAllArticlesAsync(currentPage, pageSize, SortByDefault);

            var totalResultCount = paginationMetadata.TotalItemCount;

            //assert
            Assert.That(articles.Count(), Is.EqualTo(totalResultCount));
            Assert.That(result.Count(), Is.EqualTo(pageSize));
        }

        [Test]
        public async Task ApproveArticle_ShouldSetIsApproveToTrue()
        {
            //arrange
            var article = await dbContext
                .Articles
                .Where(a=>a.Id == articleId)
                .FirstAsync();

            article.IsApproved = false;
            await dbContext.SaveChangesAsync();

            //act
            await articleService.ApproveArticle(article.Id);

            //assert
            Assert.That(article.IsApproved, Is.True);
        }


    }
}
