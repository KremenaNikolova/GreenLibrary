namespace GreenLibrary.Services.Tests
{

    using Microsoft.EntityFrameworkCore;

    using static DatabaseSeeder;
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;
    using static GreenLibrary.Common.ApplicationConstants;

    public class CategoryServiceTests
    {
        private DbContextOptions<GreenLibraryDbContext> dbOptions;
        private GreenLibraryDbContext dbContext;
        private ICategoryService categoryService;

        string categoryName;
        int currentPage;
        int pageSize;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<GreenLibraryDbContext>()
                .UseInMemoryDatabase("GreenLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;

            dbContext = new GreenLibraryDbContext(dbOptions);
            dbContext.Database.EnsureCreated();

            categoryService = new CategoryService(dbContext);
            SeedDatabase(dbContext);

            categoryName = "Вредители";
            currentPage = 1;
            pageSize = 10;

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllArticlesByCategoryNameAsync_ShouldReturnArticleCollectionWithSameCategoryApprovedArticles()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Include(a => a.Category)
                .Where(a => a.Category.Name == categoryName
                && a.IsApproved == true)
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await categoryService.GetAllArticlesByCategoryNameAsync(categoryName, currentPage, pageSize, SortByDefault);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetAllArticlesByCategoryNameAsync_ShouldReturnArticleCollectionWithSameCategoryApprovedArticles2()
        {
            //arrange
            var articles = await dbContext
                .Articles
                .Include(a => a.Category)
                .Where(a => a.Category.Name == categoryName
                && a.IsApproved == true)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await categoryService.GetAllArticlesByCategoryNameAsync(categoryName, currentPage, pageSize, SortByDefault);

            //assert
            Assert.That(articles.Count(), Is.EqualTo(paginationMetadata.TotalItemCount));
        }

        [Test]
        public async Task GetAllCategoriesAsync_ShouldReturnCollectionWithAllCategories()
        {
            //arrange
            var categories = await dbContext
                .Categories
                .Where(c=>c.IsDeleted == false)
                .ToListAsync();

            //act
            var result = await categoryService.GetAllCategoriesAsync();

            //assert
            Assert.That(categories.Count(), Is.EqualTo(result.Count()));
        }
    }
}
