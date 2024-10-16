﻿namespace PizzaOrderAPI.Customers.Dto
{
    public class CreateCustomerRequest
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
