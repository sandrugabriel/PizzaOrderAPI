using Moq;
using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Repository.interfaces;
using PizzaOrderAPI.OrdersDetails.Services;
using PizzaOrderAPI.OrdersDetails.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.OrderDetails.Helpers;

namespace Tests.OrderDetails.UnitTests
{
    public class TestOrderDetailQueryService
    {
        private readonly Mock<IOrderDetailsRepository> _mock;
        private readonly IOrderDetailsQueryService _orderDetailQueryService;

        public TestOrderDetailQueryService()
        {
            _mock = new Mock<IOrderDetailsRepository>();
            _orderDetailQueryService = new OrderDetailsQueryService(_mock.Object);

        }
        [Fact]
        public async Task GetAllOrderDetails_ReturnAllOrderDetails()
        {
            var orderDetails = TestOrderDetailFactory.CreateOrderDetails(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderDetails);


            var result = await _orderDetailQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(orderDetails[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((OrderDetailsResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _orderDetailQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnOrderDetail()
        {
            var orderDetail = TestOrderDetailFactory.CreateOrderDetail(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(orderDetail);

            var result = await _orderDetailQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(orderDetail, result);

        }

    }
}
