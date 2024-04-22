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
        }

        public static class CategoryConstants
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
        }

        public static class ArticleConstants
        {
            public const int TitleMinLenth = 10;
            public const int TitleMaxLength = 60;

            public const int DescriptionMinLength = 500;
            public const int DescriptionMaxLength = 10000;
        }

        public static class TagConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
        }
    }
}
