using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Models;
using PizzaOrderAPI.Orders.Repository.interfaces;

namespace PizzaOrderAPI.Orders.Repository
{
    public class OrderRepository : IOrderRepository
    {

        AppDbContext _context;
        IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var orders = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Pizza).ToListAsync();

            return _mapper.Map<List<OrderResponse>>(orders);
        }

        public async Task<Order> GetById(int id)
        {

            return await _context.Orders.Include(s => s.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var order = await _context.Orders.Include(s => s.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<OrderResponse>(order);

        }


    }
}
