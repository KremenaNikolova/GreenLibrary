﻿namespace GreenLibrary.Common
{
    using System.Runtime.InteropServices;

    public static class SuccessfulMessage
    {
        public static class UserSuccessfulMessages
        {
            public const string SuccessfullDeleteUser = "Потребителят беше успешно изтрит.";

            public const string SuccessfullFollowUser = "Потребителят беше успешно добавен.";

            public const string SuccessfullUnFollowUser = "Потребителят беше успешно премахнат.";
        }

        public static class ArticleSuccesfulMessage
        {
            public const string SuccessfullEditedArticle = "Статията беше успешно променена.";

            public const string SuccessfullDeletedArticle = "Статията беше успешно изтрита";
        }
    }
}
