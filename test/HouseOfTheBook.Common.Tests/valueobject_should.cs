using FluentAssertions;
using HouseOfTheBook.Common.Domain.Model;
using Xunit;

namespace HouseOfTheBook.Common.Tests
{
    public class valueobject_should
    {
        [Fact]
        public void be_equal_to_other_valueobject_with_the_same_values()
        {
            var title = new ValueObjectTest("As You Like It");
            var anotherTitle = new ValueObjectTest("As You Like It");
            title.Should().Be(anotherTitle);
        }

        [Fact]
        public void not_be_equal_to_other_valueobject_with_diferent_values()
        {
            var title = new ValueObjectTest("As You Like It");
            var anotherTitle = new ValueObjectTest("Troilus and Cressida");
            title.Should().NotBe(anotherTitle);
        }

        private class ValueObjectTest : ValueObject<ValueObjectTest>
        {
            private readonly string value;

            public ValueObjectTest(string value)
            {
                this.value = value;
            }
        }
    }
}
