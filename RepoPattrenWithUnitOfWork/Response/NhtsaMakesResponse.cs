using RepoPattrenWithUnitOfWork.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Response
{
    public class NhtsaMakesResponse
    {
        public List<NhtsaMakeResult> Results { get; set; } = new();
    }
}
