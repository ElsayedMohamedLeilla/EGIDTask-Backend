using EGIDTask.Models.Dtos.Orders;

namespace EGIDTask.Contract.BusinessValidation.Orders
{
    public interface IOrderBLValidation
    {
        Task<bool> CreateOrderValidation(CreateOrderModel model);
    }
}
