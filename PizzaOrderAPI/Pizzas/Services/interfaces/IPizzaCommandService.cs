using PizzaOrderAPI.Pizzas.Dto;

namespace PizzaOrderAPI.Pizzas.Services.interfaces
{
    public interface IPizzaCommandService
    {
        Task<PizzaResponse> CreatePizza(CreatePizzaRequest createRequest);

        Task<PizzaResponse> UpdatePizza(int id, UpdatePizzaRequest updateRequest);

        Task<PizzaResponse> DeletePizza(int id);
    }
}
