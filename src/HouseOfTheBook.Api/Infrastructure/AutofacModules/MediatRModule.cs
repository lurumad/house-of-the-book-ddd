using System.Collections.Generic;
using System.Reflection;
using Autofac;
using MediatR;
using HouseOfTheBook.Catalog.Application.Books;

namespace HouseOfTheBook.Api.Infrastructure.AutofacModules
{
    public class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Create).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register<SingleInstanceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.TryResolve(t, out object o) ? o : null;
            });

            builder.Register<MultiInstanceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });
        }
    }
}