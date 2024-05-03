namespace GreenLibrary.Common
{
    using System.Security.Cryptography.X509Certificates;

    public static class ErrorMessages
    {
        public static class ArticleErrorMessages
        {
            public const string InvalidTitle = "Заглавието трябва да е между {2} и {1} символа.";

            public const string InvalidDescription = "Съдържанието на статията трябва да е между {2} и {1} символа.";

            public const string RequiredField = "Полето е задължително!";

        }

        public static class  UserErrorMessages
        {
            public const string InvalidUsernameOrPassword = "Невалидно потребителско име или парола.";

            public const string RequiredField = "Полето е задължително!";

            public const string InvalidFirstName = "Името трябва да съдържа между {2} и {1} символа.";

            public const string InvalidLastName = "Фамилията трябва да съдържа между {2} и {1} символа.";

            public const string InvalidUsername = "Потребителското име трябва да съдържа между {2} и {1} символа.";

            public const string UserNameAlreadyExist = "Това потребителско име вече е заето.";

            public const string InvalidPasswordLength = "Паролата трябва да е най-малко 6 символа";

            public const string InvalidPasswordPattern = "Паролата задължително трябва да съдържа главна буква, малка буква, символ и цифра.";

            public const string PasswordDoesntMatch = "Паролата в двете полета не съвпада.";

            public const string InvalidEmail = "Имейла трябва да бъде във формат example@test.com";

            public const string EmailAlreadyExist = "Вече съществува потребител с този Имейл.";
        }
    }
}
