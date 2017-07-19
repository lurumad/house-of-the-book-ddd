using Autofac;
using HouseOfTheBook.Catalog.Infrastructure;

namespace HouseOfTheBook.Api.Infrastructure.AutofacModules
{
    public class DataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CatalogContext>().AsSelf();
        }
    }
}
