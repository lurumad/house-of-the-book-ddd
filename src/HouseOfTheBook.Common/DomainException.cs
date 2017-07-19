using System.Collections.Generic;

namespace HouseOfTheBook.Common
{
    /// <summary>
    /// Specific exception for domain.
    /// </summary>
    public class DomainException : BaseException
    {
        public IList<string> ErrorMessages { get; private set; }

        public DomainException(string errorMessage) : base(errorMessage)
        {
            ErrorMessages = new List<string> {errorMessage};
        }

        public DomainException(IList<string> errorMessages)
            : base(string.Join("\n", errorMessages))
        {
            ErrorMessages = new List<string>(errorMessages);
        }
    }
}