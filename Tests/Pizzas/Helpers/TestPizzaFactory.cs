using PizzaOrderAPI.Pizzas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Pizzas.Helpers
{
    public class TestPizzaFactory
    {
        public static PizzaResponse CreatePizza(int id)
        {
            return new PizzaResponse
            {
                Name = "test" + id,
                Price = id * 10,
                Type = "Asdasd"
            };
        }

        public static List<PizzaResponse> CreatePizzas(int count)
        {
            var pizzas = new List<PizzaResponse>();

            for (int i = 0; i < count; i++)
            {
                pizzas.Add(CreatePizza(i));
            }

            return pizzas;
        }
    }
}
