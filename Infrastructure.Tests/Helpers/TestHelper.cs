using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecipeStudio.DataAccess.Data;
using Respawn;


namespace RecipeStudio.Infrastructure.IntegrationTests.Helpers
{
    internal class TestHelper
    {
        internal static RecipeStudioDbContext? DbContext(string connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString, 
                nameof(connectionString));
            var options = new DbContextOptionsBuilder<RecipeStudioDbContext>()
                .UseSqlServer(connectionString)
                .Options;


            var context = new RecipeStudioDbContext(options);
           if( context.Database.EnsureCreated()) // apply schema
                return context;

           throw new Exception("Failed to create the database context.");
        }

      
    }
}
