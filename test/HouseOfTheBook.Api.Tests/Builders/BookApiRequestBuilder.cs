using HouseOfTheBook.Catalog.Application.Books;

namespace HouseOfTheBook.Api.Tests.Builders
{
    public class BookApiRequestBuilder
    {
        private int authorId;
        private string isbn;

        public Add.Request Build()
        {
            return new Add.Request
            {
                Title = "Hamlet",
                Description = "Lorem ipsum...",
                Pages = 600,
                Isbn = isbn,
                AuthorId = authorId,
                AvailableStock = 10
            };
        }

        public BookApiRequestBuilder WithAuthorId(int authorId)
        {
            this.authorId = authorId;
            return this;
        }

        public BookApiRequestBuilder WithAValidISBN()
        {
            isbn = "9788437610979";
            return this;
        }
        public BookApiRequestBuilder WithAnInvalidISBN()
        {
            isbn = "97884";
            return this;
        }
    }
}
