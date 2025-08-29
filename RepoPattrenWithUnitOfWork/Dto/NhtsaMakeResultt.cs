using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Dto
{
    public class NhtsaMakeResult
    {
        [JsonPropertyName("Make_ID")]
        public int MakeId { get; set; }
        [JsonPropertyName("Make_Name")]
        public string MakeName { get; set; }
    }
}
