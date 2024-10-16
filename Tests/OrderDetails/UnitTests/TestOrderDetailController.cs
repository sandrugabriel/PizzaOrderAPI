﻿using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.OrdersDetails.Controllers.interfaces;
using PizzaOrderAPI.OrdersDetails.Controllers;
using PizzaOrderAPI.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.OrderDetails.Helpers;
using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.System.Constants;
using Moq;
using PizzaOrderAPI.OrdersDetails.Services.interfaces;
using PizzaOrderAPI.OrdersDetails.Dto;

namespace Tests.OrderDetails.UnitTests
{
    public class TestOrderDetailController
    {

        private readonly Mock<IOrderDetailsQueryService> _mockQueryService;
        private readonly ControllerAPIOrderDetails orderDetailApiController;

        public TestOrderDetailController()
        {
            _mockQueryService = new Mock<IOrderDetailsQueryService>();

            orderDetailApiController = new ControllerOrderDetails(_mockQueryService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await orderDetailApiController.GetOrderDetails();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var orderDetails = TestOrderDetailFactory.CreateOrderDetails(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderDetails);

            var result = await orderDetailApiController.GetOrderDetails();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allOrderDetails = Assert.IsType<List<OrderDetailsResponse>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await orderDetailApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var orderDetails = TestOrderDetailFactory.CreateOrderDetail(1);
            _mockQueryService.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(orderDetails);

            var result = await orderDetailApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allOrderDetails = Assert.IsType<OrderDetailsResponse>(okResult.Value);

            Assert.Equal(1, allOrderDetails.Id);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
