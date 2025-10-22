using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project.Model
{
  public class Order
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [ValidateNever]
    public Customer Customer { get; set; }
    [ValidateNever]
    public List<OrderItem> OrderItems { get; set; }

  }
}