using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;

namespace PizzaOrderAPI.Orders.Repository.interfaces
{
    public interface IOrderRepository
    {

        Task<List<OrderResponse>> GetAllAsync();

        Task<OrderResponse> GetByIdAsync(int id);

        Task<Order> GetById(int id);
    }
}
