using EGIDTask.Data;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Models.Criteria.Orders;

namespace EGIDTask.Contract.Repository.Orders
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        IQueryable<Stock> GetAsQueryable(GetStocksCriteria criteria);
    }
}
