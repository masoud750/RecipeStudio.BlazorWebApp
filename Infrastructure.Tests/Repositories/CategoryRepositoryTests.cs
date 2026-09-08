using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecipeStudio.DataAccess.Data;
using RecipeStudio.Infrastructure.Repositories;
using Respawn;
using System.Data.Common;
using Domain.Entities;
using System.Runtime.CompilerServices;
namespace RecipeStudio.Infrastructure.Tests.Repositories
{
    [TestFixture]

    public class CategoryRepositoryTests
    {
        [SetUp]
        public async Task Setup()
        {

            await InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            _dbName = $"RecipeStudio_Infrastructure_IntegrationTests_{Guid.NewGuid()}";

            var connectionString = $"Server=.;Database={_dbName};" +
                $"Trusted_Connection=True;TrustServerCertificate=True;";

            using var connection = new SqlConnection(
                "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = $"CREATE DATABASE [{_dbName}]";
            await cmd.ExecuteNonQueryAsync();

            // Jetzt EF Core Context erzeugen
            _dbContext = new RecipeStudioDbContext(
                new DbContextOptionsBuilder<RecipeStudioDbContext>()
                    .UseSqlServer(connectionString)
                    .Options
            );

            await _dbContext.Database.MigrateAsync();
        }


        private RecipeStudioDbContext? _dbContext;
        private string? _connectionString;
        private DbConnection? _DbConnection;
        private Respawner? _respawner;

        private string _dbName;

        private bool IsDBCreated => _dbContext != null;

        [TearDown]
        public async Task TearDownAsync()
        {
            await DisposeAsync();
        }

        public async Task DisposeAsync()
        {
            // frist close the Test-DB
            await _dbContext.DisposeAsync();

            using var connection = new SqlConnection(
                "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = $@"
            IF EXISTS (SELECT name FROM sys.databases WHERE name = '{_dbName}')
            BEGIN
                ALTER DATABASE [{_dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{_dbName}];
            END";


            await cmd.ExecuteNonQueryAsync();
        }

        [Test]
        public void Constructor_WhenDbcontextIsNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CategoryRepository(null!));
            Assert.That(ex.ParamName, Is.EqualTo("dbContext"));
        }

        [Test]
        public void AdddAsync_WhenCategoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange

            var repository = new CategoryRepository(_dbContext!);
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddAsync(null!));
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        [Test]
        public async Task AddAsync_AddingValidEntry_ShouldBeAdded()
        {
            var entity = new Category()
            {
                Name = "Test",
            };

            var sut = new CategoryRepository(_dbContext!);
            await sut.AddAsync(entity);


        }

        [Test]
        public async Task ExistAsync_WhenEntityIsNull_ThrowsArgumentNullException()
        {

            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                var sut = new CategoryRepository(_dbContext!);
                _ = await sut.ExistAsync(null!);

            });

            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }


        [Test]
        public async Task ExistAsync_PassingExistingEntity_ShouldReturnTrue()
        {
            /// <see cref="RecipeStudioDbContext"/>
            var category = new Category() { Id = 2, Name = "Thahi" };

            var sut = new CategoryRepository(_dbContext!);
            bool result = await sut.ExistAsync(category);
            Assert.That(result, Is.True);

        }

        [Test]
        public async Task DeleteAsync_WhenEntityIsNull_ShouldThrowArgNullEx()
        {
            var sut = new CategoryRepository(_dbContext!);
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
             await sut.DeleteAsync(null!));
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }
        [Test]
        public async Task DeleteAsync_DeleteExitingEntity_ExisingingAsyncShouldReturnFalse()
        {
            /// <see cref="RecipeStudioDbContext"/>
            var category = new Category() { Id = 2, Name = "Thahi" };
            var sut = new CategoryRepository(_dbContext!);
            await sut.DeleteAsync(category);
            Assert.That(await sut.ExistAsync(category), Is.False);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllCategories()
        {
            var sut = new CategoryRepository(_dbContext!);
            var categories = await sut.GetAllAsync();
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories.Count, Is.EqualTo(10));
        }
    }
}
