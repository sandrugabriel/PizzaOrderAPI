using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Pizzas.Controllers.interfaces;
using PizzaOrderAPI.System.Exceptions;
using PizzaOrderAPI.Pizzas.Services.interfaces;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Pizzas.Controllers
{
    public class ControllerPizza : ControllerAPIPizza
    {

        IPizzaCommandService _command;
        IPizzaQueryService _query;

        public ControllerPizza(IPizzaCommandService command, IPizzaQueryService query)
        {
            _command = command;
            _query = query;
        }

        [Authorize]
        public override async Task<ActionResult<List<PizzaResponse>>> GetAll()
        {
            try
            {
                var option = await _query.GetAllAsync();
                return Ok(option);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize]

        public override async Task<ActionResult<PizzaResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var option = await _query.GetByIdAsync(id);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<PizzaResponse>> GetByName([FromQuery] string name)
        {
            try
            {
                var option = await _query.GetByNameAsync(name);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<PizzaResponse>> CreatePizza([FromBody] CreatePizzaRequest createPizzaRequest)
        {
            try
            {
                var option = await _command.CreatePizza(createPizzaRequest);
                return Ok(option);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<PizzaResponse>> UpdatePizza([FromQuery] int id, [FromBody] UpdatePizzaRequest updatePizzaRequest)
        {
            try
            {
                var option = await _command.UpdatePizza(id, updatePizzaRequest);
                return Ok(option);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<PizzaResponse>> DeletePizza([FromQuery] int id)
        {
            try
            {
                var option = await _command.DeletePizza(id);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
