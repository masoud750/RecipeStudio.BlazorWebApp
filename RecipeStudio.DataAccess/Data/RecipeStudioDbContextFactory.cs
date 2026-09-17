using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace RecipeStudio.DataAccess.Data
{
    public class RecipeStudioDbContextFactory : IDesignTimeDbContextFactory<RecipeStudioDbContext>
    {
        public RecipeStudioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecipeStudioDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=RecipeStudio_Test;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            DesignTimeHostEnvironment hostEnv = new DesignTimeHostEnvironment
            {
                EnvironmentName = Environments.Development,
                ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name ?? "RecipeStudio",
                ContentRootPath = Directory.GetCurrentDirectory()
            };

            return new RecipeStudioDbContext(optionsBuilder.Options, hostEnv);
        }

       
    }
    // Minimal IHostEnvironment implementation for design-time
    public class DesignTimeHostEnvironment : IHostEnvironment
    {

        public string EnvironmentName { get; set; } = Environments.Development;
        public string ApplicationName { get; set; } = "RecipeStudio";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; } = new PhysicalFileProvider(Directory.GetCurrentDirectory());


    }
}

