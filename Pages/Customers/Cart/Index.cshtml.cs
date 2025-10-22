using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Customers.Cart
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        
        public ShoppingCart ShoppingCart { get; set; }

        public List<CartItem> CartItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? userId { get; set; }

        public IActionResult OnGet(int userId)
        {
            var customer = _context.Customers
                .Include(c => c.ShoppingCart)
                    .ThenInclude(s => s.CartItems)
                        .ThenInclude(ci => ci.Product)
                .FirstOrDefault(f => f.Id == userId);

            if (customer == null)
                return RedirectToPage("/NotFound");
            
            ShoppingCart = customer.ShoppingCart;
            CartItems = ShoppingCart.CartItems;

            return Page();

            // CartItems = customer.ShoppingCart.CartItems.ToList();

            // CartItems = _context.CartItems
            //                     .Where(ci => ci.ShoppingCart.CustomerId == userId)
            //                     .Include(c => c.ShoppingCart)
            //                         .ThenInclude(t => t.Customer)
            //                     .ToList();
        }

        public void OnPost()
        {
        
        }
    }
}