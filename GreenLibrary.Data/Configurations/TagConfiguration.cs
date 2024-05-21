namespace GreenLibrary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using GreenLibrary.Data.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasOne(t => t.Article)
                .WithMany(a => a.Tags)
                .HasForeignKey(t => t.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
