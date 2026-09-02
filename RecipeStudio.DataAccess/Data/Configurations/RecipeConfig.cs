using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeStudio.DataAccess.Data.Configurations;
using System.Reflection.Emit;


namespace Domain.Entities
{
    internal class RecipeConfig: ConfigBase<Recipe>
    {
        public RecipeConfig() { }
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            base.Configure(builder);
            builder.Property(r => r.Id).HasColumnName("RecipeId");
            builder.HasKey(r => r.Id);

            // User -> Recipe (1:N)            
            builder.HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId);

            // Recipe <-> Category (M:N via CategoryRecipes)

            builder.HasMany(r => r.Categories)
             .WithMany(c => c.Recipes)
             .UsingEntity<Dictionary<string, object>>(
                 "CategoryRecipes",
                 j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                 j => j.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId"),
                 j => j.HasKey("CategoryId", "RecipeId")
             );

            // Recipe <-> Step (M:N via RecipeSteps)
            builder.HasMany(r => r.Steps)
                .WithMany(s => s.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeSteps",
                    j => j.HasOne<Step>().WithMany().HasForeignKey("StepId"),
                    j => j.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId"),
                    j => j.HasKey("RecipeId", "StepId")
                );

            // Recipe <-> Ingredient (M:N via RecipeIngredients)

            builder.HasMany(r => r.Ingredients)
              .WithMany(i => i.Recipes)
              .UsingEntity<Dictionary<string, object>>(
                  "RecipeIngredients",
                  j => j.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId"),
                  j => j.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId"),
                  j => j.HasKey("RecipeId", "IngredientId")
              );

            // Recipe ↔ Category
            
               builder.HasMany(r => r.Categories)
                .WithMany(c => c.Recipes)
                .UsingEntity(j => j.ToTable("CategoryRecipes"));

            // Recipe ↔ Ingredient
            
                builder.HasMany(r => r.Ingredients)
                .WithMany(i => i.Recipes)
                .UsingEntity(j => j.ToTable("RecipeIngredients"));

            // Recipe ↔ Step            
                builder.HasMany(r => r.Steps)
                .WithMany(s => s.Recipes)
                .UsingEntity(j => j.ToTable("RecipeSteps"));

            // Uniqueness of recipe names should be handled in application logic,
            // since we want to allow duplicates for expired recipes and keep history.
            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Name)
                .IsRequired();

        }
    }
}
