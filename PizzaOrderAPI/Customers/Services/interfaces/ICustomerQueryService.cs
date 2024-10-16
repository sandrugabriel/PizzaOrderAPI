using PizzaOrderAPI.Customers.Dto;

namespace PizzaOrderAPI.Customers.Services.interfaces
{
    public interface ICustomerQueryService
    {

        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

    }
}
