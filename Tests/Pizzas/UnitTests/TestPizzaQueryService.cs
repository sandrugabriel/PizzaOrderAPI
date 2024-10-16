using Moq;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Repository.interfaces;
using PizzaOrderAPI.Pizzas.Services;
using PizzaOrderAPI.Pizzas.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Pizzas.Helpers;

namespace Tests.Pizzas.UnitTests
{
    public class TestPizzaQueryService
    {
        private readonly Mock<IPizzaRepository> _mock;
        private readonly IPizzaQueryService _queryPizzaPizza;

        public TestPizzaQueryService()
        {
            _mock = new Mock<IPizzaRepository>();
            _queryPizzaPizza = new PizzaQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAllPizza_ReturnPizza()
        {
            var pizzas = TestPizzaFactory.CreatePizzas(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pizzas);

            var result = await _queryPizzaPizza.GetAllAsync();

            Assert.Equal(5, result.Count);

        }

        [Fact]
        public async Task GetByIdPizza_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((PizzaResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryPizzaPizza.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdPizza_ReturnPizza()
        {
            var pizza = TestPizzaFactory.CreatePizza(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(pizza);

            var result = await _queryPizzaPizza.GetByIdAsync(1);

            Assert.Equal("test1", result.Name);

        }


        [Fact]
        public async Task GetByNamePizza_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((PizzaResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryPizzaPizza.GetByNameAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByNamePizza_ReturnPizza()
        {
            var pizza = TestPizzaFactory.CreatePizza(1);
            _mock.Setup(repo => repo.GetByNameAsync("test1")).ReturnsAsync(pizza);

            var result = await _queryPizzaPizza.GetByNameAsync("test1");

            Assert.Equal("test1", result.Name);

        }
    }
}
