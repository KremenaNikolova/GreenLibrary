namespace GreenLibrary.Server.Dtos.Article
{
    using System.ComponentModel.DataAnnotations;

    using static GreenLibrary.Common.ValidationConstants.ArticleConstants;
    using static GreenLibrary.Common.ErrorMessages.ArticleErrorMessages;

    public class CreateArticleDto
    {
        public CreateArticleDto()
        {
            Tags = new List<string>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLenth, ErrorMessage = InvalidTitle)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = InvalidDescription)]
        [Display(Name = "Съдържание")]
        public string Description { get; set; } = null!;

        [Display(Name = "Снимка")]
        public string ImagePath { get; set; } = null!;

        public int CategoryId { get; set; }

        public ICollection<string> Tags { get; set; }

    }
}
