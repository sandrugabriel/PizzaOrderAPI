using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.Orders.Models;

namespace PizzaOrderAPI.Customers.Models
{
    public class Customer : IdentityUser<int>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Order> Orders { get; set; }

    }
}
