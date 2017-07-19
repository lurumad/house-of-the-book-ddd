using System;
using System.ComponentModel;

namespace HouseOfTheBook.Catalog.Model
{
    public class Isbn
    {
        private readonly string value;

        public Isbn(string value)
        {
            if (!IsValidIsbn10Digit(value) && !IsValidIsbn13Digit(value))
            {
                throw new FormatException("Invalid ISBN format");
            }
            this.value = value;
        }

        public static implicit operator string(Isbn isbn)
        {
            return isbn.value;
        }

        public static explicit operator Isbn(string value)
        {
            return new Isbn(value);
        }

        public static Isbn Parse(string isbn)
        {
            if (String.IsNullOrWhiteSpace(isbn))
            {
                throw new InvalidEnumArgumentException(nameof(isbn));
            }

            if (!TryParse(isbn, out Isbn result))
            {
                throw new FormatException("Invalid ISBN format");
            }

            return result;
        }

        public static bool TryParse(string isbn, out Isbn result)
        {
            if (String.IsNullOrWhiteSpace(isbn))
            {
                result = null;
                return false;
            }

            if (IsValidIsbn10Digit(isbn) || IsValidIsbn13Digit(isbn))
            {
                result = new Isbn(isbn);
                return true;
            }

            result = null;
            return false;
        }

        private static bool IsValidIsbn10Digit(string isbn)
        {
            isbn = isbn.Replace(@"/[^\dX]/gi", String.Empty);
            if (isbn.Length != 10)
            {
                return false;
            }
            var chars = isbn.ToUpper().ToCharArray();
            var sum = 0;
            for (var i = 0; i < chars.Length; i++)
            {
                var numericChar = chars[9] == 'X' ? 10 : chars[i];
                sum += (10 - i) * numericChar;
            }
            return sum % 11 == 0;
        }

        private static bool IsValidIsbn13Digit(string isbn)
        {
            isbn = isbn.Replace(@"/[^\dX]/gi", String.Empty);
            if (isbn.Length != 13)
            {
                return false;
            }
            var chars = isbn.ToCharArray();
            var sum = 0;
            for (var i = 0; i < chars.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += chars[i];
                }
                else
                {
                    sum += chars[i] * 3;
                }
            }
            return sum % 10 == 0;
        }
    }
}
