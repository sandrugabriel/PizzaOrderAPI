using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Repository.interfaces;
using PizzaOrderAPI.Pizzas.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Pizzas.Services
{
    public class PizzaCommandService : IPizzaCommandService
    {

        IPizzaRepository _repo;

        public PizzaCommandService(IPizzaRepository repo)
        {
            _repo = repo;
        }

        public async Task<PizzaResponse> CreatePizza(CreatePizzaRequest createRequest)
        {
            if (createRequest.Name.Equals("") || createRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            if (createRequest.Price <= 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }

            var pizza = await _repo.CreatePizza(createRequest);

            return pizza;
        }

        public async Task<PizzaResponse> UpdatePizza(int id, UpdatePizzaRequest updateRequest)
        {

            var pizza = await _repo.GetByIdAsync(id);

            if (pizza == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (updateRequest.Price <= 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }

            pizza = await _repo.UpdatePizza(id, updateRequest);
            return pizza;
        }

        public async Task<PizzaResponse> DeletePizza(int id)
        {
            var pizza = await _repo.GetByIdAsync(id);

            if (pizza == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeletePizza(id);

            return pizza;
        }


    }
}
