using CSharpFunctionalExtensions;
using MediatR;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Models;

namespace RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author
{
    public class GetModelsForMakeIdYearQuery : IRequest<Result<PagedResult<VehicleModelDto>>>
    {
        public int MakeId { get; set; }
        public int Year { get; set; }
        public string? VehicleType { get; set; }  



        //  paging/sort/search style
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }      // ModelName
        public string? SortBy { get; set; }          // "name" | "id"
        public string? SortDirection { get; set; }   // "asc" | "desc"

    }

    public class GetModelsForMakeIdYearQueryHandler : IRequestHandler<GetModelsForMakeIdYearQuery, Result<PagedResult<VehicleModelDto>>>
    {
        private readonly IVehicleService _vehicleService;

        public GetModelsForMakeIdYearQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<PagedResult<VehicleModelDto>>> Handle(GetModelsForMakeIdYearQuery request, CancellationToken cancellationToken)
        {
            var result = await _vehicleService.GetModelsForMakeIdYearAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<PagedResult<VehicleModelDto>>(result.Error);
            }

            return Result.Success(result.Value);
        }
    }
    
    }
