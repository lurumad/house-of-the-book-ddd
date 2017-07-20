using System;

namespace HouseOfTheBook.Common.Exceptions
{
    /// <summary>
    /// Exception base.
    /// </summary>
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {
            
        }
    }
}