using System.Linq.Expressions;

namespace EGIDTask.Data
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string IncludeProperties = "");
        IQueryable<T> GetWithTracking(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string IncludeProperties = "");
        T Insert(T entity);
        IQueryable<T> OrderBy(IQueryable<T> query, string orderColumn = "", string orderType = "");
        void SetEntryModified(T entity);
    }
}