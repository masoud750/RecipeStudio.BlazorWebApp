using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Ingredient : EntityBase, IEquatable<Ingredient>
    {
        public string Name { get; set; } = string.Empty;

        public required string Quantity { get; set; }

        // Navigation
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

       
        public override bool Equals(object? obj)
        {
            if (obj is Ingredient other)
            {
                return Equals(other);
            }
            return false;
        }
        public bool Equals(Ingredient? other)
        {
            if(other is null) return false;
            if (other.Id == this.Id) return true;
            return false;
        }

        public override int GetHashCode()
        {
          return HashCode.Combine(Id, Name, Quantity);
        }
    }
}