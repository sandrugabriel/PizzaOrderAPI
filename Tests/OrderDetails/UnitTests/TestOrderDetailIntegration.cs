using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PizzaOrderAPI.Customers.Dto;
using Tests.Infrastucture;
using Tests.OrderDetails.Helpers;

namespace Tests.OrderDetails.UnitTests
{
    public class TestOrderDetailIntegration : IClassFixture<ApiWebApplicationFactory>
    {



        private readonly HttpClient _client;

        public TestOrderDetailIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllOrderDetails_OrderDetailsFound_ReturnsOkStatusCode()
        {
            
            var createCustomer = new CreateCustomerRequest {Username = "test123",Name = "test",Email = "asda@gma.com",Password = "aASda123@",PhoneNumber = "0777777777"};
            var contentCustomer = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");
            var responseCustomer = await _client.PostAsync("/api/v1/ControllerCustomer/CreateCustomer",contentCustomer);
            var responseCustomerString = await responseCustomer.Content.ReadAsStringAsync();
            var resultCustomer = JsonConvert.DeserializeObject<CustomerResponse>(responseCustomerString);
            string token = resultCustomer.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var createOrderDetailRequest = TestOrderDetailFactory.CreateOrderDetail(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOrderDetailRequest), Encoding.UTF8, "application/json");
            await _client.GetAsync("/api/v1/ControllerOrderDetails/all");

            var response = await _client.GetAsync("/api/v1/ControllerOrderDetails/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllOrderDetails_OrderDetailsFound_ReturnsNotFoundStatusCode()
        {
            
            
            var createCustomer = new CreateCustomerRequest {Username = "test123",Name = "test",Email = "asda@gma.com",Password = "aASda123@",PhoneNumber = "0777777777"};
            var contentCustomer = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");
            var responseCustomer = await _client.PostAsync("/api/v1/ControllerCustomer/CreateCustomer",contentCustomer);
            var responseCustomerString = await responseCustomer.Content.ReadAsStringAsync();
            var resultCustomer = JsonConvert.DeserializeObject<CustomerResponse>(responseCustomerString);
            string token = resultCustomer.Token;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client.GetAsync("/api/v1/ControllerOrderDetails/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderDetailById_OrderDetailNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOrderDetails/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
