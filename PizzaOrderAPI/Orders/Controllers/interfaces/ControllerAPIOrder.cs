using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Orders.Dto;

namespace PizzaOrderAPI.Orders.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOrder : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<OrderResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<OrderResponse>>> GetOrders();


        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(OrderResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<OrderResponse>> GetById([FromQuery] int idOrder);


    }
}
