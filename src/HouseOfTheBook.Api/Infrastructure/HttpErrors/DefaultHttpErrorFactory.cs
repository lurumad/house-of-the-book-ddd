using HouseOfTheBook.Common;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HouseOfTheBook.Api.Infrastructure.HttpErrors
{
    public class DefaultHttpErrorFactory : IHttpErrorFactory
    {
        private readonly IHostingEnvironment env;
        private readonly IDictionary<Type, Func<Exception, HttpError>> factory;

        public DefaultHttpErrorFactory(IHostingEnvironment env)
        {
            this.env = env;

            factory = new Dictionary<Type, Func<Exception, HttpError>>
            {
                { typeof(Exception), InternalServerError }
            };
        }

        public HttpError CreateFrom(Exception exception)
        {
            if (factory.TryGetValue(exception.GetType(), out Func<Exception, HttpError> func))
            {
                return func(exception);
            }

            return factory[typeof(Exception)](exception);
        }

        private HttpError BadRequest(Exception exception)
        {
            var domainException = (DomainException)exception;

            return HttpError.Create(
                env,
                status: HttpStatusCode.BadRequest,
                code: string.Empty,
                userMessage: domainException.ErrorMessages.ToArray(),
                developerMessage: null);
        }

        private HttpError NotFound(Exception exception)
        {
            var notFoundException = (EntityNotFoundException)exception;

            return HttpError.Create(
                env,
                status: HttpStatusCode.NotFound,
                code: null,
                userMessage: new[] { notFoundException.Message },
                developerMessage: null);
        }

        private HttpError InternalServerError(Exception exception)
        {
            return HttpError.Create(
                env,
                status: HttpStatusCode.InternalServerError,
                code: string.Empty,
                userMessage: new[] { "500" },
                developerMessage: exception.Message);
        }
    }
}
