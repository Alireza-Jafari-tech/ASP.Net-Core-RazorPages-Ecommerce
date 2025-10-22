using Microsoft.EntityFrameworkCore;
using project.Model;

namespace project.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Define composite key
      modelBuilder.Entity<OrderItem>()
          .HasKey(oi => new { oi.OrderId, oi.ProductId });

      // Define relationships
      modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Order)
          .WithMany(o => o.OrderItems)
          .HasForeignKey(oi => oi.OrderId);

      modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Product)
          .WithMany(p => p.OrderItems)
          .HasForeignKey(oi => oi.ProductId);
    }
  }
}