using CSharpFunctionalExtensions;
using MediatR;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Models;

namespace RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author
{
    public class GetAllMakesQuery : IRequest<Result<PagedResult<GetAllMakesDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }

    }

    public class GetAllMakesQueryHandler : IRequestHandler<GetAllMakesQuery, Result<PagedResult<GetAllMakesDto>>>
    {
        private readonly IVehicleService _vehicleService;

        public GetAllMakesQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<PagedResult<GetAllMakesDto>>> Handle(GetAllMakesQuery request, CancellationToken cancellationToken)
        {
            var result = await _vehicleService.GetAllMakesAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<PagedResult<GetAllMakesDto>>(result.Error);
            }

            return Result.Success(result.Value);
        }
    }
    
    }
