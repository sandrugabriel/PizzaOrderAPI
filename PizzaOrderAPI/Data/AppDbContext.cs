using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.Property(s => s.Email).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedEmail).HasMaxLength(256);
                entity.Property(s => s.UserName).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedUserName).HasMaxLength(256);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(256);

                entity.HasDiscriminator<string>("Discriminator").HasValue("Customer");

            });


            modelBuilder.Entity<OrderDetails>()
               .HasOne(a => a.Pizza)
               .WithMany(s => s.OrdersDetails)
               .HasForeignKey(a => a.PizzaId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetails>()
               .HasOne(a => a.Order)
               .WithMany(s => s.OrderDetails)
               .HasForeignKey(a => a.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasOne(a => a.Customer)
               .WithMany(s => s.Orders)
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
