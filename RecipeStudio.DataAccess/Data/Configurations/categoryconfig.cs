using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class CategoryConfig : ConfigBase<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Id).HasColumnName("CategoryId");
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Name).IsRequired()
                .HasMaxLength(100);
                  
                
            builder.HasIndex(c => c.Name).IsUnique();
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Category_Name_NotEmpty",
                    "Name <> ''"
                );
            });
        }
    }
}
