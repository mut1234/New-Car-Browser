using RepoPattrenWithUnitOfWork.Core.Const;
using System.Linq.Expressions;

namespace RepoPattrenWithUnitOfWork.Core.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Query();
        void RemoveRange(IEnumerable<T> t);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsynclong(long id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<long?> MaxAsync(Expression<Func<T, long>> selector);
        Task<T> FindByIdAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[]? includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip,
            Expression<Func<T, object>>? orderBy = null, string OrderByDirection = OrderBy.Assending);
        Task<T> AddAsync(T entity);
        T Add(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        Task<T> SingleAsync(
         Expression<Func<T, bool>> predicate,
         bool asNoTracking = true,
         CancellationToken ct = default,
         params Expression<Func<T, object>>[] includes);

        Task<T?> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = true,
        CancellationToken ct = default,
        params Expression<Func<T, object>>[] includes);
    }
}
