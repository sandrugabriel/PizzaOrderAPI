using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.Orders.Repository;
using PizzaOrderAPI.OrdersDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.OrderDetails.Helpers
{
    public class TestOrderDetailFactory
    {
        public static OrderDetailsResponse CreateOrderDetail(int id)
        {
            OrderDetailsResponse orderDetailsResponse = new OrderDetailsResponse();
            orderDetailsResponse.Id = id;
            orderDetailsResponse.PizzaId = id ;
            orderDetailsResponse.Price = 100;
            return orderDetailsResponse;
            
        }

        public static List<OrderDetailsResponse> CreateOrderDetails(int cout)
        {

            List<OrderDetailsResponse> orderResponses = new List<OrderDetailsResponse>();

            for (int i = 0; i < cout; i++)
            {
                orderResponses.Add(CreateOrderDetail(i));
            }

            return orderResponses;
        }
    }
}
