namespace GreenLibrary.Services.Dtos.Article
{

    public class ArticlesDto
    {
        public ArticlesDto()
        {
            Tags = new List<string>();
        }

        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string User { get; set; } = null!;

        public Guid UserId { get; set; }

        public int Likes { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
