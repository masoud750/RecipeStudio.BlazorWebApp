using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeStudio.Shared.DTOs.Commands
{
    public record DeleteCategoryCommand(int id,
         [property: Required][property: Range(3, 20,
            ErrorMessage = "Name must be between 3 and 20.")] string name)
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
