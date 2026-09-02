using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.DataAccess.Data
{
    public class RecipeStudioDbContext : DbContext
    {
        public RecipeStudioDbContext(DbContextOptions<RecipeStudioDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Load automatically all IEntityTypeConfiguration<T> classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeStudioDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
     
            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "ChefAnna", Email = "anna@example.com", PasswordHash = "hash1" },
                new User { Id = 2, UserName = "ChefSam", Email = "masoud@example.com", PasswordHash = "hash2" },
                new User { Id = 3, UserName = "ChefLuca", Email = "luca@example.com", PasswordHash = "hash3" },
                new User { Id = 4, UserName = "ChefMaria", Email = "maria@example.com", PasswordHash = "hash4" },
                new User { Id = 5, UserName = "ChefTom", Email = "tom@example.com", PasswordHash = "hash5" },
                new User { Id = 6, UserName = "ChefSara", Email = "sara@example.com", PasswordHash = "hash6" },
                new User { Id = 7, UserName = "ChefAli", Email = "ali@example.com", PasswordHash = "hash7" },
                new User { Id = 8, UserName = "ChefEmma", Email = "emma@example.com", PasswordHash = "hash8" },
                new User { Id = 9, UserName = "ChefJohn", Email = "john@example.com", PasswordHash = "hash9" },
                new User { Id = 10, UserName = "ChefSophia", Email = "sophia@example.com", PasswordHash = "hash10" }
                );



            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Italian" },
                new Category { Id = 2, Name = "Indian" },
                new Category { Id = 3, Name = "Mexican" },
                new Category { Id = 4, Name = "French" },
                new Category { Id = 5, Name = "Japanese" },
                new Category { Id = 6, Name = "Chinese" },
                new Category { Id = 7, Name = "Mediterranean" },
                new Category { Id = 8, Name = "American" },
                new Category { Id = 9, Name = "Thai" },
                new Category { Id = 10, Name = "Spanish" }
            );

            // Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Spaghetti", Quantity = "200g" },
                new Ingredient { Id = 2, Name = "Chicken", Quantity = "500g" },
                new Ingredient { Id = 3, Name = "Tomatoes", Quantity = "3 pcs" },
                new Ingredient { Id = 4, Name = "Garlic", Quantity = "2 cloves" },
                new Ingredient { Id = 5, Name = "Olive Oil", Quantity = "50ml" },
                new Ingredient { Id = 6, Name = "Rice", Quantity = "250g" },
                new Ingredient { Id = 7, Name = "Beef", Quantity = "400g" },
                new Ingredient { Id = 8, Name = "Potatoes", Quantity = "3 pcs" },
                new Ingredient { Id = 9, Name = "Cheese", Quantity = "100g" },
                new Ingredient { Id = 10, Name = "Basil", Quantity = "10 leaves" }
            );

            // Steps
            modelBuilder.Entity<Step>().HasData(
                new Step { Id = 1, Name = "Boil pasta until al dente." },
                new Step { Id = 2, Name = "Cook chicken with onions and garlic." },
                new Step { Id = 3, Name = "Chop vegetables finely." },
                new Step { Id = 4, Name = "Simmer sauce for 20 minutes." },
                new Step { Id = 5, Name = "Preheat oven to 180°C." },
                new Step { Id = 6, Name = "Grill beef until medium rare." },
                new Step { Id = 7, Name = "Steam rice until fluffy." },
                new Step { Id = 8, Name = "Mash potatoes with butter." },
                new Step { Id = 9, Name = "Mix salad dressing." },
                new Step { Id = 10, Name = "Serve hot with garnish." }
            );

            // Recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Name = "Spaghetti Carbonara", Description = "Classic Italian pasta.", UserId = 1 },
                new Recipe { Id = 2, Name = "Chicken Curry", Description = "Aromatic curry with chicken.", UserId = 2 },
                new Recipe { Id = 3, Name = "Beef Tacos", Description = "Mexican street food.", UserId = 3 },
                new Recipe { Id = 4, Name = "Sushi Roll", Description = "Japanese delicacy.", UserId = 4 },
                new Recipe { Id = 5, Name = "French Onion Soup", Description = "Rich and savory.", UserId = 5 },
                new Recipe { Id = 6, Name = "Sweet and Sour Pork", Description = "Chinese classic.", UserId = 6 },
                new Recipe { Id = 7, Name = "Greek Salad", Description = "Fresh Mediterranean salad.", UserId = 7 },
                new Recipe { Id = 8, Name = "Burger Deluxe", Description = "American comfort food.", UserId = 8 },
                new Recipe { Id = 9, Name = "Pad Thai", Description = "Thai stir-fried noodles.", UserId = 9 },
                new Recipe { Id = 10, Name = "Paella", Description = "Spanish rice dish.", UserId = 10 }
            );


            modelBuilder.Entity("CategoryRecipes").HasData(
                new { RecipeId = 1, CategoryId = 1 },
                new { RecipeId = 2, CategoryId = 2 },
                new { RecipeId = 3, CategoryId = 3 },
                new { RecipeId = 4, CategoryId = 5 },
                new { RecipeId = 5, CategoryId = 4 },
                new { RecipeId = 6, CategoryId = 6 },
                new { RecipeId = 7, CategoryId = 7 },
                new { RecipeId = 8, CategoryId = 8 },
                new { RecipeId = 9, CategoryId = 9 },
                new { RecipeId = 10, CategoryId = 10 }
            );

            modelBuilder.Entity("RecipeIngredients").HasData(
                new { RecipeId = 1, IngredientId = 1 },
                new { RecipeId = 2, IngredientId = 2 },
                new { RecipeId = 3, IngredientId = 7 },
                new { RecipeId = 4, IngredientId = 6 },
                new { RecipeId = 5, IngredientId = 4 },
                new { RecipeId = 6, IngredientId = 7 },
                new { RecipeId = 7, IngredientId = 9 },
                new { RecipeId = 8, IngredientId = 8 },
                new { RecipeId = 9, IngredientId = 6 },
                new { RecipeId = 10, IngredientId = 10 }
            );

            modelBuilder.Entity("RecipeSteps").HasData(
                new { RecipeId = 1, StepId = 1 },
                new { RecipeId = 2, StepId = 2 },
                new { RecipeId = 3, StepId = 3 },
                new { RecipeId = 4, StepId = 4 },
                new { RecipeId = 5, StepId = 5 },
                new { RecipeId = 6, StepId = 6 },
                new { RecipeId = 7, StepId = 7 },
                new { RecipeId = 8, StepId = 8 },
                new { RecipeId = 9, StepId = 9 },
                new { RecipeId = 10, StepId = 10 }
            );

            modelBuilder.Entity("Favorites").HasData(
                new { UserId = 1, RecipeId = 2 }, // ChefAnna favorites Chicken Curry
                new { UserId = 2, RecipeId = 1 }, // ChefSam favorites Spaghetti Carbonara
                new { UserId = 3, RecipeId = 5 }, // ChefLuca favorites French Onion Soup
                new { UserId = 4, RecipeId = 4 }, // ChefMaria favorites Sushi Roll
                new { UserId = 5, RecipeId = 3 }, // ChefTom favorites Beef Tacos
                new { UserId = 6, RecipeId = 6 }, // ChefSara favorites Sweet and Sour Pork
                new { UserId = 7, RecipeId = 7 }, // ChefAli favorites Greek Salad
                new { UserId = 8, RecipeId = 8 }, // ChefEmma favorites Burger Deluxe
                new { UserId = 9, RecipeId = 9 }, // ChefJohn favorites Pad Thai
                new { UserId = 10, RecipeId = 10 } // ChefSophia favorites Paella
            );
        }
    }
}
