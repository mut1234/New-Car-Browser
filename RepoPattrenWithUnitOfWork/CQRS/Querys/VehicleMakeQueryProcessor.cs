using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Models;
using RepoPattrenWithUnitOfWork.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.CQRS.Querys
{
    public class VehicleMakeQueryProcessor : IQueryProcessor<GetAllMakesQuery, GetAllMakesDto>
    {     
        public PagedResult<GetAllMakesDto> ProcessQuery(IEnumerable<GetAllMakesDto> data, GetAllMakesQuery query)
        {
            var processedData = data;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchTerm = query.SearchTerm.Trim();
                processedData = processedData.Where(x =>
                    x.MakeName != null &&
                    x.MakeName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            processedData = ApplySorting(processedData, query.SortBy, query.SortDirection);

            var totalCount = processedData.Count();

            var pageNumber = Math.Max(1, query.PageNumber);
            var pageSize = Math.Max(1, Math.Min(100, query.PageSize)); 

            var pagedItems = processedData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<GetAllMakesDto>
            {
                Items = pagedItems,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private static IEnumerable<GetAllMakesDto> ApplySorting(
            IEnumerable<GetAllMakesDto> data,
            string? sortBy,
            string? sortDirection)
        {
            var normalizedSortBy = (sortBy ?? "name").ToLowerInvariant();
            var isDescending = string.Equals(sortDirection?.Trim().ToLowerInvariant(), "desc");

            return normalizedSortBy switch
            {
                "id" => isDescending
                    ? data.OrderByDescending(x => x.MakeId)
                    : data.OrderBy(x => x.MakeId),
                _ => isDescending
                    ? data.OrderByDescending(x => x.MakeName).ThenBy(x => x.MakeId)
                    : data.OrderBy(x => x.MakeName).ThenBy(x => x.MakeId)
            };
        }

    }

}
