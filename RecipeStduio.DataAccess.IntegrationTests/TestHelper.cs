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

            var context = new RecipeStudioDbContext(options);
            context.Database.EnsureCreated(); // apply schema

            return context;
        }
    }
}
