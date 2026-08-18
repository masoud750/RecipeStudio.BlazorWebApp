using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class ConfigBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
           builder.Property(b => b.ValidFrom)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.ValidTo)
                .HasDefaultValue(new DateTime(2090, 1, 1, 0, 0, 0));
        }

       
    }
}
