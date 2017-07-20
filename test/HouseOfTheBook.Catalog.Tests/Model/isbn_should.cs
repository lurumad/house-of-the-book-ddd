using System;
using FluentAssertions;
using HouseOfTheBook.Catalog.Model;
using Xunit;
using HouseOfTheBook.Common.Exceptions;

namespace HouseOfTheBook.Catalog.Tests.Model
{
    public class isbn_should
    {
        private const string ValidISBN = "9788437610979";
        private const string InvalidISBN = "978";

        [Fact]
        public void parse_a_valid_isbn()
        {
            var isbn = Isbn.Parse(ValidISBN);
            isbn.Should().NotBeNull();
        }

        [Fact]
        public void throws_a_domain_exception_when_isbn_is_not_valid()
        {
            Action action = () => Isbn.Parse(InvalidISBN);
            action.ShouldThrow<DomainException>();
        }
    }
}
