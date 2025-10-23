using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace project.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(450, MinimumLength = 3, ErrorMessage = "product name length must be between 3 to 450 character")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 0")]
        [Precision(18, 2)] // precision: total digits, scale: digits after decimal
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}