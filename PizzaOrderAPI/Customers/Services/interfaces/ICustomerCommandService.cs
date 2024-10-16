using PizzaOrderAPI.Customers.Dto;

namespace PizzaOrderAPI.Customers.Services.interfaces
{
    public interface ICustomerCommandService
    {

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddProductToOrder(int idCurtomer, string name, int quantity);

        Task<CustomerResponse> DeleteOrder(int idCustomer, int idOrder);

        Task<CustomerResponse> DeleteProductToOrder(int idCurtomer, string name);

    }
}
