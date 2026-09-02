using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Recipe : EntityBase
    {
        // EF Core will use this
        public Recipe()
        {
            Categories = new List<Category>();
            Steps = new List<Step>();
            Ingredients = new List<Ingredient>();
        }

        public Recipe(string name, int userId,
                   IEnumerable<Category> categories,
                   IEnumerable<Step> steps,
                   IEnumerable<Ingredient> ingredients)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Recipe name cannot be empty.", nameof(name));
            ArgumentNullException.ThrowIfNull(categories, nameof(categories));
            ArgumentNullException.ThrowIfNull(steps, nameof(steps));
            ArgumentNullException.ThrowIfNull(ingredients, nameof(ingredients));
            if (!categories.Any()) throw new ArgumentException("Recipe must have at least one category.");
            if (!steps.Any()) throw new ArgumentException("Recipe must have at least one step.");
            if (!ingredients.Any()) throw new ArgumentException("Recipe must have at least one ingredient.");

            Name = name;
            UserId = userId;
            Categories = categories.ToList();
            Steps = steps.ToList();
            Ingredients = ingredients.ToList();
        }



        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
       

        [NotMapped]
        public bool IsExpired => ValidTo < DateTime.Now;

        // Foreign key
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Step> Steps { get; set; } = new List<Step>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public ICollection<User> FavoritedBy { get; set; } = new List<User>();

        
    }
}