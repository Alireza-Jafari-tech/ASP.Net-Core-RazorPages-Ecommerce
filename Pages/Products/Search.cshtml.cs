using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Products
{
  public class SearchModel : PageModel
{
    private readonly AppDbContext _context;

    public SearchModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public void OnGet()
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            query = query.Where(p => p.Name.Contains(SearchTerm) || p.Description.Contains(SearchTerm));
        }

        Products = query.ToList();
    }
}
}