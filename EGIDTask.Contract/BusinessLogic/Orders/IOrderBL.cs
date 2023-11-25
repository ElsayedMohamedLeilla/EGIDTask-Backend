using EGIDTask.Models.Criteria.Orders;
using EGIDTask.Models.Dtos.Orders;
using EGIDTask.Models.Response.Orders;

namespace EGIDTask.Contract.BusinessLogic.Orders
{
    public interface IOrderBL
    {
        Task<int> Create(CreateOrderModel model);
        Task<GetOrdersResponse> Get(GetOrdersCriteria model);
    }
}
