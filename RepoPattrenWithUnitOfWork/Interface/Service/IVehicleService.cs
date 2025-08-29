using CSharpFunctionalExtensions;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Models;

namespace RepoPattrenWithUnitOfWork.Core.Interface.Service
{
    public interface IVehicleService
    {
        public  Task<Result<PagedResult<GetAllMakesDto>>> GetAllMakesAsync(
          GetAllMakesQuery query,
          CancellationToken cancellationToken = default);

        public Task<Result<List<VehicleTypeDto>>> GetVehicleTypesForMakeIdAsync(
           GetVehicleTypesForMakeIdQuery query,
           CancellationToken ct = default);
        Task<Result<PagedResult<VehicleModelDto>>> GetModelsForMakeIdYearAsync(
        GetModelsForMakeIdYearQuery query, CancellationToken cancellationToken = default);
    }
}