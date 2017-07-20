using System.Threading.Tasks;
using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Tests.Extensions
{
    public static class ContainerExtensions
    {
        public static async Task PersistAuthor(this ContainerFixture containerFixture, Author author)
        {
            await containerFixture.ExecuteDbContextAsync(async context =>
            {
                context.Auhtors.Add(author);
                await context.SaveChangesAsync();
            });
        }
    }
}