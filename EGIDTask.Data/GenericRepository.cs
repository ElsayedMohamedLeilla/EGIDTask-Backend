using EGIDTask.Data.UnitOfWork;
using EGIDTask.Domain.Entities;
using EGIDTask.Helpers.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace EGIDTask.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private DbSet<T> _entities;

        private string _errorMessage = string.Empty;

        private bool _isDisposed;
        private ApplicationDBContext Context { get; set; }

        public GenericRepository(IUnitOfWork<ApplicationDBContext> unitOfWork)
        {
            _isDisposed = false;
            Context = unitOfWork.Context;

        }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public virtual T Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                //}
                Entities.Add(entity);
                if (Context == null || _isDisposed)
                    Context = new ApplicationDBContext();
                return entity;

            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    _errorMessage += string.Format("Part number '{0}' already exists.", s.Number);
                }
                else
                {
                    _errorMessage += string.Format("Error");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = Entities;
            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {

                //return orderBy(query).ToList();
                return orderBy(query);
            }
            else
            {
                //return query.ToList(); 
                return query;
            }
        }
        public virtual IQueryable<T> GetWithTracking(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = Entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {

                //return orderBy(query).ToList();
                return orderBy(query);
            }
            else
            {
                //return query.ToList(); 
                return query;
            }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> OrderBy(IQueryable<T> query, string orderColumn = "", string orderType = "")
        {
            var orderBy = SortingHelper<T>.GetOrderBy(orderColumn, orderType);

            return orderBy(query);
        }

    }
}