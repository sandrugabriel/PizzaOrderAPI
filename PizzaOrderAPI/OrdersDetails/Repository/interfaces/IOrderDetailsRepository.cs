using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;

namespace PizzaOrderAPI.OrdersDetails.Repository.interfaces
{
    public interface IOrderDetailsRepository
    {

        Task<List<OrderDetailsResponse>> GetAllAsync();

        Task<OrderDetailsResponse> GetByIdAsync(int id);

        Task<OrderDetails> GetById(int id);

    }
}
