using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;
using Mattger_DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mattger_DAL.Data
{
    public class MattgerDBContext : IdentityDbContext<AppUser>
    {
        public MattgerDBContext(DbContextOptions<MattgerDBContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product → Brand
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductBrand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductBrandId);

            // Product → Type
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.ProductTypeId);

            // Cart → User
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            // CartItem → Cart
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId);

            // CartItem → Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            // Order → User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // OrderItem → Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // OrderItem → Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            //seeding data
            // ========================
            // 5. USERS (Identity Seed)
            // ========================
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "user-1",
                    UserName = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@TEST.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEexamplehash",
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            // ========================
            // 1. PRODUCT BRANDS
            // ========================
            modelBuilder.Entity<ProductBrand>().HasData(
                new ProductBrand { Id = 1, Name = "Apple" },
                new ProductBrand { Id = 2, Name = "Samsung" },
                new ProductBrand { Id = 3, Name = "Sony" }
            );

            // ========================
            // 2. PRODUCT TYPES
            // ========================
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Smartphones" },
                new ProductType { Id = 2, Name = "Laptops" },
                new ProductType { Id = 3, Name = "Headphones" }
            );

            // ========================
            // 3. PRODUCTS
            // ========================
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    Description = "Apple flagship smartphone",
                    Details = "Titanium design, A17 Pro chip, 48MP camera system, 5x zoom telephoto lens, advanced AI processing, all-day battery life, MagSafe support.",
                    Price = 60000,
                    OldPrice = 65000,
                    Discount = 8,
                    StockQuantity = 20,
                    ProductBrandId = 1,
                    ProductTypeId = 1,
                    Rating = 4.9m,
                    CreatedAt = new DateTime(2024, 01, 01)
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S24 Ultra",
                    Description = "Android premium flagship",
                    Details = "Snapdragon 8 Gen 3, 200MP camera, S-Pen support, AI translation, 120Hz AMOLED display, DeX productivity mode.",
                    Price = 55000,
                    OldPrice = 59000,
                    Discount = 7,
                    StockQuantity = 25,
                    ProductBrandId = 2,
                    ProductTypeId = 1,
                    Rating = 4.8m,
                    CreatedAt = new DateTime(2024, 01, 01)
                },
                new Product
                {
                    Id = 3,
                    Name = "MacBook Pro M3 14-inch",
                    Description = "Professional laptop",
                    Details = "Apple M3 chip, Liquid Retina XDR display, ultra-fast SSD, 22-hour battery, optimized for developers and creators.",
                    Price = 90000,
                    OldPrice = 95000,
                    Discount = 5,
                    StockQuantity = 10,
                    ProductBrandId = 1,
                    ProductTypeId = 2,
                    Rating = 5.0m,
                    CreatedAt = new DateTime(2024, 01, 01)
                },
                new Product
                {
                    Id = 4,
                    Name = "Sony WH-1000XM5",
                    Description = "Noise cancelling headphones",
                    Details = "Industry-leading noise cancellation, 30-hour battery, crystal clear sound, adaptive ambient mode, fast charging.",
                    Price = 15000,
                    OldPrice = 17000,
                    Discount = 12,
                    StockQuantity = 35,
                    ProductBrandId = 3,
                    ProductTypeId = 3,
                    Rating = 4.7m,
                    CreatedAt = new DateTime(2024, 01, 01)
                }
            );

            // ========================
            // 4. COUPONS
            // ========================
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "WELCOME10",
                    DiscountType = DiscountType.Precent,
                    Discount = 10,
                    StartDate = new DateTime(2024, 01, 01),
                    EndDate = new DateTime(2026, 01, 01)
                },
                new Coupon
                {
                    Id = 2,
                    Code = "SAVE500",
                    DiscountType = DiscountType.Value,
                    Discount = 500,
                    StartDate = new DateTime(2024, 01, 01),
                    EndDate = new DateTime(2026, 01, 01)
                }
            );


            // ========================
            // 6. CARTS
            // ========================
            modelBuilder.Entity<Cart>().HasData(
                new Cart
                {
                    Id = 1,
                    UserId = "user-1"
                }
            );

            // ========================
            // 7. CART ITEMS
            // ========================
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem
                {
                    Id = 1,
                    CartId = 1,
                    ProductId = 1,
                    Quantity = 1
                },
                new CartItem
                {
                    Id = 2,
                    CartId = 1,
                    ProductId = 2,
                    Quantity = 2
                }
            );

            // ========================
            // 8. WISHLIST
            // ========================
            modelBuilder.Entity<Wishlist>().HasData(
                new Wishlist
                {
                    Id = 1,
                    UserId = "user-1"
                }
            );

            modelBuilder.Entity<WishlistItem>().HasData(
                new WishlistItem
                {
                    Id = 1,
                    WishlistId = 1,
                    ProductId = 3
                },
                new WishlistItem
                {
                    Id = 2,
                    WishlistId = 1,
                    ProductId = 4
                }
            );
        }
    }
    
}
