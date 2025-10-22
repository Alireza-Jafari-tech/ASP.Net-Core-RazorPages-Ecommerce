using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace project.Model
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Category name length must be between 3 to 150 character")]
    public string Name { get; set; }

    [StringLength(350, MinimumLength = 3, ErrorMessage = "Category description length must be between 3 to 350 character")]
    public string? Description { get; set; }
    public string IconUrl { get; set; }
    [ValidateNever]
    public List<Product> Products { get; set; }
  }
}