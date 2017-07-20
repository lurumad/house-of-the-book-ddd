using System.Diagnostics;
using HouseOfTheBook.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace HouseOfTheBook.Catalog.Infrastructure
{
    public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<CatalogContext>()
                .Build();

            var builder = new DbContextOptionsBuilder();
            var data = new Data();
            configuration.GetSection(nameof(Data)).Bind(data);
            builder.UseSqlServer(data.ConnectionString);
            return new CatalogContext(builder.Options);
        }
    }
}
