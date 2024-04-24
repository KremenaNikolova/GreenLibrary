namespace GreenLibrary.Server.Dtos.Article
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static GreenLibrary.Common.ValidationConstants.ArticleConstants;
    using static GreenLibrary.Common.ErrorMessages.ArticleErrorMessages;
    using GreenLibrary.Server.Dtos.Category;

    public class CreateArticleDto
    {
        public CreateArticleDto()
        {
            Categories = new List<CategoryDto>();
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

        public int CateogoryId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = "Категории")]
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
