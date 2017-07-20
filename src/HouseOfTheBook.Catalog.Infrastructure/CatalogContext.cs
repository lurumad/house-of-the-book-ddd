using HouseOfTheBook.Catalog.Model;
using Microsoft.EntityFrameworkCore;

namespace HouseOfTheBook.Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Auhtors { get; set; }

        public CatalogContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBook(modelBuilder);
            ConfigureAuthor(modelBuilder);
        }

        private static void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasKey(author => author.Id);
            modelBuilder.Entity<Author>()
                .Property(author => author.FirstName)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<Author>()
                .Property(author => author.LastName)
                .IsRequired()
                .HasMaxLength(250);
        }

        private static void ConfigureBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(book => book.Id);
            modelBuilder.Entity<Book>()
                .HasAlternateKey(book => book.Isbn);
            modelBuilder.Entity<Book>()
                .Property(book => book.Title)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Book>()
                .Property(book => book.Description)
                .IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<Book>()
                .Property(book => book.Isbn)
                .IsRequired()
                .HasMaxLength(13);
        }
    }
}
