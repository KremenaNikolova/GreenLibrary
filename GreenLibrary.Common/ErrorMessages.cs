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


            public const string NotFountArticles = "Не са намерени статии, които да съвпадат с критериите за търсене.";

        }

        public static class  UserErrorMessages
        {
            public const string InvalidUsernameOrPassword = "Невалидно потребителско име или парола.";

            public const string RequiredField = "Полето е задължително!";

            public const string InvalidFirstName = "Името трябва да съдържа между {2} и {1} символа.";

            public const string InvalidLastName = "Фамилията трябва да съдържа между {2} и {1} символа.";

            public const string InvalidUsername = "Потребителското име трябва да съдържа между {2} и {1} символа.";

            public const string UserNameAlreadyExist = "Това потребителско име вече е заето.";

            public const string InvalidPasswordLength = "Паролата трябва да е най-малко 6 символа.";

            public const string InvalidPasswordPattern = "Паролата задължително трябва да съдържа главна буква, малка буква, символ и цифра.";

            public const string PasswordDoesntMatch = "Паролата в двете полета не съвпада.";

            public const string EmptyNewPasswordField = "Моля, въведете нова парола.";

            public const string EmptyRepeatNewPasswordField = "Моля, повторете новата парола.";

            public const string InvalidPassword = "Не сте въвели правилно старата си парола.";

            public const string InvalidEmail = "Имейла трябва да бъде във формат example@test.com.";

            public const string EmailAlreadyExist = "Вече съществува потребител с този Имейл.";

            public const string NotFountUser = "Не е намерен потребител с това ID";

            public const string NotFoundFollowings = "Все още не сте последвали никого.";

            public const string InvalidFollowUserOperation = "Нещо се случи по време на операцията. Потребителят не е добавен към колекцията.";
        }

        public static class EmailErrorMessages
        {
            public const string WrongEmailAddress = "Въвели сте грешен имейл адрес.";

            public const string BadRequestDefaultMessage = "Възникна грешка при опит да се нулира вашата парола. Моля опитайте отново!";
        }
    }
}
