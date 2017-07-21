using System;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HouseOfTheBook.Common.Options;
using Microsoft.Extensions.Logging;

namespace HouseOfTheBook.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Env { get; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddIf(env.IsDevelopment(), x => x.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .AddEnvironmentVariables()
                .Build();
            Env = env;

            loggerFactory
                .AddIf(env.IsDevelopment(), x => x.AddConsole().AddDebug());
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
                .AddResponseCompression()
                .AddAutoMapperClasses(new []{ typeof(Startup).GetTypeInfo().Assembly })
                .AddCustomizeEf(Configuration.GetSection<Data>().ConnectionString)
                .AddCustomAutoMapper();
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
