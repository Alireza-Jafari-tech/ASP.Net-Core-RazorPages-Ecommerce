using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Categories.Add(Category);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}
