using Microsoft.AspNetCore.Hosting;
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

            var hostEnv = new DesignTimeHostEnvironment()
            {
                EnvironmentName = "Testing"
            };
            var context = new RecipeStudioDbContext(options,hostEnv);
           if( context.Database.EnsureCreated()) // apply schema
                return context;

           throw new Exception("Failed to create the database context.");
        }

        public static async Task DisposeAsync(RecipeStudioDbContext dbContext, string dbName)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            ArgumentException.ThrowIfNullOrEmpty(dbName, nameof(dbName));
            // frist close the Test-DB
            await dbContext.DisposeAsync();

            using var connection = new SqlConnection(
                "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = $@"
            IF EXISTS (SELECT name FROM sys.databases WHERE name = '{dbName}')
            BEGIN
                ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{dbName}];
            END";


            await cmd.ExecuteNonQueryAsync();
        }
    }
}
