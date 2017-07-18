using System;
using FluentAssertions;
using Xunit;
using HouseOfTheBook.Catalog.Domain.Model;

namespace HouseOfTheBook.Catalog.Tests.Domain.Model
{
    public class isbn_should
    {
        [Fact]
        public void throws_format_exception_if_isbn_is_not_valid()
        {
            Action action = () => new Isbn("invalid_isbn");
            action.ShouldThrow<FormatException>();
        }

        [Fact]
        public void not_throws_format_exception_if_isbn_is_10_digit_valid()
        {
            Action action = () => new Isbn("818732919X");
            action.ShouldNotThrow<FormatException>();
        }

        [Fact]
        public void not_throws_format_exception_if_isbn_is_13_digit_valid()
        {
            Action action = () => new Isbn("9788490663196");
            action.ShouldNotThrow<FormatException>();
        }
    }
}
