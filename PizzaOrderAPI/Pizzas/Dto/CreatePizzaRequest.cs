namespace PizzaOrderAPI.Pizzas.Dto
{
    public class CreatePizzaRequest
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }  

        public string Description { get; set; }
    }
}
