using System;
using System.Collections.Generic;
using System.Text;
using DatingAPI.DAT;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingTests.Infrastructure
{
    public class DefaultWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<DatingContext>(options =>
                {   

                    options.UseInMemoryDatabase("DefaultConnection")
                           .UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<DatingContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<DefaultWebApplicationFactory<TEntryPoint>>>();
                    context.Database.EnsureCreated();   

                    try
                    {
                        Seed.SeedUsers(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {ex.Message}");
                    }
                }
            });

        }
    }
}
