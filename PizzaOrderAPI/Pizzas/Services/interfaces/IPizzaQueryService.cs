using PizzaOrderAPI.Pizzas.Dto;

namespace PizzaOrderAPI.Pizzas.Services.interfaces
{
    public interface IPizzaQueryService
    {
        Task<List<PizzaResponse>> GetAllAsync();

        Task<PizzaResponse> GetByIdAsync(int id);

        Task<PizzaResponse> GetByNameAsync(string name);
    }
}
