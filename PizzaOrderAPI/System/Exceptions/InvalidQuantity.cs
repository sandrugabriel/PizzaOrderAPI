namespace PizzaOrderAPI.System.Exceptions
{
    public class InvalidQuantity : Exception
    {
        public InvalidQuantity(string? message):base(message) { }
    }
}
