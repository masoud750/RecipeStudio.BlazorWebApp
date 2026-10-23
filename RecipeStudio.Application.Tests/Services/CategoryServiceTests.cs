using Domain.Entities;
using Infrastructure.Repositories;
using RecipeStudio.Application.Services;
using RecipeStudio.Application.Tests.Mocks;
using RecipeStudio.Shared.DTOs.Commands;
using RecipeStudio.Shared.DTOs;

namespace RecipeStudio.Application.Tests.Services
{
    [TestFixture]
    internal class CategoryServiceTests
    {
        [Test]
        public void constructor_WhenRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CategoryService(null!,
                new LoggerMock()));
            Assert.That(ex.ParamName, Is.EqualTo("repos"));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfCategories()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();                      
            var sut = new CategoryService(mockRepo, new LoggerMock());
            // Act
            List<CategoryDTO> result = await sut.GetAllAsync();
            // Assert
            Assert.That(result, Is.Not.Null);   
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Any(x => x.ItemType != ItemType.Category),
                        Is.False);
        }

        [Test]
        public async Task GetByIdAsync_PassingValidId_ShouldReturnCategory()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo, new LoggerMock());
            int validId = 1;
            // Act
            ItemDTO? result = await sut.GetByIdAsync(validId);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(validId));
            Assert.That(result.ItemType, Is.EqualTo(ItemType.Category));
        }

        [Test]
        public async Task GetByIdAsync_PassingInvalidId_ShouldReturnNull()
        {
            // Arrange
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo, new LoggerMock());
            int invalidId = 999;
            // Act
            ItemDTO? result = await sut.GetByIdAsync(invalidId);
            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_PassingValidCategory_ShouldBeDelted()
        {
            var mockRepo = new CategoryRepositoryMock();
            var sut = new CategoryService(mockRepo, new LoggerMock());
            var categoryToDelete = new Shared.DTOs.Commands.DeleteCategoryCommand(1, "Test Category 1");
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
            var sut = new CategoryService(mockRepo, new LoggerMock());
            var categoryToCheck = new ExistCategoryCommand(1, "Test Category 1");
            bool result = await sut.ExistAsync(categoryToCheck);
            Assert.That(result, Is.True);
        }

    }
}

      
