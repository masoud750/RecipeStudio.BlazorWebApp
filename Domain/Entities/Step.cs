using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Step : EntityBase, IEquatable<Step>
    {
        public Step() { }

        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

       public override bool Equals(object? obj)
        {
            return obj is Step other && Equals(other);
        }
        public bool Equals(Step? other)
        {
            if(other is null) return false; 
            if(other.Id == this.Id) return true; // Assuming Id is a unique identifier for the Step entity
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}