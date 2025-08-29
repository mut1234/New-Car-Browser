using CSharpFunctionalExtensions;
using MediatR;
using RepoPattrenWithUnitOfWork.Core.Dto;            // define VehicleTypeDto here or adjust namespace
using RepoPattrenWithUnitOfWork.Core.Interface.Service;

namespace RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author
{
    public class GetVehicleTypesForMakeIdQuery : IRequest<Result<List<VehicleTypeDto>>>
    {
        public int MakeId { get; set; }

        //public int PageNumber { get; set; } = 1;
        //public int PageSize { get; set; } = 10;
    }


    public class GetVehicleTypesForMakeIdQueryHandler
        : IRequestHandler<GetVehicleTypesForMakeIdQuery, Result<List<VehicleTypeDto>>>
    {
        private readonly IVehicleService _vehicleService;

        public GetVehicleTypesForMakeIdQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Result<List<VehicleTypeDto>>> Handle(
            GetVehicleTypesForMakeIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.MakeId <= 0)
                return Result.Failure<List<VehicleTypeDto>>("MakeId must be greater than zero.");

            var result = await _vehicleService.GetVehicleTypesForMakeIdAsync(request, cancellationToken);

            if (result.IsFailure)
                return Result.Failure<List<VehicleTypeDto>>(result.Error);

            return Result.Success(result.Value);
        }
    }
}
