using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }


        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Customers.Add(Customer);
            _context.SaveChanges();

            var customer = _context.Customers
                .FirstOrDefault(f => f.Name == Customer.Name && f.Email == Customer.Email && f.Password == Customer.Password);

            var newShoppingCard = new ShoppingCart()
            {
                CustomerId = customer.Id
            };

                _context.ShoppingCarts.Add(newShoppingCard);
                _context.SaveChanges();
                customer.ShoppingCart = newShoppingCard;
                _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}