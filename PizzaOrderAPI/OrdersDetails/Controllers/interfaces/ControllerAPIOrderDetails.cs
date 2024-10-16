using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Models;

namespace PizzaOrderAPI.OrdersDetails.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOrderDetails : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<OrderDetails>))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<List<OrderDetailsResponse>>> GetOrderDetails();


        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(OrderDetails))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<OrderDetailsResponse>> GetById([FromQuery] int idOrder);



    }
}
