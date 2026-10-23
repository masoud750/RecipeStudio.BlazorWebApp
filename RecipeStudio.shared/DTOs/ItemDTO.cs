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

    public record CategoryDTO : ItemDTO
    {
        public CategoryDTO(int id,
                                string name):base(id, 
                                    name, ItemType.Category)
        {

        }
    }

    public record StepDTO : ItemDTO
    {
        public StepDTO(int id,
                                string name) : base(id,
                                    name, ItemType.Step)
        {

        }
    }

    public record IngredientDTO : ItemDTO
    {
        public IngredientDTO(
                int id,
                string name,               
                string quantity = "Unknown"): base(id,name,
                                                ItemType.Category)
        {
            ArgumentNullException.ThrowIfNull(
                quantity, nameof(quantity));

            Quantity = quantity;
        }

        public string Quantity { get; init; }
    }
    
    

    public enum ItemType
    {
        Category,
        Step,
        Ingredient,
        Default
    }
}
