using PizzaOrderAPI.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.OrderDetails.Helpers;

namespace Tests.Orders.Helpers
{
    internal class TestOrderFactory
    {
        public static OrderResponse CreateOrder(int id)
        {
            var factory = TestOrderDetailFactory.CreateOrderDetail(1);
            return new OrderResponse
            {
                Id = id,
                Ammount = 10,
                Status = "open"
            };
        }

        public static List<OrderResponse> CreateOrders(int cout)
        {

            List<OrderResponse> orderViews = new List<OrderResponse>();

            for (int i = 0; i < cout; i++)
            {
                orderViews.Add(CreateOrder(i));
            }

            return orderViews;
        }
    }
}
