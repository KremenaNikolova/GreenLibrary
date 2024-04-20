namespace GreenLibrary.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static GreenLibrary.Common.ValidationConstants.TagConstants;

    public class Tag
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public Article Article { get; set; } = null!;

        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
    }
}
