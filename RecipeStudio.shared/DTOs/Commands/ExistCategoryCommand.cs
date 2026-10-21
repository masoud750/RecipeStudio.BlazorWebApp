using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeStudio.Shared.DTOs.Commands
{
  
    public record ExistCategoryCommand(
    int Id,

    [property: Required]
    [property: StringLength(20, MinimumLength = 3)]
    string Name);
}
