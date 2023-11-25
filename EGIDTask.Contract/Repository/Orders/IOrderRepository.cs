using EGIDTask.Data;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Models.Criteria.Orders;

namespace EGIDTask.Contract.Repository.Orders
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> GetAsQueryable(GetOrdersCriteria criteria);
    }
}
