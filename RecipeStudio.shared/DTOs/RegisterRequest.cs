using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipeStudio.Shared.DTOs
{

    public class RegisterRequest 
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max length should be 10")]
        [MinLength(5, ErrorMessage = "Min length should be 5")]
        public  string UserName { get; set; } = string.Empty;

        [Required]
        [Range(5,10,ErrorMessage ="Password must be between 5 and 10.")]
        public  string Password { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MinLength(5, ErrorMessage = "Min length should be 5")]
        public  string Email { get; set; } = string.Empty;

       
    }
}
