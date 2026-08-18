using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}