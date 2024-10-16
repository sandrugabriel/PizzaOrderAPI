using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Customers.Repository.interfaces
{
    public interface ICustomerRepository
    {

        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<Customer> GetById(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddProductToOrder(int idCurtomer, string name, int quantity);

        Task<CustomerResponse> DeleteOrder(int idCustomer, int idOrder);

        Task<CustomerResponse> DeleteProductToOrder(int idCurtomer, string name);
       

    }
}
