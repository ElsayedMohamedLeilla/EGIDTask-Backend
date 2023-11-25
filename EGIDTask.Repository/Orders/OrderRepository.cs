using EGIDTask.Contract.Repository.Orders;
using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Domain.Entities.Orders;
using EGIDTask.Models.Criteria.Orders;
using LinqKit;

namespace EGIDTask.Repository.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork<ApplicationDBContext> unitOfWork) : base(unitOfWork)
        {

        }
        public IQueryable<Order> GetAsQueryable(GetOrdersCriteria criteria)
        {
            var predicate = PredicateBuilder.New<Order>(a => !a.IsDeleted);
            var inner = PredicateBuilder.New<Order>(true);

            if (!string.IsNullOrWhiteSpace(criteria.FreeText))
            {
                criteria.FreeText = criteria.FreeText.ToLower().Trim();
                inner = inner.And(x => x.Stock != null && x.Stock.Name.ToLower().Trim().Contains(criteria.FreeText));
                inner = inner.Or(x => x.PersonName.ToLower().Trim().Contains(criteria.FreeText));
                if (int.TryParse(criteria.FreeText, out int id))
                {
                    criteria.Id = id;
                }
            }
            if (criteria.Ids != null && criteria.Ids.Count > 0)
            {
                predicate = predicate.And(e => criteria.Ids.Contains(e.Id));
            }
            if (criteria.IsActive != null)
            {
                predicate = predicate.And(e => e.IsActive == criteria.IsActive);
            }

            predicate = predicate.And(inner);
            var Query = Get(predicate);
            return Query;

        }
    }
}
