namespace GreenLibrary.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;
    
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Server.Dtos.Category;

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
    }
}
