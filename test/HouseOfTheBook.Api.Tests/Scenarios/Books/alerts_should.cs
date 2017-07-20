using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using HouseOfTheBook.Catalog.Model;
using Xunit;

namespace HouseOfTheBook.Api.Tests.Scenarios.Books
{
    public class catalog_api_should : IClassFixture<ContainerFixture>, IClassFixture<HostFixture>
    {
        private readonly HostFixture host;
        private readonly ContainerFixture container;

        public catalog_api_should(HostFixture host, ContainerFixture container)
        {
            this.host = host;
            this.container = container;
        }

        [Fact]
        [ResetDatabase]
        public async Task allows_to_add_a_new_book()
        {
            var author = new Author()
            {
                FirstName = "William",
                LastName = "Shakespeare"
            };
            await container.ExecuteDbContextAsync(async context =>
            {
                context.Auhtors.Add(author);
                await context.SaveChangesAsync();
            });
            var book = new Book
            {
                Title = "Hamlet",
                Description = "Lorem ipsum...",
                Pages = 600,
                Isbn = "9788437610979",
                AuthorId = author.Id,
                AvailableStock = 10,
                Language = "en-GB"
            };
            var result = await host.PostAsync(Api.Post.Books(), book);
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
