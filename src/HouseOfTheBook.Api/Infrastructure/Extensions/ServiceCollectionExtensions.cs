using HouseOfTheBook.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HouseOfTheBook.Api;
using HouseOfTheBook.Api.Infrastructure.HttpErrors;
using HouseOfTheBook.Catalog.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIf(
            this IServiceCollection services,
            bool condition,
            Func<IServiceCollection,
                IServiceCollection> action)
        {
            return condition ? action(services) : services;
        }

        public static IServiceCollection AddApiErrorHandler(this IServiceCollection services)
        {
            services.AddSingleton<IHttpErrorFactory, DefaultHttpErrorFactory>();
            return services;
        }

        public static IMvcCoreBuilder AddCustomMvcOptions(
            this IMvcCoreBuilder builder,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    if (hostingEnvironment.IsDevelopment())
                    {
                        options.Filters.Add(new FormatFilterAttribute());
                    }

                    options.Filters.Add(new ValidateModelStateAttribute());
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    options.ReturnHttpNotAcceptable = true;
                });

        public static IMvcCoreBuilder AddCustomJsonOptions(this IMvcCoreBuilder builder) =>
            builder.AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
                });

        public static IServiceCollection AddCustomVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

        public static IServiceProvider AddAutofac(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyModules(typeof(Startup).GetTypeInfo().Assembly);
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider()
                        .GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(
                            description.GroupName,
                            new Info
                            {
                                Title = $"Sample API {description.ApiVersion}",
                                Version = description.ApiVersion.ToString()
                            });
                    }
                });

        public static IServiceCollection AddAutoMapperClasses(this IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();
            
            var types = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles = types
                .Where(t => typeof(Profile).IsAssignableFrom(t.GetTypeInfo()))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return services;
        }

        public static IServiceCollection AddCustomizeEf(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services) =>
            services
                .AddAutoMapper()
                .AddSingleton(Mapper.Configuration)
                .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
    }
}
