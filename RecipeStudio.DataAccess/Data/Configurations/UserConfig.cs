using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;



namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class UserConfig : ConfigBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(u => u.Id)
                .IsRequired()
                .HasColumnName("UserId");

            builder.HasKey(u => u.Id);

            // User <-> Recipe (M:N via Favorites)
            builder.HasMany(u => u.FavoriteRecipes)
                .WithMany(r => r.FavoritedBy)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorites",
                    j => j.HasOne<Recipe>()
                          .WithMany()
                          .HasForeignKey("RecipeId")
                          .OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<User>()
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.NoAction),
                    j =>
                    {
                        j.HasKey("UserId", "RecipeId");
                        j.ToTable("Favorites");
                    });

            builder.HasIndex(r => r.UserName)
                .IsUnique();

            builder.HasIndex(r => r.Email)
                .IsUnique();

            builder.Property(c => c.UserName);
        }
    }
}
