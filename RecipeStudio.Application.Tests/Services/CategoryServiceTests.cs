using Domain.Entities;
using Infrastructure.Repositories;
using RecipeStudio.Application.Services;
using RecipeStudio.Application.Tests.Mocks;

namespace RecipeStudio.Application.Tests.Services
{
    [TestFixture]
    internal class CategoryServiceTests
    {
        [Test]
        public void constructor_WhenRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CategoryService(null!));
            Assert.That(ex.ParamName, Is.EqualTo("repos"));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfCategories()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();                      
            var sut = new CategoryService(mockRepo);
            // Act
            List<Category> result = await sut.GetAllAsync();
            // Assert
            Assert.That(result, Is.Not.Null);   
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetByIdAsync_PassingValidId_ShouldReturnCategory()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo);
            int validId = 1;
            // Act
            Category? result = await sut.GetByIdAsync(validId);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(validId));
        }

        [Test]
        public async Task GetByIdAsync_PassingInvalidId_ShouldReturnNull()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo);
            int invalidId = 999;
            // Act
            Category? result = await sut.GetByIdAsync(invalidId);
            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_PassingValidCategory_ShouldBeDelted()
        {
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo);
            var categoryToDelete = new Category { Id = 1, Name = "Test Category 1" };
            await sut.DeleteAsync(categoryToDelete);
            var categories = await sut.GetAllAsync();
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories.Count, Is.EqualTo(2));
            Assert.That(categories.Any( x => x.Id == categoryToDelete.Id), Is.False);

        }

        [Test]
        public async Task ExistAsync_PassingValidCategory_ShouldReturnTrue()
        {
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo);
            var categoryToCheck = new Category { Id = 1, Name = "Test Category 1" };
            bool result = await sut.ExistAsync(categoryToCheck);
            Assert.That(result, Is.True);
        }

    }
}

      
