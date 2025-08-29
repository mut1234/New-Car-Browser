using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Models
{
    public class VehicleModel 
    {
        public int ModelId { get; set; }
        public string? ModelName { get; set; }
        public int MakeId { get; set; }
        public string? MakeName { get; set; }
    }
}
