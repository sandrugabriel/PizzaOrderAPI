using AutoMapper;
using PizzaOrderAPI.Pizzas.Dto;
using PizzaOrderAPI.Pizzas.Models;

namespace PizzaOrderAPI.Mappings
{
    public class ProfileMaps : Profile
    {
        public ProfileMaps()
        {

            CreateMap<CreatePizzaRequest, Pizza>();
            CreateMap<Pizza, PizzaResponse>();

        }
    }
}
