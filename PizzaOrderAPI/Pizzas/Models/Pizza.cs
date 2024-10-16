using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzaOrderAPI.OrdersDetails.Models;

namespace PizzaOrderAPI.Pizzas.Models
{
    public class Pizza
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<OrderDetails> OrdersDetails { get; set; }

    }
}
