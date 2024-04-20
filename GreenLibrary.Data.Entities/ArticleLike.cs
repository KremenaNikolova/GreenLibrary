namespace GreenLibrary.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ArticleLike
    {
        public Article Article { get; set; } = null!;

        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }

        public User User { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
