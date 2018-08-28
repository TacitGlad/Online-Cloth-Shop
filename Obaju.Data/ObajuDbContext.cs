using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Obaju.Models;

namespace Obaju.Data
{
    public class ObajuDbContext : IdentityDbContext<User>
    {
        public ObajuDbContext(DbContextOptions<ObajuDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Issues> Issues { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<UserWishlist> UsersWishlist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscriber>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Many-to-many / One order has many products <-> one product applies to many orders
            builder.Entity<OrderProduct>()
               .HasKey(op => new { op.OrderId, op.ProductId });

            builder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(op => op.OrderId);

            builder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(op => op.ProductId);

            // Many-to-many / One user has many products in their wishlist <-> one product applies to many user's wishlists
            builder.Entity<UserWishlist>()
               .HasKey(w => new { w.UserId, w.ProductId });

            builder.Entity<UserWishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlist)
                .HasForeignKey(w => w.UserId);

            builder.Entity<UserWishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.Users)
                .HasForeignKey(w => w.ProductId);
        }
    }
}
