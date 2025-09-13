using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.CQRS.Querys;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Models;

public class VehicleService : IVehicleService
{
    private readonly INhtsaApiClient _nhtsaClient;
    private readonly VehicleMakeQueryProcessor _queryMakeProcessor;
    private readonly VehicleModelQueryProcessor _queryModelProcessor;
    private readonly ILogger<VehicleService> _logger;
    private readonly IMapper _mapper;

    public VehicleService(
        INhtsaApiClient nhtsaClient,
        VehicleMakeQueryProcessor queryMakeProcessor,
        VehicleModelQueryProcessor queryModelProcessor,
        ILogger<VehicleService> logger
        , IMapper mapper)
    {
        _nhtsaClient = nhtsaClient;
        _queryMakeProcessor = queryMakeProcessor;
        _queryModelProcessor = queryModelProcessor;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<GetAllMakesDto>>> GetAllMakesAsync(
        GetAllMakesQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query == null)
            return Result.Failure<PagedResult<GetAllMakesDto>>("Query cannot be null");

        try
        {
            var apiResult = await _nhtsaClient.GetAllMakesAsync(cancellationToken);
            if (apiResult.IsFailure)
                return Result.Failure<PagedResult<GetAllMakesDto>>(apiResult.Error);

            //_mapper
            var dtos = _mapper.Map<List<GetAllMakesDto>>(apiResult.Value.Results);


            var result = _queryMakeProcessor.ProcessQuery(dtos, query);

            _logger.LogInformation("Retrieved {Count} vehicle makes (page {Page} of {Total})",
                result.Items.Count, result.PageNumber, result.TotalPages);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllMakesAsync");
            return Result.Failure<PagedResult<GetAllMakesDto>>($"Service error: {ex.Message}");
        }
    }


    public async Task<Result<List<VehicleTypeDto>>> GetVehicleTypesForMakeIdAsync(
    GetVehicleTypesForMakeIdQuery query,
    CancellationToken ct = default)
    {
        if (query.MakeId <= 0)
            return Result.Failure<List<VehicleTypeDto>>("MakeId must be greater than zero.");

        var apiResult = await _nhtsaClient.GetVehicleTypesForMakeIdAsync(query.MakeId, ct);
        if (apiResult.IsFailure)
            return Result.Failure<List<VehicleTypeDto>>(apiResult.Error);

        //_mapper
        var dtos = _mapper.Map<List<VehicleTypeDto>>(apiResult.Value.Results);


        return Result.Success(dtos);
    }

    public async Task<Result<PagedResult<VehicleModelDto>>> GetModelsForMakeIdYearAsync(
       GetModelsForMakeIdYearQuery query, CancellationToken cancellationToken = default)
    {
        if (query is null) 
            return Result.Failure<PagedResult<VehicleModelDto>>("Query cannot be null");

        if (query.MakeId <= 0)
            return Result.Failure<PagedResult<VehicleModelDto>>("MakeId must be greater than zero.");

        if (query.Year <= 0)
            return Result.Failure<PagedResult<VehicleModelDto>>("Year must be greater than zero.");

        try
        {
            var api = await _nhtsaClient.GetModelsForMakeIdYearAsync(query, cancellationToken);
            if (api.IsFailure)
                return Result.Failure<PagedResult<VehicleModelDto>>(api.Error);

            //_mapper
            var dtos = _mapper.Map<List<VehicleModelDto>>(api.Value.Results);


            var paged = _queryModelProcessor.ProcessQuery(dtos, query);

            _logger.LogInformation("Retrieved {Count} models for Make {MakeId} / Year {Year} (page {Page}/{Total})",
                paged.Items.Count, query.MakeId, query.Year, paged.PageNumber, paged.TotalPages);

            return Result.Success(paged);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetModelsForMakeIdYearAsync");
            return Result.Failure<PagedResult<VehicleModelDto>>($"Service error: {ex.Message}");
        }
    }

}
