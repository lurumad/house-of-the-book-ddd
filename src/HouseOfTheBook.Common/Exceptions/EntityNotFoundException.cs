namespace HouseOfTheBook.Common.Exceptions
{
    /// <summary>
    /// Specific exception when not found an entity or resource
    /// </summary>
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}