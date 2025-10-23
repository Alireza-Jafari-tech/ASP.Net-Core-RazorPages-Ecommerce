using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "name length must be between 3 to 250 character")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
    }
}