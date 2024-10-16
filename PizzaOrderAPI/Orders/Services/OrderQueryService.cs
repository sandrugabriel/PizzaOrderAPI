using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.Orders.Repository.interfaces;
using PizzaOrderAPI.Orders.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Orders.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        IOrderRepository _repo;

        public OrderQueryService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();

            if (orders == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _repo.GetById(id);

            if (order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return order;
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return order;
        }
    }
}
