using CSharpFunctionalExtensions;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Models;
using RepoPattrenWithUnitOfWork.Core.Response;
using RepoPattrenWithUnitOfWork.Core.Service.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Interface.Service
{
    public interface INhtsaApiClient
    {
        Task<Result<NhtsaMakesResponse>> GetAllMakesAsync(CancellationToken cancellationToken = default);

        Task<Result<NhtsaVehicleTypesResponse>> GetVehicleTypesForMakeIdAsync(
        int makeId,
        CancellationToken cancellationToken = default);

        Task<Result<NhtsaModelsResponse>> GetModelsForMakeIdYearAsync(
            GetModelsForMakeIdYearQuery query, CancellationToken cancellationToken = default);

    }
}
