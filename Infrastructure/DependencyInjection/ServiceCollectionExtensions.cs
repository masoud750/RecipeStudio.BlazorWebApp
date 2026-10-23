using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeStudio.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;


namespace Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
    
         public static IServiceCollection RegisterDBService(this 
                                                    IServiceCollection services,
             string connectionString)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(connectionString, 
                nameof(connectionString));
            services.AddDbContext<RecipeStudioDbContext>((sp, options) =>
            {
                var env = sp.GetRequiredService<IHostEnvironment>();
                options.UseSqlServer(connectionString);
            });
            
            return services;
        }

        public static WebApplication ApplyMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RecipeStudioDbContext>();
                db.Database.Migrate();
            }
            return app;
        }

    }
}
