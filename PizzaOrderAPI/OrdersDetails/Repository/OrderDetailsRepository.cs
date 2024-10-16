using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;
using PizzaOrderAPI.OrdersDetails.Repository.interfaces;

namespace PizzaOrderAPI.OrdersDetails.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public OrderDetailsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<OrderDetailsResponse>> GetAllAsync()
        {
            List<OrderDetails> orderDetails = await _context.OrderDetails.ToListAsync();

            return _mapper.Map<List<OrderDetailsResponse>>(orderDetails);

        }

        public async Task<OrderDetailsResponse> GetByIdAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(s => s.Id == id);
            return _mapper.Map<OrderDetailsResponse>(orderDetail);
        }

        public async Task<OrderDetails> GetById(int id)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(s => s.Id == id);

        }


    }
}
