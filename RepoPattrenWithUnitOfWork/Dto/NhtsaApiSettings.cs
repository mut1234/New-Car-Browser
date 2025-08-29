using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Dto
{
    public class NhtsaApiSettings
    {
        public string BaseUrl { get; set; }

        [ConfigurationKeyName("Endpoints:GetAllMakes")]
        public string GetAllMakesEndpoint { get; set; } = "/getallmakes";
        public string GetVehicleTypesForMakeIdEndpoint { get; set; } = "/GetVehicleTypesForMakeId";
        public string GetModelsForMakeIdYearEndpoint { get; set; } = "/GetModelsForMakeIdYear";
    }


}
