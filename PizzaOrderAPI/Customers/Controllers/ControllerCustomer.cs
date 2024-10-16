﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PizzaOrderAPI.Customers.Controllers.interfaces;
using PizzaOrderAPI.Customers.Dto;
using PizzaOrderAPI.Customers.Models;
using PizzaOrderAPI.Customers.Services.interfaces;
using PizzaOrderAPI.System.Constants;
using PizzaOrderAPI.System.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PizzaOrderAPI.Customers.Controllers
{
    public class ControllerCustomer : ControllerAPICustomer
    {

        ICustomerQueryService _query;
        ICustomerCommandService _command;
        UserManager<Customer> userManager;
        SignInManager<Customer> signInManager;
        IConfiguration configuration;
        private IMapper _mapper;
        public ControllerCustomer(IMapper maper, UserManager<Customer> userManager, SignInManager<Customer> signInManager, IConfiguration configuration, ICustomerQueryService query, ICustomerCommandService command)
        {
            _query = query;
            _command = command;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            _mapper = maper;
        }


        public override async Task<ActionResult<CustomerResponse>> LoginCustomer([FromBody] LoginRequest request)
        {

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = GenerateToken();
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        public override async Task<ActionResult<CustomerResponse>> RegisterCustomer([FromBody] CreateCustomerRequest createRequestCustomer)
        {
            try
            {

                var customer = new Customer
                {
                    UserName = createRequestCustomer.Username,
                    Name = createRequestCustomer.Name,
                    Email = createRequestCustomer.Email,
                    PhoneNumber = createRequestCustomer.PhoneNumber
                };

                if (createRequestCustomer.Name.Length <= 1) throw new InvalidName(Constants.InvalidName);

                var result = await userManager.CreateAsync(customer, createRequestCustomer.Password);

                var customerResponse = _mapper.Map<CustomerResponse>(customer);
                if (result.Succeeded)
                {
                    var token = GenerateToken();
                    customerResponse.Token = token;
                    return Ok(customerResponse);
                }

                return BadRequest(result.Errors);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<List<CustomerResponse>>> GetAll()
        {
            try
            {
                var customers = await _query.GetAllAsync();
                return Ok(customers);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CustomerResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var customers = await _query.GetByIdAsync(id);
                return Ok(customers);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<CustomerResponse>> GetByName([FromQuery] string name)
        {
            try
            {
                var customers = await _query.GetByNameAsync(name);
                return Ok(customers);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize]
        public override async Task<ActionResult<CustomerResponse>> UpdateCustomer([FromQuery] int id, [FromBody] UpdateCustomerRequest updateRequest)
        {
            try
            {
                var customer = await _command.UpdateCustomer(id, updateRequest);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<CustomerResponse>> DeleteCustomer([FromQuery] int id)
        {
            try
            {
                var customer = await _command.DeleteCustomer(id);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        private string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public override async Task<ActionResult<CustomerResponse>> AddProductToOrder([FromQuery] int idCurtomer, [FromQuery] string name, [FromQuery] int quantity)
        {

            try
            {
                var customer = await _command.AddProductToOrder(idCurtomer, name, quantity);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> DeleteOrder([FromQuery] int id, [FromQuery] int idOrder)
        {
            try
            {
                var customer = await _command.DeleteOrder(id, idOrder);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> DeleteProductToOrder([FromQuery] int id, [FromQuery] string name)
        {

            try
            {
                var customer = await _command.DeleteProductToOrder(id, name);
                return Ok(customer);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
