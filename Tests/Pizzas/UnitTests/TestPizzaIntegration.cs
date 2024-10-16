using Newtonsoft.Json;
using PizzaOrderAPI.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.Infrastucture;
using Tests.Pizzas.Helpers;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;

namespace Tests.Pizzas.UnitTests
{
    public class TestPizzaIntegration
    {

        private readonly HttpClient _client;

        public TestPizzaIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPizzas_PizzasFound_ReturnsOkStatusCode_ValidResponse()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            var createPizzaRequest = TestPizzaFactory.CreatePizza(1);
            content = new StringContent(JsonConvert.SerializeObject(createPizzaRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerPizza/createPizza", content);

            response = await _client.GetAsync("/api/v1/ControllerPizza/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPizzaById_PizzaFound_ReturnsOkStatusCode_ValidResponse()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            var createPizza = new CreatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(createPizza), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("/api/v1/ControllerPizza/CreatePizza", content);
            var resString = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PizzaResponse>(resString);

            response = await _client.GetAsync($"/api/v1/ControllerPizza/FindById?id={result.Id}");
            resString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<PizzaResponse>(resString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createPizza.Name);
        }

        [Fact]
        public async Task GetPizzaById_PizzaNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerPizza/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            request = "/api/v1/ControllerPizza/createPizza";
            var ControllerPizza = new CreatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(ControllerPizza), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Pizza>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerPizza.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {


            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            request = "/api/v1/ControllerPizza/CreatePizza";
            var createPizza = new CreatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(createPizza), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PizzaResponse>(responseString)!;

            request = $"/api/v1/ControllerPizza/UpdatePizza?id={result.Id}";
            var updatePizza = new UpdatePizzaRequest { Price = 100 };
            content = new StringContent(JsonConvert.SerializeObject(updatePizza), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<PizzaResponse>(responseString);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            Assert.Equal(updatePizza.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidPizzaName_ReturnsBadRequestStatusCode()
        {


            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            request = "/api/v1/ControllerPizza/CreatePizza";
            var createPizza = new CreatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(createPizza), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PizzaResponse>(responseString)!;

            request = $"/api/v1/ControllerPizza/UpdatePizza?id={result.Id}";
            var updatePizza = new UpdatePizzaRequest { Name = "" };
            content = new StringContent(JsonConvert.SerializeObject(updatePizza), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result.Name, updatePizza.Name);
        }

        [Fact]
        public async Task Put_Update_PizzaDoesNotExist_ReturnsNotFoundStatusCode()
        {


            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            request = "/api/v1/ControllerPizza/updatePizza";
            var updatePizza = new UpdatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(updatePizza), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_PizzaExists_ReturnsDeletedPizza()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            request = "/api/v1/ControllerPizza/CreatePizza";
            var createPizza = new CreatePizzaRequest { Name = "New Pizza 1", Type = "asdsdf", Price = 100, Description = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(createPizza), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PizzaResponse>(responseString)!;

            request = $"/api/v1/ControllerPizza/DeletePizza?id={result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Delete_Delete_PizzaDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerPizza/DeletePizza/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
