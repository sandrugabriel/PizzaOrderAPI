using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.Pizzas.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaOrderAPI.OrdersDetails.Dto
{
    public class OrderDetailsResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
