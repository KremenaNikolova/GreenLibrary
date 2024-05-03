namespace GreenLibrary.Common
{
    public class ValidationConstants
    {
        public static class UserConstants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 50;

            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 20;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 50;
            public const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$";

            public const int EmailMaxLength = 70;
            public const string EmailPattern = "^((?!\\.)[\\w-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$";
        }

        public static class CategoryConstants
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
        }

        public static class ArticleConstants
        {
            public const int TitleMinLenth = 3;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 200;
            public const int DescriptionMaxLength = 10000;
        }

        public static class TagConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
        }

    }
}
