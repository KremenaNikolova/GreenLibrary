namespace GreenLibrary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GreenLibrary.Data.Entities;

    public class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
    {
        public void Configure(EntityTypeBuilder<ArticleLike> builder)
        {
            builder
                .HasKey(al => new { al.UserId, al.ArticleId });

            builder
                .HasOne(al => al.Article)
                .WithMany(a => a.ArticleLikes)
                .HasForeignKey(al => al.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(al => al.User)
                .WithMany(u => u.LikedArticles)
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
