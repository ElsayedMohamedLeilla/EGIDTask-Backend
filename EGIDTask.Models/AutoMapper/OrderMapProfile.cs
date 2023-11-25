using AutoMapper;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Models.Dtos.Orders;

namespace EGIDTask.Models.AutoMapper
{
    public class SchedulePlansMapProfile : Profile
    {
        public SchedulePlansMapProfile()
        {
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<StockDTO, Stock>().ReverseMap();
        }
    }
}
