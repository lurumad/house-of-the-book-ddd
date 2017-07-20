using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using HouseOfTheBook.Api.Tests.Builders;
using HouseOfTheBook.Api.Tests.Extensions;
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
            var author = await container.PersistAuthor();
            var request = new BookApiRequestBuilder()
                .WithAuthorId(author.Id)
                .WithAValidISBN()
                .Build(); 
            var response = await host.PostAsync(Api.Post.Books(), request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
