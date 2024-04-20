namespace GreenLibrary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GreenLibrary.Data.Entities;

    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasMany(a => a.ArticleLikes)
                .WithOne(al => al.Article)
                .HasForeignKey(al => al.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(a => a.Tags)
                .WithOne(t => t.Article)
                .HasForeignKey(t => t.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
