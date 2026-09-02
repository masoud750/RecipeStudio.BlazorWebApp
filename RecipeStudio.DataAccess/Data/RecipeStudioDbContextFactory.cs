using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecipeStudio.DataAccess.Data
{
    public class RecipeStudioDbContextFactory : IDesignTimeDbContextFactory<RecipeStudioDbContext>
    {
        public RecipeStudioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecipeStudioDbContext>();

            // TODO: Use your real connection string here
            //optionsBuilder.UseSqlServer(
            //    "Server=localhost;Database=RecipeStudioDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer(
            "Server=.;Database=RecipeStudio_Test;Trusted_Connection=True;TrustServerCertificate=True;"
        );
            return new RecipeStudioDbContext(optionsBuilder.Options);
        }
    }
}

