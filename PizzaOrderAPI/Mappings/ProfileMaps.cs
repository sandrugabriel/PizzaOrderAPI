using AutoMapper;
using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Mappings
{
    public class ProfileMaps : Profile
    {
        public ProfileMaps()
        {

            CreateMap<CreatePizzaRequest, Pizza>();
            CreateMap<Pizza, PizzaResponse>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<OrderDetails, OrderDetailsResponse>();
            CreateMap<Order, OrderResponse>();


        }
    }
}
