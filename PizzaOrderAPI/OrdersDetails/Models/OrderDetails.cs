using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PizzaOrderAPI.Pizzas.Models;
using PizzaOrderAPI.Orders.Models;

namespace PizzaOrderAPI.OrdersDetails.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [ForeignKey("PizzaId")]
        public int PizzaId { get; set; }

        [JsonIgnore]
        public virtual Pizza Pizza { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
