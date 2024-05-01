namespace GreenLibrary.Server.Dtos.Article
{
    using System.ComponentModel.DataAnnotations;

    using static GreenLibrary.Common.ValidationConstants.ArticleConstants;
    using static GreenLibrary.Common.ErrorMessages.ArticleErrorMessages;
    using Microsoft.AspNetCore.Http;

    public class CreateArticleDto
    {
        public CreateArticleDto()
        {
            Tags = new List<string>();
        }

        [Required(ErrorMessage = RequiredField)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLenth, ErrorMessage = InvalidTitle)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = InvalidDescription)]
        [Display(Name = "Съдържание")]
        public string Description { get; set; } = null!;

        [Display(Name = "Снимка")]
        public string? ImageName { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public int CategoryId { get; set; }

        public ICollection<string> Tags { get; set; }

    }
}
