using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Repository.interfaces;
using PizzaOrderAPI.Customers.Services.interfaces;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.Orders.Repository.interfaces;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Customers.Services
{
    public class CustomerCommandService : ICustomerCommandService
    {


        ICustomerRepository _repo;
        IOrderRepository _repoOrder;


        public CustomerCommandService(ICustomerRepository repo, IOrderRepository repoOrder)
        {
            _repo = repo;
            _repoOrder = repoOrder;
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {
            if (createRequest.Name.Equals("") || createRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            var customer = await _repo.CreateCustomer(createRequest);

            return customer;
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (updateRequest.Name.Equals("") || updateRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            customer = await _repo.UpdateCustomer(id, updateRequest);
            return customer;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeleteCustomer(id);

            return customer;
        }

        public async Task<CustomerResponse> AddProductToOrder(int idCurtomer, string name, int quantity)
        {

            var customer = await _repo.AddProductToOrder(idCurtomer, name, quantity);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (name.Equals(""))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            return customer;
        }

        public async Task<CustomerResponse> DeleteOrder(int idCustomer, int idOrder)
        {
            var customer = await _repo.GetByIdAsync(idCustomer);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var order = await _repoOrder.GetByIdAsync(idOrder);
            if (order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            await _repo.DeleteOrder(idCustomer, idOrder);
            return customer;
        }

        public async Task<CustomerResponse> DeleteProductToOrder(int idCurtomer, string name)
        {
            var customer = await _repo.DeleteProductToOrder(idCurtomer, name);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;

        }

    }
}
