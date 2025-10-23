using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Products
{
    public class ReadByCategoryModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReadByCategoryModel(AppDbContext context)
        {
            _context = context;
        }


        public int? UserId { get; set; }

        public List<Product> Products { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public int PageSize { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; } = 1;

        public IActionResult OnGet(int? categoryId)
        {
            if (categoryId == null)
            {
                return RedirectToPage("/NotFound");
            }

            if (categoryId.HasValue)
            {
                CategoryId = categoryId ?? 0;
            }

            // 1. Check if category exists FIRST
            var category = _context.Categories
                .Include(i => i.Products)
                .FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                return RedirectToPage("/NotFound");
            }

            Category = category;

            // 2. Get products from the category
            var products = Category.Products?.AsQueryable() ?? Enumerable.Empty<Product>().AsQueryable();

            // 3. Apply ordering
            // OrderFilter = orderFilter; // Set the property from parameter
            // switch (orderFilter?.ToLower())
            // {
            //     case "price-low":
            //         products = products.OrderByDescending(o => o.Price);
            //         break;
            //     case "price-high":
            //         products = products.OrderBy(o => o.Price);
            //         break;
            //     default:
            //         products = products.OrderBy(o => o.Name);
            //         break;
            // }

            // 4. Calculate pagination BEFORE taking the page
            var totalItems = products.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            // CurrentPage = page;

            // 5. Apply pagination and execute query
            Products = products
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }
    }
}