using System.Threading.Tasks;
using HouseOfTheBook.Api.Tests.Builders;
using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Tests.Extensions
{
    public static class ContainerExtensions
    {
        public static async Task<Author> PersistAuthor(this ContainerFixture containerFixture)
        {
            var author = new AuthorBuilder().Build();
            await containerFixture.ExecuteDbContextAsync(async context =>
            {
                context.Auhtors.Add(author);
                await context.SaveChangesAsync();
            });
            return author;
        }
    }
}