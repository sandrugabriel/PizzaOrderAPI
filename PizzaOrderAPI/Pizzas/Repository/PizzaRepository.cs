using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;
using PizzaOrderAPI.Pizzas.Repository.interfaces;
using System;

namespace PizzaOrderAPI.Pizzas.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public PizzaRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PizzaResponse>> GetAllAsync()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return _mapper.Map<List<PizzaResponse>>(pizzas);
        }

        public async Task<PizzaResponse> GetByIdAsync(int id)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<PizzaResponse>(pizza);
        }

        public async Task<Pizza> GetById(int id)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(c => c.Id == id);
            return pizza;
        }

        public async Task<PizzaResponse> GetByNameAsync(string name)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<PizzaResponse>(pizza);
        }

        public async Task<PizzaResponse> CreatePizza(CreatePizzaRequest createRequest)
        {

            var pizza = _mapper.Map<Pizza>(createRequest);

            _context.Pizzas.Add(pizza);

            await _context.SaveChangesAsync();

            PizzaResponse pizzaView = _mapper.Map<PizzaResponse>(pizza);

            return pizzaView;
        }
        public async Task<PizzaResponse> UpdatePizza(int id, UpdatePizzaRequest updateRequest)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(s => s.Id == id);
            pizza.Type = updateRequest.Type ?? pizza.Type;
            pizza.Name = updateRequest.Name ?? pizza.Name;
            pizza.Price = updateRequest.Price ?? pizza.Price;
            pizza.Description = updateRequest.Description ?? pizza.Description;

            _context.Pizzas.Update(pizza);

            await _context.SaveChangesAsync();

            PizzaResponse pizzaView = _mapper.Map<PizzaResponse>(pizza);

            return pizzaView;
        }

        public async Task<PizzaResponse> DeletePizza(int id)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(s => s.Id == id);

            _context.Pizzas.Remove(pizza);

            await _context.SaveChangesAsync();

            return _mapper.Map<PizzaResponse>(pizza);
        }

    }
}
