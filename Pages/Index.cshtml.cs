using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? UserId { get; set; }

        [BindProperty]
        public int? CategoryId { get; set; }

        // [BindProperty(SupportsGet = true)]
        // public string? SearchTerm { get; set; }

        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> SearchedProducts { get; set; }

        public void OnGet(int? user)
        {
            Categories = _context.Categories.ToList();
            Products = _context.Products
    .OrderBy(p => p.Id) // or any other property
    .Skip(24)
    .Take(8)
    .ToList();

            if (user.HasValue)
                ViewData["UserId"] = user;
        }

        // public void OnGetSearch()
        // {
        //     var query = _context.Products
        //         .Include(p => p.Category)
        //         .AsQueryable();

        //     if (!string.IsNullOrEmpty(SearchTerm))
        //     {
        //         query = query.Where(p => p.Name.Contains(SearchTerm) || p.Description.Contains(SearchTerm));
        //     }

        //     SearchedProducts = query.ToList();
        // }

        public IActionResult OnPostCategoryFilter()
        {
            if (!ModelState.IsValid)
                return Page();


            return RedirectToPage("/Products/ReadByCategory", new
            { categoryId = CategoryId, userId = ViewData["UserId"], page = 1 }
                );
        }

        public void OnPost()
        {

        }
    }
}
