using RecipeStudio.Shared.DTOs;

namespace RecipeStudio.Shared.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCategory_WhenCategoryNullIs_ShouldThrowEx()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var recipe = new RecipeDTO();
                recipe.AddCategory(null);
            });
 

            Assert.That(ex.Message, Does.Contain(ex.ParamName));
        }

        [Test]
        public void AddCategory_AddValidCategory_ShouldNotBeEmpty()
        {
            var recipe = new RecipeDTO();
            Assert.That(recipe.Categories.Count, Is.EqualTo(0));

            recipe.AddCategory(new CategoryDTO(1, "testCategory"));
            Assert.That(recipe.Categories.Count, Is.EqualTo(1));
            Assert.That(recipe.Categories[0].Name, Does.Contain("Category"));
        }
    }
}
