using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaOrderAPI.Pizzas.Controllers;
using PizzaOrderAPI.Pizzas.Controllers.interfaces;
using PizzaOrderAPI.Pizzas.Dto;
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
    public class TestControllerPizza
    {
        private readonly Mock<IPizzaCommandService> _mockCommandPizza;
        private readonly Mock<IPizzaQueryService> _mockQueryPizza;
        private readonly ControllerAPIPizza pizzaApiController;

        public TestControllerPizza()
        {
            _mockCommandPizza = new Mock<IPizzaCommandService>();
            _mockQueryPizza = new Mock<IPizzaQueryService>();

            pizzaApiController = new ControllerPizza( _mockCommandPizza.Object,_mockQueryPizza.Object);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var pizzas = TestPizzaFactory.CreatePizzas(5);
            _mockQueryPizza.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pizzas);

            var result = await pizzaApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allPizzas = Assert.IsType<List<PizzaResponse>>(okResult.Value);

            Assert.Equal(5, allPizzas.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryPizza.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await pizzaApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var pizzas = TestPizzaFactory.CreatePizza(1);
            _mockQueryPizza.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(pizzas);

            var result = await pizzaApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allPizzas = Assert.IsType<PizzaResponse>(okResult.Value);

            Assert.Equal("test1", allPizzas.Name);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetByPrice_ItemsDoNotExist()
        {
            _mockQueryPizza.Setup(repo => repo.GetByNameAsync("10")).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await pizzaApiController.GetByName("10");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetByPrice_ValidData()
        {
            var pizzas = TestPizzaFactory.CreatePizza(1);
            _mockQueryPizza.Setup(repo => repo.GetByNameAsync("test1")).ReturnsAsync(pizzas);

            var result = await pizzaApiController.GetByName("test1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allPizzas = Assert.IsType<PizzaResponse>(okResult.Value);

            Assert.Equal("test1", allPizzas.Name);
            Assert.Equal(200, okResult.StatusCode);

        }

       
        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreatePizzaRequest
            {
                Name = "test",
                Price = 10
            };

            var pizza = TestPizzaFactory.CreatePizza(1);
            pizza.Price = createRequest.Price;

            _mockCommandPizza.Setup(repo => repo.CreatePizza(It.IsAny<CreatePizzaRequest>())).ReturnsAsync(pizza);

            var result = await pizzaApiController.CreatePizza(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, pizza);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdatePizzaRequest
            {
                Name = "test",
                Price = 0
            };


            _mockCommandPizza.Setup(repo => repo.UpdatePizza(1, updateRequest)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await pizzaApiController.UpdatePizza(1, updateRequest);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var updateRequest = new UpdatePizzaRequest
            {
                Name = "test",
                Price = 10
            };

            var pizza = TestPizzaFactory.CreatePizza(1);

            _mockCommandPizza.Setup(repo => repo.UpdatePizza(It.IsAny<int>(), It.IsAny<UpdatePizzaRequest>())).ReturnsAsync(pizza);

            var result = await pizzaApiController.UpdatePizza(1, updateRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, pizza);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandPizza.Setup(repo => repo.DeletePizza(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await pizzaApiController.DeletePizza(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var pizza = TestPizzaFactory.CreatePizza(1);

            _mockCommandPizza.Setup(repo => repo.DeletePizza(It.IsAny<int>())).ReturnsAsync(pizza);

            var result = await pizzaApiController.DeletePizza(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, pizza);

        }

    }
}
