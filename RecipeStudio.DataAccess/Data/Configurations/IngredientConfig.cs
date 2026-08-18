using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class IngredientConfig : ConfigBase<Ingredient>
    {
        public override void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.Id).HasColumnName("IngredientId");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100)
                .UseCollation("NOCASE");
            builder.HasIndex(i => i.Name).IsUnique();
        }

    }
}
