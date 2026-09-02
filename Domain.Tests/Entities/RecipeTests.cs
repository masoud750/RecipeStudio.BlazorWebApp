using Domain.Entities;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Tests.Entities
{
    [TestFixture]
    internal class RecipeTests
    {
        [Test]
        public void Consstructor_WhenNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                var recipe = new Recipe(null!, 1,
                    new List<Category> { new Category() },
                        new List<Step> { new Step() },
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Consstructor_WhenNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var recipe = new Recipe(string.Empty, 1,
                    new List<Category> { new Category() },
                        new List<Step> { new Step() },
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenNameIsEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var recipe = new Recipe(string.Empty, 1,
                    new List<Category> { new Category() },
                        new List<Step> { new Step() },
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenCategoriesIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    null!,
                        new List<Step> { new Step() },
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenStepsIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    new List<Category> { new Category() },
                        null!,
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenIngredientsIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    new List<Category> { new Category() },
                        new List<Step> { new Step() },
                            null!);
            });
        }

        [Test]
        public void Constructor_WhenCategoriesIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    new List<Category>(),
                        new List<Step> { new Step() },
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenStepsIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    new List<Category> { new Category() },
                        new List<Step>(),
                            new List<Ingredient> { new Ingredient(){
                                Name = "milk",Quantity = "20 gr" } });
            });
        }

        [Test]
        public void Constructor_WhenIngredientsIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var recipe = new Recipe("Test Recipe", 1,
                    new List<Category> { new Category() },
                        new List<Step> { new Step() },
                            new List<Ingredient>());
            });

        }

        [Test]
        public void Constructor_WhenValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var categories = new List<Category> { new Category() };
            var steps = new List<Step> { new Step() };
            ICollection<Ingredient> ingredients = new List<Ingredient> { 
                new Ingredient() { Name = "milk", Quantity = "20 gr" } };
            // Act
            var recipe = new Recipe("Test Recipe", 1, categories, steps, ingredients);
            // Assert
            Assert.That(recipe.Name, Is.EqualTo("Test Recipe"));
            Assert.That(recipe.UserId, Is.EqualTo(1));
            Assert.That(recipe.Categories, Is.EqualTo(categories));
         
            Assert.That(recipe.Steps, Is.EqualTo(steps));
            Assert.That(recipe.Ingredients, Is.EqualTo(ingredients));
        }

        
    }
}

