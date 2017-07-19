using HouseOfTheBook.Catalog.Model;
using Microsoft.EntityFrameworkCore;

namespace HouseOfTheBook.Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBook(modelBuilder);
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
