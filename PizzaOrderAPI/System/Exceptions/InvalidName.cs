﻿namespace PizzaOrderAPI.System.Exceptions
{
    public class InvalidName : Exception
    {
        public InvalidName(string? message):base(message) { }
    }
}
