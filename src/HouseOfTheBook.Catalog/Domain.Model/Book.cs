using HouseOfTheBook.Common;

namespace HouseOfTheBook.Catalog.Domain.Model
{
    public class Book : AggregateRoot
    {
        private readonly BookId bookId;
        private readonly Title title;
        private readonly Isbn isbn;
        private readonly Author author;

        public Book(
            BookId bookId,
            Title title,
            Isbn isbn,
            Author author)
        {
            this.bookId = bookId;
            this.title = title;
            this.isbn = isbn;
            this.author = author;
        }
    }
}
