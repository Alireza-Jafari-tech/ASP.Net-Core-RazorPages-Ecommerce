using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<SelectListItem> CategoriesList { get; set; }


        public void OnGet()
        {
            CategoriesList = _context.Categories
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                CategoriesList = _context.Categories
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                    .ToList();

                return Page();
            }

            _context.Products.Add(Product);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}