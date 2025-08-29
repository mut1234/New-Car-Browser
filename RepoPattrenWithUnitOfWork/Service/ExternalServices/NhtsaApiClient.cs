using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Models;
using RepoPattrenWithUnitOfWork.Core.Response;
using System.Net.Http.Json;

namespace RepoPattrenWithUnitOfWork.Core.Service.ExternalServices
{
    public class NhtsaApiClient : INhtsaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NhtsaApiClient> _logger;
        private readonly NhtsaApiSettings _settings;

        public NhtsaApiClient(HttpClient httpClient, IOptions<NhtsaApiSettings> settings, ILogger<NhtsaApiClient> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<Result<NhtsaMakesResponse>> GetAllMakesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var requestUri = $"{_settings.BaseUrl.TrimEnd('/')}/{_settings.GetAllMakesEndpoint.TrimStart('/')}?format=json";

                _logger.LogInformation("Calling NHTSA API: {Uri}", requestUri);

                var response = await _httpClient.GetAsync(requestUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var error = $"NHTSA API error {(int)response.StatusCode}: {response.ReasonPhrase}";
                    _logger.LogWarning(error);
                    return Result.Failure<NhtsaMakesResponse>(error);
                }

                var data = await response.Content.ReadFromJsonAsync<NhtsaMakesResponse>(cancellationToken: cancellationToken);

                if (data?.Results == null)
                {
                    _logger.LogWarning("No data received from NHTSA API");
                    return Result.Failure<NhtsaMakesResponse>("No data received");
                }

                return Result.Success(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling NHTSA API");
                return Result.Failure<NhtsaMakesResponse>($"API call failed: {ex.Message}");
            }
        }
        public async Task<Result<NhtsaVehicleTypesResponse>> GetVehicleTypesForMakeIdAsync(
        int makeId, CancellationToken cancellationToken = default)
        {
            try
            {
                var requestUri = $"{_settings.BaseUrl.TrimEnd('/')}/" +
                                 $"{_settings.GetVehicleTypesForMakeIdEndpoint.TrimStart('/')}/" +
                                 $"{makeId}?format=json";

                _logger.LogInformation("Calling NHTSA API: {Uri}", requestUri);

                var response = await _httpClient.GetAsync(requestUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var error = $"NHTSA API error {(int)response.StatusCode}: {response.ReasonPhrase}";
                    _logger.LogWarning(error);
                    return Result.Failure<NhtsaVehicleTypesResponse>(error);
                }

                var data = await response.Content.ReadFromJsonAsync<NhtsaVehicleTypesResponse>(cancellationToken: cancellationToken);

                if (data?.Results == null)
                {
                    _logger.LogWarning("No data received from NHTSA API");
                    return Result.Failure<NhtsaVehicleTypesResponse>("No data received");
                }

                return Result.Success(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling NHTSA API");
                return Result.Failure<NhtsaVehicleTypesResponse>($"API call failed: {ex.Message}");
            }
        }
        public async Task<Result<NhtsaModelsResponse>> GetModelsForMakeIdYearAsync(
        GetModelsForMakeIdYearQuery query, CancellationToken cancellationToken = default)
        {
            if (query is null) 
                throw new ArgumentNullException(nameof(query));

            try
            {
                var basePart = $"{_settings.BaseUrl.TrimEnd('/')}/" +
                 $"{_settings.GetModelsForMakeIdYearEndpoint.TrimStart('/')}" +
                 $"/makeId/{query.MakeId}/modelyear/{query.Year}?format=json";


                var requestUri = string.IsNullOrWhiteSpace(query.VehicleType)
                    ? basePart
                    : $"{basePart}&vehicleType={Uri.EscapeDataString(query.VehicleType!)}";

                _logger.LogInformation("Calling NHTSA API: {Uri}", requestUri);

                var response = await _httpClient.GetAsync(requestUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    return Result.Failure<NhtsaModelsResponse>($"NHTSA API error {(int)response.StatusCode}: {response.ReasonPhrase}");

                var data = await response.Content.ReadFromJsonAsync<NhtsaModelsResponse>(cancellationToken: cancellationToken);
                if (data?.Results is null)
                    return Result.Failure<NhtsaModelsResponse>("No data received");

                return Result.Success(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling NHTSA API (GetModelsForMakeIdYear)");
                return Result.Failure<NhtsaModelsResponse>($"API call failed: {ex.Message}");
            }
        }

      
    }
}
