using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCustomJsonOptions()
                .AddDataAnnotations()
                .AddCustomMvcOptions(Env);

            return services.AddAutofac();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseApiErrorHandler()
                .UseMvc();
        }
    }
}
