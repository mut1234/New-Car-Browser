using RepoPattrenWithUnitOfWork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Response
{
    public class NhtsaModelsResponse
    {
        public List<NhtsaModelResult> Results { get; set; } = new();
    }
}
