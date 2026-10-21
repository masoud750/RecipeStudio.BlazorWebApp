using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeStudio.Shared.DTOs.Commands
{

    public record AddCategoryCommand
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
    }

}

