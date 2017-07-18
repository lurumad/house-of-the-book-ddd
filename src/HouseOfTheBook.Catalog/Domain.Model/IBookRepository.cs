using System.Threading.Tasks;

namespace HouseOfTheBook.Catalog.Domain.Model
{
    public interface IBookRepository
    {
        Task<Book> Get(BookId bookId);
    }
}
