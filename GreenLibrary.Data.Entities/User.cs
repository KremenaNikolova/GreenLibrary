﻿namespace GreenLibrary.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static GreenLibrary.Common.ValidationConstants.UserConstants;

    public class User : IdentityUser<Guid>
    {
        public User()
        {
            IsModerator = false;
            IsDeleted = false;

            CreatedOn = DateTime.UtcNow;

            Followers = new List<User>();
            Following = new List<User>();
            Articles = new List<Article>();
            LikedArticles = new List<ArticleLike>();
        }

        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public string? Image { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsModerator { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<User> Followers { get; set; }

        public IEnumerable<User> Following { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public IEnumerable<ArticleLike> LikedArticles { get; set; }
    }
}
