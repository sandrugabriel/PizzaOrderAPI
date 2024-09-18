using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Repository.interfaces;
using PizzaOrderAPI.Pizzas.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Pizzas.Services
{
    public class PizzaQueryService : IPizzaQueryService
    {

        IPizzaRepository _repo;

        public PizzaQueryService(IPizzaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PizzaResponse>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0) return new List<PizzaResponse>();

            return customers;
        }

        public async Task<PizzaResponse> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

        public async Task<PizzaResponse> GetByNameAsync(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }



    }
}
