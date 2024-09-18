using System.ComponentModel.DataAnnotations;

namespace PizzaOrderAPI.Pizzas.Dto
{
    public class PizzaResponse
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }
    }
}
