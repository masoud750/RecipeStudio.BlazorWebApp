using Domain.Entities;

namespace Domain.Tests.Entities
{
    [TestFixture]
    internal class IngredientTests
    {
        [Test]
        public void Equals_WhenOtherIsNull_ReturnFalse()
        {
            // Arrange
            var ingredient1 = new Ingredient { Name = "Test Ingredient", Quantity = "1 cup" };
            

            // Act
            var result = ingredient1.Equals((Ingredient?)null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_IngredientsWithSameReference_ReturnTrue()
        {
            // Arrange
            var ingredient1 = new Ingredient { Name = "Test Ingredient", Quantity = "1 cup", Id = 1 };
            var ingredient2 = ingredient1;

            // Act
            bool result = object.ReferenceEquals(ingredient1, ingredient2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_IngredientsWithSameValues_ReturnTrue()
        {
            // Arrange
            var ingredient1 = new Ingredient { Name = "Test Ingredient", Quantity = "1 cup", Id = 1 };
            var ingredient2 = new Ingredient { Name = "Test Ingredient", Quantity = "1 cup", Id = 1 };

            // Act
            var result = ingredient1.Equals(ingredient2);

            // Assert
            Assert.That(result, Is.True);
        }

        

        [Test]
        public void Equals_DifferentInstancesDifferentValues_ReturnFalse()
        {
            // Arrange
            var ingredient1 = new Ingredient { Name = "Test Ingredient 1", Quantity = "1 cup", Id = 1 };
            var ingredient2 = new Ingredient { Name = "Test Ingredient 2", Quantity = "2 cups", Id = 2 };

            // Act
            var result = object.Equals(ingredient1, ingredient2);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_EqualObjects_ReturnSameHash()
        {
            var c1 = new Ingredient { Id = 1, Name = "Test Ingredient", Quantity = "1 cup" };
            var c2 = new Ingredient { Id = 1, Name = "Test Ingredient", Quantity = "1 cup" };

            Assert.That(c1.Equals(c2), Is.True);
            Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));

        }
    }
}
