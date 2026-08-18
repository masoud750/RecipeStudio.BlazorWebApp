using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public required string Quantity { get; set; }

        // Navigation
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}