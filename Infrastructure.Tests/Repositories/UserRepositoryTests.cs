using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RecipeStudio.DataAccess.Data;
using RecipeStudio.Infrastructure.IntegrationTests.Helpers;
using RecipeStudio.Infrastructure.Repositories;
using Respawn;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;


namespace RecipeStudio.Infrastructure.IntegrationTests.Repositories
{
    [TestFixture]
    internal class UserRepositoryTests
    {
        [SetUp]
        public async Task Setup()
        {
            await InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            // Initialize the database or any required resources for testing
            _dbName = $"RecipeStudio_Infrastructure_IntegrationTests_{Guid.NewGuid()}";

            _connectionString = $"Server=.;Database={_dbName};" +
                $"Trusted_Connection=True;TrustServerCertificate=True;";

            using var connection = new SqlConnection(
                "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            await connection.OpenAsync();

            var cmd = connection.CreateCommand();
            cmd.CommandText = $"CREATE DATABASE [{_dbName}]";
            await cmd.ExecuteNonQueryAsync();

            // Jetzt EF Core Context erzeugen

            DesignTimeHostEnvironment hostEnv = new DesignTimeHostEnvironment()
            {
                EnvironmentName = "Testing"
            };

            _dbContext = new RecipeStudioDbContext(
                        new DbContextOptionsBuilder<RecipeStudioDbContext>()
                            .UseSqlServer(_connectionString)
                            .Options, hostEnv
                    );
            // now create tables
            await _dbContext.Database.EnsureCreatedAsync();
        }


        private RecipeStudioDbContext? _dbContext;
        private string? _connectionString;
        private string _dbName;

        private bool IsDBCreated => _dbContext != null;

        [TearDown]
        public async Task TearDownAsync()
        {
            if (IsDBCreated)
            {
                await TestHelper.DisposeAsync(_dbContext!, _dbName);
                _dbContext?.Dispose();  // important after disposing in DisposeAsync!
            }
        }


        [Test]
        public void Constructor_WhenDbcontextIsNull_ThrowsArgumentNullException()
        {

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new UserRepository(null!));
            Assert.That(ex.ParamName, Is.EqualTo("dbContext"));
        }

        [Test]
        public void AddAsync_WhenUserIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var repository = new UserRepository(_dbContext!);
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddAsync(null!));
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        [Test]
        public async Task AddAsync_WhenEntityIsValid_ShouldAddUser()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);
            var user = new Domain.Entities.User
            {
                UserName = "testuser",
                PasswordHash = "testpassword",
                Email = "testuser@example.com",
                ValidFrom = DateTime.Now,
                ValidTo = new DateTime(2099, 12, 30)
            };

            await sut.AddAsync(user);
        }

        [Test]
        public void ExistAsync_WhenUserIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                        async () => await sut.ExistAsync(null!));
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        [Test]
        public async Task ExistAsync_WhenUserExists_ShouldReturnTrue()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes("password"));
            var hash = Convert.ToHexString(bytes); // .NET 5+
            var user = new Domain.Entities.User
            {
                UserName = "testuser",
                PasswordHash = "hash119",
                Email = "testuser@example.com"
            };

            await sut.AddAsync(user);
            var result = await sut.ExistAsync(user);
            Assert.That(result, Is.True);
        }


        [Test]
        public async Task GetTaskAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);
      
            var userOne = new Domain.Entities.User
            {
                UserName = "testuserOne",
                PasswordHash = "hash119",
                Email = "testuserOne@example.com"
            };

            var userTwo = new Domain.Entities.User
            {
                UserName = "testuserTwo",
                PasswordHash = "hash120",
                Email = "testuserTwo@example.com"
            };

            await sut.AddAsync(userOne);
            await sut.AddAsync(userTwo);
            var userFromDb = await sut.GetAllAsync();
            Assert.That(userFromDb.Count, Is.EqualTo(2));
            Assert.That(userFromDb.Any(u => u.UserName == "testuserOne"), Is.True);
            Assert.That(userFromDb.Any(u => u.UserName == "testuserTwo"), Is.True);
            Assert.That(userFromDb.Any(u => u.Email == "testuserOne@example.com"), Is.True);
            Assert.That(userFromDb.Any(u => u.Email == "testuserTwo@example.com"), Is.True);
            Assert.That(userFromDb.Any(u => u.PasswordHash == "hash119"), Is.True);
            Assert.That(userFromDb.Any(u => u.PasswordHash == "hash120"), Is.True);

        }

        [Test]
        public async Task GetById_PassingInvalidId_ShouldReturnNull()
        {                        
            UserRepository sut = new UserRepository(_dbContext!);
            User? user = await sut.GetByIdAsync(0);
            Assert.That(user, Is.Null);            
        }

        [Test]
        public async Task GetById_passingValidId_ShouldReturnUser()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);

            var userOne = new Domain.Entities.User
            {
                UserName = "testuserOne",
                PasswordHash = "hash119",
                Email = "testuserOne@example.com"
            };
            await sut.AddAsync(userOne);
            User? user = await sut.GetByIdAsync(1);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserName, Is.EqualTo("testuserOne"));
        }

        [Test]
        public async Task UpdateAsync_PassingModifiedEntity_ShouldBeUpdated()
        {
            // Arrange
            var sut = new UserRepository(_dbContext!);

            var userOne = new Domain.Entities.User
            {
                UserName = "testuserOne",
                PasswordHash = "hash119",
                Email = "testuserOne@example.com"
            };
            await sut.AddAsync(userOne);
            User? user = await sut.GetByIdAsync(1);
            user?.Email = "modified@example.com";
            await sut.UpdateAsync(user!);
            user = await sut.GetByIdAsync(1);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Email,
                Is.EqualTo("modified@example.com"));
        }
    }
}
