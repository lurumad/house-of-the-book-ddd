using HouseOfTheBook.Catalog.Application.Books;
using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Tests.Builders
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