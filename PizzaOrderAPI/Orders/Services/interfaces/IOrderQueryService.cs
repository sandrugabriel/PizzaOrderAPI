using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;

namespace PizzaOrderAPI.Orders.Services.interfaces
{
    public interface IOrderQueryService
    {
        Task<List<OrderResponse>> GetAllAsync();

        Task<OrderResponse> GetByIdAsync(int id);

        Task<Order> GetById(int id);

    }
}
