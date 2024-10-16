using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.Customers.Repository.interfaces;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.Pizzas.Models;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Customers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        AppDbContext _context;
        IMapper _mapper;

        public CustomerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _context.Customers.Include(a => a.Orders).ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.Include(a => a.Orders).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers.Include(a => a.Orders).FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(a=>a.Orders).FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {

            var customer = _mapper.Map<Customer>(createRequest);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }
        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {
            var customer = await _context.Customers.Include(a => a.Orders).FirstOrDefaultAsync(s => s.Id == id);
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.Name = updateRequest.Name ?? customer.Name;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Include(a => a.Orders).FirstOrDefaultAsync(s => s.Id == id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> AddProductToOrder(int idCurtomer, string name, int quantity)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                 ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Pizza).FirstOrDefaultAsync(s => s.Id == idCurtomer);

            var pizza = await _context.Pizzas.FirstOrDefaultAsync(a=>a.Name == name);

            var order = (Order)null;
            if (customer.Orders != null)
                order = customer.Orders.FirstOrDefault(s => s.Status == "open");

                OrderDetails orderDetail = new OrderDetails();
                orderDetail.Price = quantity * pizza.Price;
                orderDetail.Quantity = quantity;
                orderDetail.PizzaId = pizza.Id;
                orderDetail.Pizza = pizza;

            if(order == null)
            {
                order = new Order();
                order.Status = "open";
                order.CustomerId = idCurtomer;
                order.Customer = customer;
                
            } 
            
                orderDetail.OrderId = order.Id;
                orderDetail.Order = order;
            order.Ammount += orderDetail.Price;
            if(order.OrderDetails == null)
            {
                order.OrderDetails = new List<OrderDetails>() {orderDetail};
            }else
            order.OrderDetails.Add(orderDetail);

            if (customer.Orders == null)
            {
                customer.Orders = new List<Order>() { order};
            }else
                customer.Orders.Add(order);

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();


            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> DeleteOrder(int idCustomer, int idOrder)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                 ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Pizza).FirstOrDefaultAsync(s => s.Id == idCustomer);


            customer.Orders.Remove(customer.Orders.FirstOrDefault(or => or.Id == idOrder));

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> DeleteProductToOrder(int idCurtomer, string name)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                  ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Pizza).FirstOrDefaultAsync(s => s.Id == idCurtomer);

            var order = customer.Orders.FirstOrDefault(s => s.Status == "open");

            if (order == null) return null;

            var orderDetail = (OrderDetails)null;
            Pizza pizza = null;
            foreach (var item in order.OrderDetails)
            {
                if (item.Pizza.Name == name) { orderDetail = item; pizza = item.Pizza; }
            }

            order.Ammount -= orderDetail.Price;

            order.OrderDetails.Remove(orderDetail);

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();


            return _mapper.Map<CustomerResponse>(customer);
        }


    }
}
