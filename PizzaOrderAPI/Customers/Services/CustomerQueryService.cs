using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Repository.interfaces;
using PizzaOrderAPI.Customers.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Customers.Services
{
    public class CustomerQueryService : ICustomerQueryService
    {

        ICustomerRepository _repo;

        public CustomerQueryService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0) return new List<CustomerResponse>();

            return customers;
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

    }
}
