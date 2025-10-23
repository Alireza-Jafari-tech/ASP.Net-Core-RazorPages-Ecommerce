using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project.Model
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ShoppingCartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}