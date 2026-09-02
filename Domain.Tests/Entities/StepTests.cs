using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests.Entities
{
    internal class StepTests
    {
        [Test]
        public void Equals_WhenOtherIsNull_ReturnFalse()
        {
            // Arrange
            var Step1 = new Step { Name = "Test Step" };


            // Act
            var result = Step1.Equals((Step?)null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_StepsWithSameReference_ReturnTrue()
        {
            // Arrange
            var Step1 = new Step { Name = "Test Step", Id = 1 };
            var Step2 = Step1;

            // Act
            var result = Step1.Equals(Step2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_StepsWithSameValues_ReturnTrue()
        {
            // Arrange
            var Step1 = new Step
            {
                Name = "Test Step",
                Id = 2,
            };
            var Step2 = new Step { Name = "Test Step", Id = 2 };

            // Act
            var result = Step1.Equals(Step2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_DifferentInstancesSameValuex_ReturnFalse()
        {
            var Step1 = new Step { Name = "Test Step", Id = 1 };
            var Step2 = new Step { Name = "Test Step", Id = 2 };

            var result = object.Equals(Step1, Step2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_DifferentInstancesDifferentValues_ReturnFalse()
        {
            var Step1 = new Step { Name = "Test Step 1", Id = 1 };
            var Step2 = new Step { Name = "Test Step 2", Id = 2 };
            bool result = object.ReferenceEquals(Step1, Step2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_EqualObjects_ReturnSameHash()
        {
            var c1 = new Step { Id = 1, Name = "Test Step" };
            var c2 = new Step { Id = 1, Name = "Test Step" };

            Assert.That(c1.Equals(c2), Is.True);
            Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));
        }
    }
}
