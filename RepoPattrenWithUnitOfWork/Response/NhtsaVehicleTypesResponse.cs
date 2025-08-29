using RepoPattrenWithUnitOfWork.Core.Service.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Response
{
    public class NhtsaVehicleTypesResponse
    {
        public List<NhtsaVehicleTypeResult> Results { get; set; } = new();

    }
}
