using Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("test");
            builder.ConfigureServices((IServiceCollection services) =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                services.Remove(descriptor);

                var config = InitConfiguration();

                //services.AddDbContext<AppDbContext>(opt =>
                //{
                //    opt.UseNpgsql(config.GetConnectionString("Default"),
                //        o => o.MigrationsAssembly("Data.Migrations.Postgres"));
                //        //opt.EnableSensitiveDataLogging(); // Do not remove from comment - uncomment it for debuging.
                //    }
                //, ServiceLifetime.Transient);
                services.AddDbContext<AppDbContext>(opt => {
                    opt.UseSqlServer(config.GetConnectionString("MSSql"),
                            o => o.MigrationsAssembly("Data.Migrations.MsSql"));
                    //opt.EnableSensitiveDataLogging(); // Do not remove from comment - uncomment it for debuging.
                }
            , ServiceLifetime.Transient);

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    //uncomment this after a new migration has been run on dev
                    //db.Database.EnsureDeleted();
                    
                    db.Database.EnsureCreated();
                    //db.Database.Migrate();

                    try
                    {
                        //TestUtilities.InitializeDbForTests(db);
                        TestUtilities.Db.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }

}
