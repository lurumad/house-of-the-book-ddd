using System.ComponentModel.DataAnnotations;
using HouseOfTheBook.Catalog.Application.Books;
using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Catalog.Application.Attributes
{
    public class IsbnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var request = (Add.Request) validationContext.ObjectInstance;

            if (!Isbn.TryParse(request.Isbn, out Isbn isbn))
            {
                return new ValidationResult("Invalid ISBN", new []{ nameof(Isbn) });
            }

            return ValidationResult.Success;
        }
    }
}
