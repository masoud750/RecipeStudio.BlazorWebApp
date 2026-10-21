using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeStudio.Shared.DTOs
{
    public record ItemDTO(
     int Id,
     string Name,
     ItemType ItemType);

    public enum ItemType
    {
        Category,
        Step,
        Ingredient,
        Default
    }
}
