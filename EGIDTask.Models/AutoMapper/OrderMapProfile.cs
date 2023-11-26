using AutoMapper;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Models.Dtos.Orders;

namespace EGIDTask.Models.AutoMapper
{
    public class OrderMapProfile : Profile
    {
        public OrderMapProfile()
        {
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<StockDTO, Stock>().ReverseMap();

            CreateMap<CreateOrderModel, Order>().ReverseMap();
        }
    }
}
