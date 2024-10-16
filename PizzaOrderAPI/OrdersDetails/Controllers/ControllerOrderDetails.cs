using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.OrdersDetails.Controllers.interfaces;
using PizzaOrderAPI.OrdersDetails.Dto;
using PizzaOrderAPI.OrdersDetails.Services.interfaces;
using PizzaOrderAPI.System.Exceptions;

namespace PizzaOrderAPI.OrdersDetails.Controllers
{
    public class ControllerOrderDetails : ControllerAPIOrderDetails
    {

        IOrderDetailsQueryService _queryServiceOrderDetail;

        public ControllerOrderDetails(IOrderDetailsQueryService queryServiceOrderDetail)
        {
            _queryServiceOrderDetail = queryServiceOrderDetail;
        }

        [Authorize]
        public override async Task<ActionResult<List<OrderDetailsResponse>>> GetOrderDetails()
        {
            try
            {
                var orderDetail = await _queryServiceOrderDetail.GetAllAsync();
                return Ok(orderDetail);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<OrderDetailsResponse>> GetById([FromQuery] int idOrderDetail)
        {
            try
            {
                var orderDetail = await _queryServiceOrderDetail.GetByIdAsync(idOrderDetail);

                return Ok(orderDetail);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}
