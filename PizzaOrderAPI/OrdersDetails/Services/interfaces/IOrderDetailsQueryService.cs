using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;

namespace PizzaOrderAPI.OrdersDetails.Services.interfaces
{
    public interface IOrderDetailsQueryService
    {
        Task<List<OrderDetailsResponse>> GetAllAsync();

        Task<OrderDetailsResponse> GetByIdAsync(int id);

        Task<OrderDetails> GetById(int id);
    }
}
