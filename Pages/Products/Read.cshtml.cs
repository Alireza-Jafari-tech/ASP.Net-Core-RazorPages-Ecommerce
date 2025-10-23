using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Model;

namespace project.Pages.Products
{
    public class ReadModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReadModel(AppDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public int userId { get; set; }

        public IActionResult OnGet(int id, int? userId)
        {
            Product = _context.Products
                .Include(i => i.Category)
                .FirstOrDefault(f => f.Id == id);

            if (Product == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            int productId = ProductId;

            AddToCart(userId, Quantity, productId);

            return RedirectToPage("/Index", new { UserId = userId });
        }

        public void AddToCart(int userId, int quantity, int productId)
        {
            var user = _context.Customers
                .Include(c => c.ShoppingCart)
                    .ThenInclude(sc => sc.CartItems)
                .FirstOrDefault(f => f.Id == userId);

            // if (user == null)
            //     // user not found

            if (user.ShoppingCart.Id < 1)
            {
                var newShoppingCard = new ShoppingCart()
                {
                    CustomerId = user.Id
                };

                _context.ShoppingCarts.Add(newShoppingCard);
                _context.SaveChanges();
                user.ShoppingCart = newShoppingCard;
            }
            //TODO
            var existingItem = user.ShoppingCart.CartItems.FirstOrDefault(f => f.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }

            var cartItem = new CartItem()
            {
                ShoppingCartId = user.ShoppingCart.Id,
                //TODO
                ProductId = productId,
                Quantity = quantity
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            user.ShoppingCart.CartItems.Add(cartItem);
            _context.SaveChanges();

            user.ShoppingCart.UpdatedDate = DateTime.Now;
        }
    }
}