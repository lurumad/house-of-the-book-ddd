using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Tests.Builders
{
    public class BookBuilder
    {
        private int authorId;

        public Book Build()
        {
            return new Book
            {
                Title = "Hamlet",
                Description = "Hamlet description",
                Isbn = "9788437610979",
                Pages = 600,
                AvailableStock = 10,
                Language = "en-GB",
                AuthorId = authorId
            };
        }

        public BookBuilder WithAuthorId(int authorId)
        {
            this.authorId = authorId;
            return this;
        }
    }
}