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
        }
    }
}
