using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Tests
{
    public class AuthorBuilder
    {
        public Author Build()
        {
            return new Author
            {
                FirstName = "William",
                LastName = "Shakespeare"
            };
        }
    }
}
