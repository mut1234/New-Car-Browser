using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
namespace RepoPattrenWithUnitOfWork.EF.Triggers
{

    public class SoftDeleteTrigger : IBeforeSaveTrigger<ISoftDeletable>
    {
        private readonly ApplicationDbContext _applicationContext;

        public SoftDeleteTrigger(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task BeforeSave(ITriggerContext<ISoftDeletable> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                var entry = _applicationContext.Entry(context.Entity);
                entry.State = EntityState.Unchanged;
                context.Entity.DeletedAt = DateTime.Now;
                context.Entity.IsDeleted = true;
            }

            await Task.CompletedTask;
        }
    }

}
