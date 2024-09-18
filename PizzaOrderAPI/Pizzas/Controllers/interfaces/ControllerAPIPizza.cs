using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Pizzas.Dto;

namespace PizzaOrderAPI.Pizzas.Controllers.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIPizza : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<PizzaResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<PizzaResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(PizzaResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<PizzaResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(PizzaResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<PizzaResponse>> GetByName([FromQuery] string name);

        [HttpPost("CreatePizza")]
        [ProducesResponseType(statusCode: 201, type: typeof(PizzaResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<PizzaResponse>> CreatePizza([FromBody] CreatePizzaRequest createRequestPizza);

        [HttpPut("UpdatePizza")]
        [ProducesResponseType(statusCode: 200, type: typeof(PizzaResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<PizzaResponse>> UpdatePizza([FromQuery] int id, [FromBody] UpdatePizzaRequest updateRequest);

        [HttpDelete("DeletePizza")]
        [ProducesResponseType(statusCode: 200, type: typeof(PizzaResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<PizzaResponse>> DeletePizza([FromQuery] int id);

    }
}
