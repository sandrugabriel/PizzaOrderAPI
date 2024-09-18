using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public virtual DbSet<Pizza> Pizzas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

    }
}
