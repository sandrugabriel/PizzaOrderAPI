using Moq;
using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Repository.interfaces;
using PizzaOrderAPI.Orders.Services;
using PizzaOrderAPI.Orders.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Orders.Helpers;

namespace Tests.Orders.UnitTests
{
    public class TestOrderQueryService
    {
        private readonly Mock<IOrderRepository> _mock;
        private readonly IOrderQueryService _orderQueryService;

        public TestOrderQueryService()
        {
            _mock = new Mock<IOrderRepository>();
            _orderQueryService = new OrderQueryService(_mock.Object);

        }
        [Fact]
        public async Task GetAllOrders_ReturnAllOrders()
        {
            var orders = TestOrderFactory.CreateOrders(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);


            var result = await _orderQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(orders[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((OrderResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _orderQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnOrder()
        {
            var order = TestOrderFactory.CreateOrder(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(order);

            var result = await _orderQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(order, result);

        }
    }
}
