using System;

namespace HouseOfTheBook.Api.Infrastructure.HttpErrors
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFrom(Exception exception);
    }
}
