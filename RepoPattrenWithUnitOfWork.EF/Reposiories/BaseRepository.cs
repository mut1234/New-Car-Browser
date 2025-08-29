using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using RepoPattrenWithUnitOfWork.Core.Const;
using RepoPattrenWithUnitOfWork.Core.Interface;
using System.Linq.Expressions;

namespace RepoPattrenWithUnitOfWork.EF.Reposiories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AnyAsync(predicate);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }
        public void RemoveRange(IEnumerable<T> T)
        {
            _context.Set<T>().RemoveRange(T);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByIdAsynclong(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task<T> FindByIdAsync(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(predicate).ToArrayAsync();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip)
        {

            return _context.Set<T>().Where(predicate).Skip(take).Take(take);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip,
            Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Assending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (take.HasValue)
                query = query.Take(take.Value);


            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Assending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                    query = query.OrderByDescending(orderBy);

            }
            return query.ToList();
        }
        public T Add(T entity)
        {

            _context.Set<T>().Add(entity);

            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);

            return entities;
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        public Task<T> SingleAsync(
           Expression<Func<T, bool>>? predicate,
           bool asNoTracking = true,
           CancellationToken ct = default,
          params Expression<Func<T, object>>[] includes)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));

            IQueryable<T> query = _context.Set<T>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (includes is { Length: > 0 })
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query.SingleAsync(predicate, ct);
        }

        public Task<T?> SingleOrDefaultAsync(
            Expression<Func<T, bool>>? predicate,
            bool asNoTracking = true,
            CancellationToken ct = default,
            params Expression<Func<T, object>>[] includes)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));

            IQueryable<T> query = _context.Set<T>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (includes is { Length: > 0 })
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query.SingleOrDefaultAsync(predicate, ct);
        }

        public T update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }



        public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector)
        {
            return await _context.Set<T>().Where(predicate).MaxAsync(selector);
        }

    }
}