namespace HouseOfTheBook.Api.Tests
{
    internal static class Api
    {
        public static string CatalogBaseUrl = "api/catalog/books";

        internal static class Get
        {
            public static string Books()
            {
                return CatalogBaseUrl;
            }

            public static string BookBy(int id)
            {
                return $"{CatalogBaseUrl}/{id}";
            }
        }

        internal static class Post
        {
            public static string Books()
            {
                return CatalogBaseUrl;
            }
        }
    }
}
