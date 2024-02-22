using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Web.Extensions
{
    public static class MigrationManager
    {
        public static void MigrateAndSeedDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IDbContext>();
                var mapper = serviceScope.ServiceProvider.GetService<IMapper>();
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var userAccessor = serviceScope.ServiceProvider.GetService<IUserAccessor>();
                try
                {
                    if (dbContext.Database.GetMigrations().Count() > 0)
                    {
                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        }
    }
}