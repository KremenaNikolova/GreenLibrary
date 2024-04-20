namespace GreenLibrary.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GreenLibrary.Common.ValidationConstants.ArticleConstants;
    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid();

            CreatedOn = DateTime.UtcNow;

            ArticleLikes = new List<ArticleLike>();
            Tags = new List<Tag>();
        }

        public Guid Id { get; set; }

        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string Image { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public User User { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public IEnumerable<ArticleLike> ArticleLikes { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
