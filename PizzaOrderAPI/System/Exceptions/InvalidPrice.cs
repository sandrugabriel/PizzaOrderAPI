﻿namespace PizzaOrderAPI.System.Exceptions
{
    public class InvalidPrice : Exception
    {
        public InvalidPrice(string? message): base(message) { }
    }
}
