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
    public class TestPizzaCommandService
    {
        private readonly Mock<IPizzaRepository> _mock;

        private readonly IPizzaCommandService _commandPizza;

        public TestPizzaCommandService()
        {
            _mock = new Mock<IPizzaRepository>();
            _commandPizza = new PizzaCommandService(_mock.Object);

        }

        [Fact]
        public async Task CreatePizza_InvalidPrice()
        {
            var createRequest = new CreatePizzaRequest
            {
                Price = 0,
                Name = "adsda",
                
                Type = "asd"
            };


            _mock.Setup(repo => repo.CreatePizza(createRequest)).ReturnsAsync((PizzaResponse)null);
            Exception exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandPizza.CreatePizza(createRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task CreatePizza_ReturnPizza()
        {
            var createRequest = new CreatePizzaRequest
            {
                Price = 10,
                Name = "adsda",
                
                Type = "asd"
            };

            var pizza = TestPizzaFactory.CreatePizza(50);
            pizza.Price = createRequest.Price;

            _mock.Setup(repo => repo.CreatePizza(It.IsAny<CreatePizzaRequest>())).ReturnsAsync(pizza);

            Assert.NotNull(_commandPizza);

            var result = await _commandPizza.CreatePizza(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Price, createRequest.Price);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdatePizzaRequest
            {
                Price = 0,
                Name = "adsda"
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((PizzaResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandPizza.UpdatePizza(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var updateRequest = new UpdatePizzaRequest
            {
                Price = 0,
                Name = "adsda"
            };

            var pizza = TestPizzaFactory.CreatePizza(1);
            pizza.Price = updateRequest.Price.Value;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(pizza);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandPizza.UpdatePizza(1, updateRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnPizza()
        {
            var updateRequest = new UpdatePizzaRequest
            {
                Type = "asd",
                Price = 10,
                Name = "adsda"
            };

            var pizza = TestPizzaFactory.CreatePizza(1);


            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pizza);
            _mock.Setup(repo => repo.UpdatePizza(It.IsAny<int>(), It.IsAny<UpdatePizzaRequest>())).ReturnsAsync(pizza);

            var result = await _commandPizza.UpdatePizza(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(pizza, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeletePizza(It.IsAny<int>())).ReturnsAsync((PizzaResponse)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandPizza.DeletePizza(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var pizza = TestPizzaFactory.CreatePizza(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pizza);

            var restul = await _commandPizza.DeletePizza(1);

            Assert.NotNull(restul);
            Assert.Equal(pizza, restul);
        }

    }
}
