namespace GreenLibrary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GreenLibrary.Data.Entities;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasMany(c => c.Articles)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .HasData(GenerateCategories());
        }

        private static Category[] GenerateCategories()
        {
            var categories = new List<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Зеленчукопроизводство"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Растениевъдство"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Вредители"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
