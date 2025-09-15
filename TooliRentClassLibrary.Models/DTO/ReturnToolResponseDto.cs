using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class ReturnToolResponseDto
    {
        public string Message2 { get; set; } = "Tool Returned Successfully";
        public bool IsLate { get; set; } = false;
        public string Message { get; set; } = "Total Fee Calculated";
        public decimal? TotalFee { get; set; } = 0;
    }
}
