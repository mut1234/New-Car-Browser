using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Service
{
    public interface IQueryProcessor<TQuery, TResult>
    {
        public PagedResult<GetAllMakesDto> ProcessQuery(IEnumerable<TResult> data, TQuery query);
    }
}
