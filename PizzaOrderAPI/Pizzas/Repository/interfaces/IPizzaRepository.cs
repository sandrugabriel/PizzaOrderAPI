using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Pizzas.Repository.interfaces
{
    public interface IPizzaRepository
    {
         Task<List<PizzaResponse>> GetAllAsync();

         Task<PizzaResponse> GetByIdAsync(int id);

         Task<Pizza> GetById(int id);

         Task<PizzaResponse> GetByNameAsync(string name);

         Task<PizzaResponse> CreatePizza(CreatePizzaRequest createRequest);

         Task<PizzaResponse> UpdatePizza(int id, UpdatePizzaRequest updateRequest);

         Task<PizzaResponse> DeletePizza(int id);
    }
}
