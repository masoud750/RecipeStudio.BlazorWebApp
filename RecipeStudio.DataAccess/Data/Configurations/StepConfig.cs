using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeStudio.DataAccess.Data.Configurations
{
    internal class StepConfig : ConfigBase<Step>
    {
        public override void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.Property(s => s.Id).HasColumnName("StepId");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(s => s.Name).IsUnique();
          
        }
    }
}
