using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project.Model
{
  public class OrderItem
  {
    public int ProductId { get; set; }
    public int OrderId { get; set; }

    [Required]
    public int Quantity { get; set; }
    
    [ValidateNever]
    public Product Product { get; set; } // Navigation property
    [ValidateNever]
    public Order Order { get; set; } // Navigation property
  }
}