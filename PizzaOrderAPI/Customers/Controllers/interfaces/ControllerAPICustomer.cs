using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Customers.Dto;
using LoginRequest = PizzaOrderAPI.Customers.Dto.LoginRequest;

namespace PizzaOrderAPI.Customers.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPICustomer : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<CustomerResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<CustomerResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CustomerResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CustomerResponse>> GetByName([FromQuery] string name);

        [HttpPost("CreateCustomer")]
        [ProducesResponseType(statusCode: 201, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> RegisterCustomer([FromBody] CreateCustomerRequest createRequestCustomer);

        [HttpPost("LoginCustomer")]
        [ProducesResponseType(statusCode: 201, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> LoginCustomer([FromBody] LoginRequest request);


        [HttpPut("UpdateCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> UpdateCustomer([FromQuery] int id, [FromBody] UpdateCustomerRequest updateRequest);

        [HttpDelete("DeleteCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> DeleteCustomer([FromQuery] int id);


        [HttpPost("AddProductToOrder")]
        [ProducesResponseType(statusCode: 201, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> AddProductToOrder([FromQuery] int idCurtomer, [FromQuery] string name, [FromQuery] int quantity);

        [HttpDelete("DeleteOrder")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> DeleteOrder([FromQuery] int id, [FromQuery] int idOrder);

        [HttpDelete("DeleteProductToOrder")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> DeleteProductToOrder([FromQuery] int id, [FromQuery] string name);

    }
}
