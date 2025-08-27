using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RepoPattrenWithUnitOfWork.Core;

namespace RepoPattrenWithUnitOfWork.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public DatabaseFacade Database => _context.Database;
   

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            // Books = new BooksRepository(_context);    
        }


        public Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken ct = default)
            => _context.Database.ExecuteSqlInterpolatedAsync(sql, ct);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var db = _context.Database;

            var hasExternalTx = db.CurrentTransaction != null;
            IDbContextTransaction? localTx = null;

            try
            {
                if (!hasExternalTx)
                    localTx = await db.BeginTransactionAsync(ct);

            
                var affected = await _context.SaveChangesAsync(ct);


                if (localTx != null)
                    await localTx.CommitAsync(ct);

                return affected;
            }
            catch
            {
                if (localTx != null)
                    await localTx.RollbackAsync(ct);

                throw;
            }
        }


        public async Task<int> CompleteTransAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                  
                    int affectedRows = await _context.SaveChangesAsync();


                    await transaction.CommitAsync();

                    return affectedRows;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
