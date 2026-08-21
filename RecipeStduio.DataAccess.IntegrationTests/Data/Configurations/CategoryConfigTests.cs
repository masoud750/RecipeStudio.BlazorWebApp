using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Recipe.DataAccess.IntegrationTests;
using RecipeStudio.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStduio.DataAccess.IntegrationTests.Data.Configurations
{
    [TestFixture]
    internal class CategoryConfigTests
    {
        [SetUp]
        public void Setup()
        {
            _dbContext = TestHelper.GetDbContext() ??
                throw new InvalidOperationException("Failed to initialize database context."); ;
        }

        RecipeStudioDbContext _dbContext;

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose(); // releases connection + deletes in-memory DB
        }

        [Test]
        public void Configuration_AddEntry_ShouldSucceed()
        {
            // Arrange
            var category = new Domain.Entities.Category
            {
                Name = "Dessert"
            };
            // Act
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            // Assert
            var savedCategory = _dbContext.Categories.Find(category.Id);
            Assert.That(savedCategory, Is.Not.Null);
            Assert.That(savedCategory.Name, Is.EqualTo("Dessert"));
        }

        [Test]
        public void Configuration_AddDuplicateEntry_ThrowsDbUpdateException()
        {
            // Arrange

            bool isThrown = false;
            var category = new Category { Name = "Dessert" };

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            // Act
            try
            {
                var sut = new Category { Name = "Dessert" };
                _dbContext.Categories.Add(sut);
               int i =  _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqliteEx = ex.InnerException as SqliteException;
                Assert.That(sqliteEx?.SqliteErrorCode, Is.EqualTo(19));
                isThrown = true;
            }

            // Assert
            Assert.That(isThrown, Is.True);
        }

        [Test]
        public void Configuration_AddEntryWithNullName_ThrowsDbUpdateException()
        {
            // Arrange
            bool isThrown = false;
            var category = new Category { Name = null! };
            // Act
            try
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqliteEx = ex.InnerException as SqliteException;
                Assert.That(sqliteEx?.SqliteErrorCode, Is.EqualTo(19));
                isThrown = true;
            }
            // Assert
            Assert.That(isThrown, Is.True);
        }
        
        [Test]
        public void Configuration_AddEntryWithEmptyName_ThrowsDbUpdateException()
        {
            // Arrange
            bool isThrown = false;
            var category = new Category { Name = "" };
            // Act
            try
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqliteEx = ex.InnerException as SqliteException;
                Assert.That(sqliteEx?.SqliteErrorCode, Is.EqualTo(19));
                isThrown = true;
            }
            catch(Exception ex)
            {
                // Handle other exceptions if necessary
                isThrown = true;
            }
            // Assert
            Assert.That(isThrown, Is.True);
        }
    }
}
