namespace Domain.Entities
{
    public class Category : EntityBase, IEquatable<Category>
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

       

        public override bool Equals(object? obj)
        {
            if (obj is Category other) return Equals(other); 
            return false;
        }

        //Value equality comparison for Category
        public bool Equals(Category? other)
        {
            if (other == null) return false;                     
            return Id == other.Id;    // Id is sufficent                      
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(Name, Id);
        }
    }
}