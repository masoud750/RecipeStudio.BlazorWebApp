using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class BaseConfig<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(b => b.ValidFrom)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.ValidTo)
                .HasDefaultValue(new DateTime(2090, 1, 1));
        }
    }
}
