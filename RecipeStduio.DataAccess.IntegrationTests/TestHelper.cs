using RecipeStudio.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
namespace Recipe.DataAccess.IntegrationTests
{
    internal class TestHelper
    {
        public static RecipeStudioDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open(); // keep connection alive for the test

            var options = new DbContextOptionsBuilder<RecipeStudioDbContext>()
                .UseSqlite(connection)
                .Options;
            DesignTimeHostEnvironment hostEnv = new DesignTimeHostEnvironment()
            {
                EnvironmentName = "Testing"
            };
            var context = new RecipeStudioDbContext(options, hostEnv);
            context.Database.EnsureCreated(); // apply schema

            return context;
        }
    }
}
