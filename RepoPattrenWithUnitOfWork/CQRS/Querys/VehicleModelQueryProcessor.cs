using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.CQRS.Querys
{
    public class VehicleModelQueryProcessor
    {
        public PagedResult<VehicleModelDto> ProcessQuery(IEnumerable<VehicleModelDto> data, GetModelsForMakeIdYearQuery query)
        {
            var processed = data;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var term = query.SearchTerm.Trim();
                processed = processed.Where(x =>
                    x.ModelName != null &&
                    x.ModelName.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            processed = ApplySorting(processed, query.SortBy, query.SortDirection);

            var total = processed.Count();
            var pageNumber = Math.Max(1, query.PageNumber);
            var pageSize = Math.Max(1, Math.Min(100, query.PageSize));

            var items = processed.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<VehicleModelDto>
            {
                Items = items,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private static IEnumerable<VehicleModelDto> ApplySorting(
            IEnumerable<VehicleModelDto> data, string? sortBy, string? sortDirection)
        {
            var key = (sortBy ?? "name").ToLowerInvariant();
            var desc = string.Equals(sortDirection?.Trim(), "desc", StringComparison.OrdinalIgnoreCase);

            return key switch
            {
                "id" => desc ? data.OrderByDescending(x => x.ModelId) : data.OrderBy(x => x.ModelId),
                _ => desc ? data.OrderByDescending(x => x.ModelName).ThenBy(x => x.ModelId)
                             : data.OrderBy(x => x.ModelName).ThenBy(x => x.ModelId)
            };
        }
    }
}