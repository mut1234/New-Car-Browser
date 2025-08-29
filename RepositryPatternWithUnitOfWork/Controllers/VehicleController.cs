using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Models;

namespace RepositryPatternWithUnitOfWork.Api.Controllers
{
    public class VehicleController :ControllerBase
    {
        private readonly IMediator _mediatR;

        public VehicleController(IMediator mediatR)
        {
            _mediatR = mediatR;

        }
        [HttpGet("makes")]
        public async Task<ActionResult<PagedResult<GetAllMakesDto>>> GetAllMakes(
            [FromQuery] GetAllMakesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediatR.Send(query);
            return result.Value != null ? Ok(result.Value) : NotFound();
        }

        [HttpGet("makes/{makeId:int}/types")]
        public async Task<ActionResult<List<VehicleTypeDto>>> GetVehicleTypesForMakeId(
              [FromRoute] int makeId,CancellationToken cancellationToken)
        {
            if (makeId <= 0)
                return BadRequest("MakeId must be greater than zero.");

            var result = await _mediatR.Send(new GetVehicleTypesForMakeIdQuery { MakeId = makeId }, cancellationToken);

            return result.Value != null ? Ok(result.Value) : NotFound();
        }

        [HttpGet("makes/{makeId:int}/models")]
        public async Task<ActionResult<PagedResult<VehicleModelDto>>> GetModelsForMakeIdYear(
    [FromRoute] int makeId,
    [FromQuery] int year,
    [FromQuery] string? vehicleType,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? searchTerm = null,
    [FromQuery] string? sortBy = null,
    [FromQuery] string? sortDirection = null,
    CancellationToken cancellationToken = default)
        {
            if (makeId <= 0) return BadRequest("MakeId must be greater than zero.");
            if (year <= 0) return BadRequest("Year must be greater than zero.");

            var query = new GetModelsForMakeIdYearQuery
            {
                MakeId = makeId,
                Year = year,
                VehicleType = vehicleType,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            var result = await _mediatR.Send(query, cancellationToken);


            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


    }
}
