using System;

namespace HouseOfTheBook.Common
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