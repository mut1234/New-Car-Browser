using AutoMapper;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Models;
using RepoPattrenWithUnitOfWork.Core.Response;
using RepoPattrenWithUnitOfWork.Core.Service.ExternalServices;

namespace RepoPattrenWithUnitOfWork.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<NhtsaMakeResult, GetAllMakesDto>();
            CreateMap<NhtsaVehicleTypeResult, VehicleTypeDto>();
            CreateMap<NhtsaModelResult, VehicleModelDto>();
        }
    }

}
