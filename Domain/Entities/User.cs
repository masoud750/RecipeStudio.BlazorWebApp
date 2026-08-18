using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        public User() { }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;


        //Navigation property for the related UserProfile entity
        public ICollection<Recipe> Recipes { get; set; } = [];
        public ICollection<Recipe> FavoriteRecipes { get; set; } = [];
    }
}
