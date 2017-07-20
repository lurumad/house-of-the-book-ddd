using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HouseOfTheBook.Api.Infrastructure.Versioning
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiVersion = context.ApiDescription.GetApiVersion();

            if (apiVersion == null)
            {
                return;
            }

            var parameters = operation.Parameters;

            if (parameters == null)
            {
                operation.Parameters = parameters = new List<IParameter>();
            }

            var parameter = parameters.FirstOrDefault(p => p.Name == "api-version");
            if (parameter == null)
            {
                parameter = new NonBodyParameter()
                {
                    Name = "api-version",
                    Required = true,
                    Default = apiVersion.ToString(),
                    In = "query",
                    Type = "string"
                };
                parameters.Add(parameter);
            }
            else if (parameter is NonBodyParameter pathParameter)
            {
                pathParameter.Default = apiVersion.ToString();
            }

            parameter.Description = "The requested API version";
        }
    }
}
