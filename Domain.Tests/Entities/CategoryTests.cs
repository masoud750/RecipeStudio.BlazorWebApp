using Domain.Entities;


namespace Domain.Tests.Entities
{
    internal class CategoryTests
    {
        [Test]
        public void Equals_WhenOtherIsNull_ReturnFalse()
        {
            // Arrange
            var category1 = new Category { Name = "Test Category" };
           

            // Act
            var result = category1.Equals((Category?)null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_CategoreisWithSameReference_ReturnTrue()
        {
            // Arrange
            var category1 = new Category { Name = "Test Category" };
            var category2 = category1;

            // Act
            var result = category1.Equals(category2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_CategoriesWithSameValues_ReturnTrue()
        {
            // Arrange
            var category1 = new Category { Name = "Test Category", Id=2,
             };
            var category2 = new Category { Name = "Test Category", Id=2 };

            // Act
            var result = category1.Equals(category2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_DifferentInstancesSameValuex_ReturnFalse()
        {
            var category1 = new Category { Name = "Test Category", Id = 1 };
            var category2 = new Category { Name = "Test Category", Id = 2 };

            var result = object.Equals(category1, category2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_DifferentInstancesDifferentValues_ReturnFalse()
        {
            var category1 = new Category { Name = "Test Category 1", Id = 1 };
            var category2 = new Category { Name = "Test Category 2",  Id = 2 };
            bool result = object.ReferenceEquals(category1, category2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_EqualObjects_ReturnSameHash()
        {
            var c1 = new Category { Id = 1, Name = "Food" };
            var c2 = new Category { Id = 1, Name = "Food" };

            Assert.That(c1.Equals(c2), Is.True);
            Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));

        }
    }
}
