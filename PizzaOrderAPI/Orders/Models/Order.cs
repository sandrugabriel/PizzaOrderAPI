using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.OrdersDetails.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaOrderAPI.Orders.Models
{
    public class Order
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [Required]
        public double Ammount { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }



    }
}
