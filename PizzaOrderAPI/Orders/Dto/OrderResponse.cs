using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.OrdersDetails.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaOrderAPI.Orders.Dto
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public double Ammount { get; set; }

        public string Status { get; set; }

    }
}
