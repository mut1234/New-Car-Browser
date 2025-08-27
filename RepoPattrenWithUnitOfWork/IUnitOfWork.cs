using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RepoPattrenWithUnitOfWork.Core
{

    public interface IUnitOfWork : IDisposable
    {
        DatabaseFacade Database { get; }

        //IBaseRepository<book> Books { get; }
        //IBooksRepository Books { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken ct = default);

        Task<int> CompleteTransAsync();

    }


}
