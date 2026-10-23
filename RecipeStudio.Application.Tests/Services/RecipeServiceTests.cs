using RecipeStudio.Application.Services;
using RecipeStudio.Application.Tests.Mocks;
using RecipeStudio.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Tests.Services
{
    internal class RecipeServiceTests
    {
        [Test]
        public void RecipeService_WhenRepositoryIsNull_ShouldThrowArgNullEx()
        {
            var ex = Assert.Throws<ArgumentNullException>(static () =>
            {
                _ = new RecipeService(null!, new LoggerMock());
            });
            
            Assert.That(ex.Message, Does.Contain(ex.ParamName));
        }

        [Test]
        public void RecipeService_WhenLoggerIsNull_ShouldThrowArgNullEx()
        {
            var ex = Assert.Throws<ArgumentNullException>(static () =>
            {
                _ = new RecipeService(new RecipeRepositoryMock(), null!);
            });

            Assert.That(ex.Message, Does.Contain(ex.ParamName));

        }
 

        [Test]
        public async Task GetAllAsync_ShouldReturnAllNotExpiredRecipes()
        {
            var service = new RecipeService(new RecipeRepositoryMock(),
                new LoggerMock());
            List<RecipeDTO> recipes = await service.GetAllAsync();
            Assert.That(recipes.Count, Is.EqualTo(4));
        }
    }
}
