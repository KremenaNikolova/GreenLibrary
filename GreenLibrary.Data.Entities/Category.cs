namespace GreenLibrary.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using static GreenLibrary.Common.ValidationConstants.CategoryConstants;

    public class Category
    {
        public Category()
        {
            IsDeleted = false;

            Articles = new List<Article>();
        }

        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
