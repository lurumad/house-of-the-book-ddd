using System;
using HouseOfTheBook.Common;

namespace HouseOfTheBook.Catalog.Domain.Model
{
    public class Title : ValueObject<Title>
    {
        private readonly string value;

        public Title(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("Title can not be null or empty");
            }
            this.value = value;
        }

        public static implicit operator string(Title title)
        {
            return title.value;
        }

        public static explicit operator Title(string value)
        {
            return new Title(value);
        }
    }
}
