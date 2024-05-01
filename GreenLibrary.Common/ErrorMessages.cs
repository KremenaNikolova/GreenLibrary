namespace GreenLibrary.Common
{
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
        }
    }
}
