using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Orders.Controllers.interfaces;
using PizzaOrderAPI.Orders.Dto;
using PizzaOrderAPI.Orders.Services.interfaces;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.Orders.Controllers
{
    public class ControllerOrder : ControllerAPIOrder
    {


        IOrderQueryService _queryServiceOrder;

        public ControllerOrder(IOrderQueryService queryServiceOrder)
        {
            _queryServiceOrder = queryServiceOrder;
        }

        [Authorize]
        public override async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            try
            {
                var order = await _queryServiceOrder.GetAllAsync();
                return Ok(order);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<OrderResponse>> GetById([FromQuery] int idOrder)
        {
            try
            {
                OrderResponse order = await _queryServiceOrder.GetByIdAsync(idOrder);
                return Ok(order);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
