using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Respawn;
using HouseOfTheBook.Catalog.Infrastructure;
using HouseOfTheBook.Common.Options;

namespace HouseOfTheBook.Api.Tests
{
    public class ContainerFixture
    {
        private static readonly Checkpoint Checkpoint;
        private static readonly IConfigurationRoot Configuration;
        private static readonly IServiceScopeFactory ScopeFactory;

        static ContainerFixture()
        {
            var env = Substitute.For<IHostingEnvironment>();
            env.EnvironmentName.Returns(EnvironmentName.Development);
            env.ContentRootPath.Returns(AppContext.BaseDirectory);
            var startup = new Startup(env, new LoggerFactory());
            Configuration = startup.Configuration;
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var rootContainer = services.BuildServiceProvider();
            ScopeFactory = rootContainer.GetService<IServiceScopeFactory>();
            Checkpoint = new Checkpoint();
        }

        public static void ResetCheckpoint()
        {
            var data = new Data();
            Configuration.GetSection(nameof(Data)).Bind(data);
            Checkpoint.Reset(data.ConnectionString).Wait();
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            IDbContextTransaction transaction = null;

            using (var scope = ScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CatalogContext>();
                try
                {
                    transaction = dbContext.Database.BeginTransaction();

                    await action(scope.ServiceProvider);
                }
                catch (Exception)
                {
                    transaction?.Rollback();
                    throw;
                }
                transaction?.Commit();
            }
        }

        public Task ExecuteDbContextAsync(Func<CatalogContext, Task> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<CatalogContext>()));
        }
    }
}
