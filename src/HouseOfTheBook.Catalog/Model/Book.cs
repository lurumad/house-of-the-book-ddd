namespace HouseOfTheBook.Catalog.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; }
        public int AvailableStock { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
