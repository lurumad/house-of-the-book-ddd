using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace HouseOfTheBook.Api
{
    public class Startup
    {
        public IHostingEnvironment Env { get; }

        public Startup(IHostingEnvironment env)
        {
            Env = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiErrorHandler()
                .AddMvcCore()
                .AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV")
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddCustomJsonOptions()
                .AddVersionedApiExplorer()
                .AddCustomMvcOptions(Env)
                .Services
                .AddCustomVersioning()
                .AddSwagger()
                .AddCors()
                .AddResponseCompression();

            return services.AddAutofac();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            app
                .UseApiErrorHandler()
                .UseResponseCompression()
                .UseCors("AllowAny")
                .UseApiErrorHandler()
                .UseSwagger()
                .UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    })
                .UseMvc();
        }
    }
}
