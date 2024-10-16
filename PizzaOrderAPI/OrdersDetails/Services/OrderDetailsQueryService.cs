using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.OrdersDetails.Repository.interfaces;
using PizzaOrderAPI.OrdersDetails.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.OrdersDetails.Services
{
    public class OrderDetailsQueryService: IOrderDetailsQueryService
    {


        IOrderDetailsRepository _repo;

        public OrderDetailsQueryService(IOrderDetailsRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OrderDetailsResponse>> GetAllAsync()
        {
            var orderDetails = await _repo.GetAllAsync();

            if (orderDetails == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return orderDetails;
        }
        public async Task<OrderDetailsResponse> GetByIdAsync(int id)
        {
            var orderDetail = await _repo.GetByIdAsync(id);

            if (orderDetail == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return orderDetail;
        }

        public async Task<OrderDetails> GetById(int id)
        {
            var orderDetail = await _repo.GetById(id);

            if (orderDetail == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            return orderDetail;
        }


    }
}
