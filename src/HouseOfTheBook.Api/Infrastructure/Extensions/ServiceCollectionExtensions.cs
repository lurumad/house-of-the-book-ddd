using HouseOfTheBook.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HouseOfTheBook.Api;
using HouseOfTheBook.Api.Infrastructure.HttpErrors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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

        public static IServiceProvider AddAutofac(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyModules(typeof(Startup).GetTypeInfo().Assembly);
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
