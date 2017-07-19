using System;
using System.Threading.Tasks;
using HouseOfTheBook.Api.Infrastructure.HttpErrors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HouseOfTheBook.Api.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpErrorFactory httpErrorFactory;
        private readonly ILogger<HouseOfTheBook> logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            IHttpErrorFactory httpErrorFactory,
            ILogger<HouseOfTheBook> logger)
        {
            this.next = next;
            this.httpErrorFactory = httpErrorFactory;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.HResult, exception, exception.Message);

                var error = httpErrorFactory.CreateFrom(exception);

                await WriteResponseAsync(
                    context,
                    JsonConvert.SerializeObject(error),
                    "application/json",
                    error.Status);
            }
        }
        private Task WriteResponseAsync(
           HttpContext context,
           string content,
           string contentType,
           int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(content);
        }
    }
}
